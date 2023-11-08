using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyNetQ.Customers.API.Bus
{
    public interface IBusService
    {
        void Publish<T>(string routingKey, T message);
    }
}
