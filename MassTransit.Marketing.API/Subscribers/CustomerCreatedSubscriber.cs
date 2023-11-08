using Microsoft.Extensions.DependencyInjection;
using RabbitProjectFiles.Models;
using RabbitProjectFiles.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MassTransit.Marketing.API.Subscribers
{
    public class CustomerCreatedSubscriber : IConsumer<CustomerCreated>
    {
        private readonly IBus _bus;
        public IServiceProvider Services { get; }
        private readonly string EXCHANGE;
        private readonly string CUSTOMER_CREATED_QUEUE;

        public CustomerCreatedSubscriber(IServiceProvider service, IBus bus, string _Exchange = "curso-rabbitmq", string _Queue = "costumer-created")
        {
            EXCHANGE = _Exchange;
            CUSTOMER_CREATED_QUEUE = _Queue;
            _bus = bus;
            Services = service;
        }
        public async Task Consume(ConsumeContext<CustomerCreated> context)
        {
            var @event = context.Message;
            using (var scope = Services.CreateScope())
            {
                var service = scope.ServiceProvider.GetRequiredService<INotificationService>();

                await service.SendEmail(@event.Email, "boas vindas", new Dictionary<string, string> { { "name", @event.FullName } });
            }
        }
    }
}
