using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MassTransit.Customers.API.Bus
{
    public interface IBusService
    {
        Task Publish<T>(T message);
    }
}
