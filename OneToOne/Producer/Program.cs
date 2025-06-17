using RabbitMQ.Client;
using System.Text;

var factory = new ConnectionFactory()
{
    Uri = new Uri("amqp://guest:guest@localhost:5672"),
    ClientProvidedName = "Producer"
};
IConnection connection = await factory.CreateConnectionAsync();
IChannel channel = await connection.CreateChannelAsync();


string exchangeName = "my_exchange";
string routingKey = "my_routing_key";
string queueName = "one_to_one_queue";

await channel.ExchangeDeclareAsync(exchangeName, ExchangeType.Direct);

await channel.QueueDeclareAsync(queueName, false, false, false, null);

await channel.QueueBindAsync(queueName, exchangeName, routingKey, null);
var properties = new BasicProperties
{
    Persistent = true
};

for (int i = 1; i <= 10; i++)
{
    string message = $"Message {i}: Hello, RabbitMQ!";
    var body = Encoding.UTF8.GetBytes(message);

    await channel.BasicPublishAsync(
     exchange: exchangeName,
     routingKey: routingKey,
     mandatory: false,
     basicProperties: properties,
     body: body);
    Console.WriteLine($"Sent: {message}");
    await Task.Delay(5000);
}

await channel.CloseAsync();
await connection.CloseAsync();