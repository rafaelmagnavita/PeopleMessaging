using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using RabbitProjectFiles.Models;
using RabbitProjectFiles.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EasyNetQ.Marketing.API.Subscribers
{
    public class CustomerCreatedSubscriber : IHostedService
    {
        private readonly IAdvancedBus _bus;
        public IServiceProvider Services { get;}
        private readonly string EXCHANGE;
        private readonly string CUSTOMER_CREATED_QUEUE;

        public CustomerCreatedSubscriber(IServiceProvider service, IBus bus, string _Exchange = "curso-rabbitmq", string _Queue = "costumer-created")
        {
            EXCHANGE = _Exchange;
            CUSTOMER_CREATED_QUEUE = _Queue;
            _bus = bus.Advanced;
            Services = service;
        }
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var queue = _bus.QueueDeclare(CUSTOMER_CREATED_QUEUE);

            _bus.Consume<CustomerCreated>(queue, async (msg, info) =>
            {
                var json = JsonConvert.SerializeObject(msg.Body);
                await SendEmail(msg.Body);
                Console.WriteLine($"Message received: {json}");
            });
        }

        public async Task SendEmail(CustomerCreated @event)
        {
            using (var scope = Services.CreateScope())
            {
                var service = scope.ServiceProvider.GetRequiredService<INotificationService>();

                await service.SendEmail(@event.Email, CUSTOMER_CREATED_QUEUE, new Dictionary<string, string> { { "name", @event.FullName } });
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
