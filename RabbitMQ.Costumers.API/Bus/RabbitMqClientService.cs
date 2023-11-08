using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RabbitMQ.Costumers.API.Bus
{
    public class RabbitMqClientService : IBusService
    {
        private readonly IModel _channel;
        private readonly string EXCHANGE;
        public RabbitMqClientService(string host = "localhost", string connName = "curso-rabbitmq-client-publisher", string Exchange = "curso-rabbitmq")
        {
            EXCHANGE = Exchange;
            var connectionFactory = new ConnectionFactory
            {
                HostName = host
            };

            var connection = connectionFactory.CreateConnection(connName);

            _channel = connection.CreateModel();
        }
        public void Publish<T>(string routingKey, T message)
        {
            var json = JsonSerializer.Serialize(message);
            var byteArray = Encoding.UTF8.GetBytes(json);
            _channel.BasicPublish(EXCHANGE, routingKey, null, byteArray);
        }
    }
}
