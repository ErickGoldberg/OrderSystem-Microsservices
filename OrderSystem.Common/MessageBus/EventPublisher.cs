using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace OrderSystem.Common.MessageBus
{
    public class EventPublisher
    {
        private readonly IModel _channel;

        public EventPublisher(IModel channel)
        {
            _channel = channel;
        }

        public void Publish<T>(T @event, string queueName)
        {
            var message = JsonConvert.SerializeObject(@event);
            var body = Encoding.UTF8.GetBytes(message);

            _channel.BasicPublish(exchange: "", routingKey: queueName, basicProperties: null, body: body);
        }
    }
}
