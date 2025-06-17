using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

var factory = new ConnectionFactory()
{
    Uri = new Uri("amqp://guest:guest@localhost:5672"),
    ClientProvidedName = "Consumer",
};
IConnection connection = await factory.CreateConnectionAsync();
IChannel channel = await connection.CreateChannelAsync();


string exchangeName = "my_exchange";
string routingKey = "my_routing_key";
string queueName = "one_to_one_queue";

await channel.ExchangeDeclareAsync(exchangeName, ExchangeType.Direct);

await channel.QueueDeclareAsync(queueName, false, false, false, null);

await channel.QueueBindAsync(queueName, exchangeName, routingKey, null);

await channel.BasicQosAsync(0, 1, false);

var consumer = new AsyncEventingBasicConsumer(channel);

consumer.ReceivedAsync += async (model, ea) =>
{
    var body = ea.Body.ToArray();
    string message = Encoding.UTF8.GetString(body);
    Console.WriteLine($"Received: {message}");
    await channel.BasicAckAsync(ea.DeliveryTag, false);
    await Task.Delay(1000);

};

await channel.BasicConsumeAsync(queue: queueName, autoAck: false, consumer: consumer);

Console.WriteLine("Waiting for messages...");
Console.ReadLine();

await channel.CloseAsync();
await connection.CloseAsync();
