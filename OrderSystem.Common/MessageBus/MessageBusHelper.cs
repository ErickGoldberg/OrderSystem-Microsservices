using System.Text;
using RabbitMQ.Client;

namespace OrderSystem.Common.MessageBus
{
    public class MessageBusHelper
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public MessageBusHelper(string hostName, string queueName)
        {
            var factory = new ConnectionFactory { HostName = hostName };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
        }

        public void PublishMessage(string queueName, string message)
        {
            var body = Encoding.UTF8.GetBytes(message);
            _channel.BasicPublish(exchange: "", routingKey: queueName, basicProperties: null, body: body);
        }

        public void Dispose()
        {
            _channel.Close();
            _connection.Close();
        }
    }
}
