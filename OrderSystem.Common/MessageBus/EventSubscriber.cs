using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace OrderSystem.Common.MessageBus
{
    public class EventSubscriber(IModel channel)
    {
        public void Subscribe(string queueName, Action<string> onMessageReceived)
        {
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                onMessageReceived(message);
            };

            channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);
        }
    }
}
