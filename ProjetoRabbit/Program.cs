using RabbitMQ.Client;
using RabbitProjectFiles.Models;
using System;
using System.Text;
using System.Text.Json;

namespace ProjetoRabbit
{
    class Program
    {
        public static ConnectionFactory conn = new ConnectionFactory { HostName = "localhost" };
        public static Person rafa = new Person("Rafael", "1412826306", new DateTime(1997, 04, 29));
        const string EXCHANGE = "curso-rabbitmq";
        static void Main(string[] args)
        {
            var connection = conn.CreateConnection("curso-rabbitMQ");
            var channel = connection.CreateModel();
            var json = JsonSerializer.Serialize(rafa);
            var byteArray = Encoding.UTF8.GetBytes(json);
            channel.BasicPublish(EXCHANGE, "rh.person-created", null, byteArray);
            Console.WriteLine($"Message Published: {json}");
        }
    }
}
