using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Costumers.API.Bus;
using RabbitProjectFiles.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RabbitMQ.Costumers.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CostumersController : ControllerBase
    {
        const string ROUTING_KEY = "costumer-created";
        private readonly IBusService _bus;

        public CostumersController(IBusService bus)
        {
            _bus = bus;
        }

        [HttpPost]
        public IActionResult Post(CustomerInputModel model)
        {
            var @event = new CustomerCreated(model.Id, model.FullName, model.Email, model.PhoneNumber, model.BirthDate);
            _bus.Publish(ROUTING_KEY, @event);

            return CreatedAtAction(nameof(Post), new { id = model.Id }, model);
        }
    }
}
