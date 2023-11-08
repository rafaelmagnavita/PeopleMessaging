using MassTransit.Customers.API.Bus;
using Microsoft.AspNetCore.Mvc;
using RabbitProjectFiles.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MassTransit.Customers.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        const string ROUTING_KEY = "customer-created";
        private readonly IBusService _bus;

        public CustomersController(IBusService bus)
        {
            _bus = bus;
        }

        [HttpPost]
        public IActionResult Post(CustomerInputModel model)
        {
            var @event = new CustomerCreated(model.Id, model.FullName, model.Email, model.PhoneNumber, model.BirthDate);
            _bus.Publish(@event);
            return CreatedAtAction(nameof(Post), new { id = model.Id }, model);
        }
    }
}
