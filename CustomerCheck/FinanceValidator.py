import pika, os, time
import json
import xgboost as xgb
import numpy as np

# Load model
xgb_classifier = xgb.XGBClassifier()
xgb_classifier.load_model('model.json')


# create a function that takes to intergers and returns their sum













def pdf_process_function(channel, msg):
  print(" PDF processing")
  print(" [x] Received " + str(msg))


 
  data =json.loads(msg)

  print(data["message"]["amount"])

  amount = np.log( float(data["message"]["amount"]))  
  print(amount)
  x  =[[1,	0,	0,	1,	0,	amount,	360.0,	1.000000,	1,	14.489984,	0.000000,	14.489984]]
  p = xgb_classifier.predict(x)

  id = data["message"]["applicationId"]
  print(id)

  time.sleep(5) # delays for 5 seconds
  print("Application processing finished");  

  status = 'approved'
  if p[0] == 1 :
    status = 'declined' 
    

  j = json.dumps({"messageType": [ "urn:message:Morgly.Application.Features.ApplicationStatusUpdated:ApplicationStatusEvent" ], "message": { 'applicationId':id, 'status':status} })
  
  channel.basic_publish(exchange='ApplicationStatus', routing_key='*',   body=j)

  return

# Access the CLODUAMQP_URL environment variable and parse it (fallback to localhost)
url = os.environ.get('CLOUDAMQP_URL', 'amqp://guest:guest@localhost:5672/%2f')
params = pika.URLParameters(url)
connection = pika.BlockingConnection(params)
channel = connection.channel() # start a channel

channel.exchange_declare(exchange='Morgly.Application.IntegrationEvents:ApplicationCreatedEvent', exchange_type='fanout', durable=True)
result  = channel.queue_declare(queue='credit_validator') # Declare a queue
queue_name = result.method.queue
channel.queue_bind(exchange='Morgly.Application.IntegrationEvents:ApplicationCreatedEvent', queue=queue_name)
# create a function which is called on incoming messages
def callback(ch, method, properties, body):
  pdf_process_function(ch, body)

# set up subscription on the queue
channel.basic_consume('credit_validator',
  callback,
  auto_ack=True)

# start consuming (blocks)

print("Starting consumer")
channel.start_consuming()
connection.close()