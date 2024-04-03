using MassTransit;
using MassTransit.MongoDbIntegration.Saga;
using MediatR;
using Microsoft.Extensions.DependencyInjection.Extensions;
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
using Serilog.Events;
using System.Linq.Expressions;
//using Serilog.Sinks.Elasticsearch;
//using Serilog.Sinks.OpenTelemetry;



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
//    x.MapIdProperty(id => id.id);
//});

//BsonClassMap.RegisterClassMap<Morgly.Application.IntegrationEvents.NewMortgageEvent>();
//BsonClassMap.RegisterClassMap<Morgly.Application.IntegrationEvents.NewMortgageTermsEvent>();
//BsonClassMap.RegisterClassMap<Morgly.Application.IntegrationEvents.ApplicationCreatedEvent>();


// Add services to the container.
Log.Logger = new LoggerConfiguration()
    //.WriteTo.OpenTelemetry(endpoint: "http://localhost:4317")//,
                                                             //protocol: OtlpProtocol.HttpProtobuf)
    .MinimumLevel.Information()
    .MinimumLevel.Override("MassTransit", LogEventLevel.Debug)
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning)
    .MinimumLevel.Override("Microsoft.EntityFrameworkCore.Database.Command", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);
    builder.Services.AddMediatR(cfg =>
    {
        cfg.RegisterServicesFromAssemblyContaining(typeof(CreateMortgageCommand));
        //        cfg.AddOpenBehavior(typeof(TransactionBehavior<,>));
    });
    builder.Host.UseSerilog();
    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
    builder.Services.AddTransient<IMortgageRepository, MortgageRepository>();
    builder.Services.AddTransient<IApplicationRepository, ApplicationRepository>();
    //builder.Services.AddTransient<IDomainEventDispatcher, DomainEventDispatcher>();
    //builder.Services.AddTransient<IEventRepository, EventRepository>();

    builder.Services.AddCors(s => s.AddPolicy("default", b => b.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()));

    //builder.Services.AddSingleton<IMongoClient>(_ => new MongoClient("mongodb://localhost:27017"));
    //builder.Services.AddSingleton(x => x.GetRequiredService<IMongoClient>().GetDatabase("morg"));

    builder.Services.AddSingleton<IMongoClient>(_ => new MongoClient("mongodb://localhost:27017"));
    builder.Services.AddSingleton<IMongoDatabase>(provider => provider.GetRequiredService<IMongoClient>().GetDatabase("morg"));

    //builder.Services.AddScoped(x => x.GetRequiredService<IMongoClient>().GetDatabase("morg"));
    //builder.Services.AddScoped(x => x.GetRequiredService<IMongoClient>().StartSession());
    //builder.Services.AddScoped(x => x.GetRequiredService<IMongoDatabase>().GetCollection<IntegrationEventContainer>("IntegrationEvents"));
    //builder.Services.AddScoped(x => x.GetRequiredService<IMongoDatabase>().GetCollection<Mortgage>("mortgages"));
    //builder.Services.AddScoped(x => x.GetRequiredService<IMongoDatabase>().GetCollection<MortgageApplication>("applications"));
    builder.Services.AddMongoDbCollection<MortgageApplication>(x => x.Id);
    builder.Services.AddMongoDbCollection<Mortgage>(x => x.Id);
    //builder.Services.AddMongoDbCollection<IntegrationEventContainer>(x => x.Id);

    builder.Services.AddMassTransit(x =>
    { 
        x.AddConsumer<ApplicationStatusConsumer>();


        x.AddMongoDbOutbox(o =>
        {
            o.DisableInboxCleanupService();
            o.ClientFactory(provider => provider.GetRequiredService<IMongoClient>());
            o.DatabaseFactory(provider => provider.GetRequiredService<IMongoDatabase>());

            o.UseBusOutbox();
        });

        x.UsingRabbitMq((c, cfg) =>
        {
            cfg.AutoStart = true;
            cfg.ConfigureEndpoints(c);
        });

       
    });
    //builder.Services.AddMassTransit(x =>
    //{
    //    x.AddMongoDbOutbox(c =>
    //    {
    //        //c.DisableInboxCleanupService();
    //        c.ClientFactory(p => p.GetRequiredService<IMongoClient>());
    //        c.DatabaseFactory(p => p.GetRequiredService<IMongoDatabase>());
    //        c.UseBusOutbox(bo => bo.DisableDeliveryService());
    //    });

    //    x.UsingRabbitMq((ctx, cfg) =>
    //    {
    //        cfg.AutoStart = true;

    //        cfg.Host("localhost", "/", h =>
    //        {
    //            h.Username("guest");
    //            h.Password("guest");
    //        });
    //        //cfg.ReceiveEndpoint("application-status-updated-event", e =>
    //        //{
    //        //    e.Consumer<ApplicationStatusConsumer>(ctx);
    //        //});


    //        //cfg.ConfigureEndpoints(ctx);
    //    });

    //    //x.RemoveMassTransitHostedService();
    //    x.AddConsumer<ApplicationStatusConsumer>();
    //});



    //builder.Services.AddScoped(x => x.GetRequiredService<IClientSessionHandle>().Client.GetDatabase("morg").GetCollection<IntegrationEvent>("IntegrationEvents"));

    //builder.Services.AddScoped<TransactionIdHolder>();
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



//public class TransactionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
//{
//    private readonly IUnitOfWork _unitOfWork;
//    private readonly IPublishEndpoint _publishEndpoint;
//    private readonly TransactionIdHolder _transactionIdHolder;

//    public TransactionBehavior(IUnitOfWork unitOfWork, IPublishEndpoint publishEndpoint, TransactionIdHolder transactionIdHolder)
//    {
//        _unitOfWork = unitOfWork;
//        _publishEndpoint = publishEndpoint;
//        _transactionIdHolder = transactionIdHolder;
//    }

//    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
//    {
//        //var trans = _unitOfWork.BeginTransaction();

//        TResponse t;
//        try
//        {
//            t = await next();
//            //await _unitOfWork.SaveChanges();
//        }
//        catch (Exception)
//        {
//            //await _unitOfWork.AbortTransaction();
//            //trans.Rollback();
//            throw;
//        }

//        //var events = await _eventRepository.GetUnpublished(_transactionIdHolder.TransactionId);

//        //foreach (var e in events)
//        //{
//        //    var type = Type.GetType(e.PayloadType);
//        //    var o = System.Text.Json.JsonSerializer.Deserialize(e.Payload, type ?? throw new InvalidOperationException()); 
//        //    await _publishEndpoint.Publish(o ?? throw new InvalidOperationException(), cancellationToken);
//        //    await _eventRepository.MarkAsSent(e.Id);
//        //}

//        return t;
//    }
//}


public static class DependencyConfigurationExtensions
{
    public static IServiceCollection AddMongoDbCollection<T>(this IServiceCollection services, Expression<Func<T, Guid>> idPropertyExpression)
        where T : class
    {
        IMongoCollection<T> MongoDbCollectionFactory(IServiceProvider provider)
        {
            if (!BsonClassMap.IsClassMapRegistered(typeof(T)))
            {
                BsonClassMap.RegisterClassMap(new BsonClassMap<T>(cfg =>
                {
                    cfg.AutoMap();
                    cfg.MapIdProperty(idPropertyExpression);
                }));
            }

            var database = provider.GetRequiredService<IMongoDatabase>();
            var collectionNameFormatter = DotCaseCollectionNameFormatter.Instance;

            return database.GetCollection<T>(collectionNameFormatter.Collection<T>());
        }

        services.TryAddSingleton(MongoDbCollectionFactory);


        return services;
    }
}