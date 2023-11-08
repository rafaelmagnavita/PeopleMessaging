using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitProjectFiles.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace RabbitMQ.Marketing.API.Subscribers
{
    public class CustomerCreatedSubscriber : IHostedService
    {
        private readonly IModel _channel;
        private readonly string EXCHANGE;
        private readonly string CUSTOMER_CREATED_QUEUE;

        public CustomerCreatedSubscriber(string host = "localhost", string connName = "curso-rabbitmq-client-publisher", string _Exchange = "curso-rabbitmq", string _Queue = "costumer-created")
        {
            EXCHANGE = _Exchange;
            CUSTOMER_CREATED_QUEUE = _Queue;
            var connectionFactory = new ConnectionFactory
            {
                HostName = host
            };

            var connection = connectionFactory.CreateConnection(connName);

            _channel = connection.CreateModel();
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += (sender, eventArgs) =>
            {
                var contentArray = eventArgs.Body.ToArray();
                var contentString = Encoding.UTF8.GetString(contentArray);

                var @event = JsonSerializer.Deserialize<CustomerCreated>(contentString);
                Console.WriteLine($"Message Received: {contentString}");
                _channel.BasicAck(eventArgs.DeliveryTag, false);
            };

            _channel.BasicConsume(CUSTOMER_CREATED_QUEUE, false, consumer);
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
