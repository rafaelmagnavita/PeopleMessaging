using RabbitMQ.Client;
using RabbitProjectFiles.Models;
using System;
using System.Text.Json;

namespace ProjetoRabbit
{
    class Program
    {
        public static ConnectionFactory conn = new ConnectionFactory { HostName = "localhost" };
        public static Person rafa = new Person("Rafael", "1412826306", new DateTime(1997, 04, 29));
        static void Main(string[] args)
        {
            var connection = conn.CreateConnection("curso-rabbitMQ");
            var channel = connection.CreateModel();
            var json = JsonSerializer.Serialize(rafa);
            Console.WriteLine("Hello World!");
        }
    }
}
