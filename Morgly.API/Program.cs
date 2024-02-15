using MassTransit;
using MediatR;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using Morgly.Application;
using Morgly.Application.Common;
using Morgly.Application.Features.ApplicationStatusUpdated;
using Morgly.Application.Features.Mortgage.Command;
using Morgly.Application.Interfaces;
using Morgly.Domain.Entities;
using Morgly.Domain.IntegrationEvents;
using Morgly.Domain.Repositories;
using Morgly.Infrastructure;
//using OpenTelemetry.Exporter;
//using OpenTelemetry.Metrics;
//using OpenTelemetry.Resources;
//using OpenTelemetry.Trace;
using Serilog;
//using Serilog.Sinks.Elasticsearch;
//using Serilog.Sinks.OpenTelemetry;


var builder = WebApplication.CreateBuilder(args);
BsonClassMap.RegisterClassMap<Mortgage>(x =>
{
    x.AutoMap();
    x.MapField("_amount").SetElementName("Amount");
    x.MapField("_originalAmount").SetElementName("OriginalAmount");
    x.MapIdProperty(id => id.Id);
});
//BsonClassMap.RegisterClassMap<IntegrationEventContainer>(x =>
//{
//    x.AutoMap();
//    x.MapIdProperty(id => id.Id);
//});

//BsonClassMap.RegisterClassMap<Morgly.Application.IntegrationEvents.NewMortgageEvent>();
//BsonClassMap.RegisterClassMap<Morgly.Application.IntegrationEvents.NewMortgageTermsEvent>();
//BsonClassMap.RegisterClassMap<Morgly.Application.IntegrationEvents.ApplicationCreatedEvent>();


// Add services to the container.
Log.Logger = new LoggerConfiguration()
    .WriteTo.OpenTelemetry(endpoint: "http://localhost:4317")//,
                                                             //protocol: OtlpProtocol.HttpProtobuf)
    .WriteTo.Console()
    //.WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri("http://localhost:9200")))
    .CreateLogger();
try
{
    builder.Services.AddMediatR(cfg =>
    {
        cfg.RegisterServicesFromAssemblyContaining(typeof(CreateMortgageCommand));
        cfg.AddOpenBehavior(typeof(TransactionBehavior<,>));
    });

    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
    builder.Services.AddTransient<IMortgageRepository, MortgageRepository>();
    builder.Services.AddTransient<IApplicationRepository, ApplicationRepository>();
    builder.Services.AddTransient<IDomainEventDispatcher, DomainEventDispatcher>();
    builder.Services.AddTransient<IEventRepository, EventRepository>();

    builder.Services.AddCors(s => s.AddPolicy("default", b => b.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()));
    builder.Services.AddMassTransit(x =>
    {
        x.UsingRabbitMq((ctx, cfg) =>
        {
            cfg.Host("localhost", "/", h =>
            {
                h.Username("guest");
                h.Password("guest");
            });
            
            //cfg.ReceiveEndpoint("application-status-updated-event", e =>
            //{
            //    e.Consumer<ApplicationStatusConsumer>(ctx);
            //});


            cfg.ConfigureEndpoints(ctx);
        });
        x.AddConsumer<ApplicationStatusConsumer>();
    });


    builder.Services.AddScoped<IMongoClient>(_ => new MongoClient("mongodb://localhost:27017"));
    //builder.Services.AddScoped(x => x.GetRequiredService<IMongoClient>().GetDatabase("morg"));
    builder.Services.AddScoped(x => x.GetRequiredService<IMongoClient>().StartSession());
    builder.Services.AddScoped(x => x.GetRequiredService<IMongoClient>().GetDatabase("morg").GetCollection<IntegrationEventContainer>("IntegrationEvents"));
    builder.Services.AddScoped(x => x.GetRequiredService<IMongoClient>().GetDatabase("morg").GetCollection<Mortgage>("mortgages"));
    builder.Services.AddScoped(x => x.GetRequiredService<IMongoClient>().GetDatabase("morg").GetCollection<MortgageApplication>("applications"));
    //builder.Services.AddScoped(x => x.GetRequiredService<IClientSessionHandle>().Client.GetDatabase("morg").GetCollection<IntegrationEvent>("IntegrationEvents"));

    builder.Services.AddScoped<TransactionIdHolder>();
    //builder.Services.AddOpenTelemetry()
    //    .WithTracing(b =>
    //    {
    //        b
    //            .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("Mortgage"))
    //            .AddHttpClientInstrumentation()
    //            .AddAspNetCoreInstrumentation()
    //            .AddConsoleExporter()
    //            .AddOtlpExporter(opt =>
    //            {
    //                //opt.Protocol = OtlpExportProtocol.HttpProtobuf;
    //                opt.Endpoint = new Uri("http://localhost:4317");
    //            });

    //    }).WithMetrics(mpb =>
    //    {
    //        mpb.AddAspNetCoreInstrumentation()
    //            .AddConsoleExporter()          
    //            .AddOtlpExporter(opt =>
    //            {
    //                //opt.Protocol = OtlpExportProtocol.HttpProtobuf;
    //                opt.Endpoint = new Uri("http://localhost:4317");
    //            });
    //    });

    builder.Host.UseSerilog();
    
    var app = builder.Build();


    app.UseSerilogRequestLogging();
    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseCors("default");
    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}



public class TransactionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly IEventRepository _eventRepository;
    private readonly TransactionIdHolder _transactionIdHolder;

    public TransactionBehavior(IUnitOfWork unitOfWork, IPublishEndpoint publishEndpoint, IEventRepository eventRepository, TransactionIdHolder transactionIdHolder)
    {
        _publishEndpoint = publishEndpoint;
        _eventRepository = eventRepository;
        _transactionIdHolder = transactionIdHolder;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        //var trans = _unitOfWork.BeginTransaction();

        TResponse t;
        try
        {
            t = await next();
            //trans.Commit();
        }
        catch (Exception)
        {
            //trans.Rollback();
            throw;
        }

        var events = await _eventRepository.GetUnpublished(_transactionIdHolder.TransactionId);

        foreach (var e in events)
        {
            var type = Type.GetType(e.PayloadType);
            var o = System.Text.Json.JsonSerializer.Deserialize(e.Payload, type ?? throw new InvalidOperationException()); 
            await _publishEndpoint.Publish(o ?? throw new InvalidOperationException(), cancellationToken);
            await _eventRepository.MarkAsSent(e.Id);
        }

        return t;
    }
}