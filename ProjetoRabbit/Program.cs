using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitProjectFiles.Models;
using System;
using System.Text;
using System.Text.Json;

namespace ProjetoRabbit
{
    public class Program
    {
        public static ConnectionFactory conn = new ConnectionFactory { HostName = "localhost" };
        public static Person rafa = new Person("Rafael", "1412826306", new DateTime(1997, 04, 29));
        const string EXCHANGE = "curso-rabbitmq";
        public static string json;
        public static IModel channel;
        public static IConnection connection;
        public static byte[] byteArray;


        public static void Main(string[] args)
        {
            connection = conn.CreateConnection("curso-rabbitMQ");
            channel = connection.CreateModel();
            json = JsonSerializer.Serialize(rafa);
            byteArray = Encoding.UTF8.GetBytes(json);
        }

        public static void SendMessage()
        {
            try
            {
                channel.BasicPublish(EXCHANGE, "rh.person-created", null, byteArray);
                Console.WriteLine($"Message Published: {json}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Sending Message: {ex}");
                throw;
            }
        }

        public static void GetMessage()
        {
            try
            {
                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += async (sender, eventArgs) =>
                {
                    var contentArray = eventArgs.Body.ToArray();
                    var contentString = Encoding.UTF8.GetString(contentArray);
                    var message = JsonSerializer.Deserialize<Person>(contentString);
                    Console.WriteLine($"Message Received: {contentString}");
                    channel.BasicAck(eventArgs.DeliveryTag, false);
                };
                channel.BasicConsume("person-created", false, consumer);
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Getting Message: {ex}");
                throw;
            }
        }

    }
}
