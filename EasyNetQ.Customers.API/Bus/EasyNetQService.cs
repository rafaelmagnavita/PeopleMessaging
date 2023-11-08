using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyNetQ.Customers.API.Bus
{
    public class EasyNetQService : IBusService
    {
        private readonly IAdvancedBus _Bus;
        private readonly string EXCHANGE = "curso-rabbitmq";

        public EasyNetQService(IBus bus)
        {
            _Bus = bus.Advanced;
        }


        public void Publish<T>(string routingKey, T message)
        {
            var exchange = _Bus.ExchangeDeclare(EXCHANGE, "topic");
            _Bus.Publish(exchange, routingKey, true, new Message<T>(message));
        }
    }
}
