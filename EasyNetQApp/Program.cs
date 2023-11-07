﻿using System;
using EasyNetQ;
using EasyNetQ.Topology;
using Newtonsoft.Json;
using RabbitProjectFiles.Models;

namespace EasyNetQApp
{
    class Program
    {
        public static string EXCHANGE = "curso-rabbitmq";
        public static string QUEUE = "person-created";
        public static string ROUTING_KEY = "rh.person-created";
        private static Person rafa = new Person("Rafael Mag", "1412826306", new DateTime(1997, 04, 29));
        private static IBus bus;
        private static IAdvancedBus advanced;
        private static Exchange exchange;
        private static Queue queue;

        public Program()
        {
            Start();
        }

        static void Main(string[] args)
        {
            Start();
            GetMessageAdvanced();
            Console.ReadLine();
        }

        private static void Start()
        {
            bus = RabbitHutch.CreateBus("host=localhost");
            advanced = bus.Advanced;
            queue = advanced.QueueDeclare(QUEUE);
            exchange = advanced.ExchangeDeclare(EXCHANGE, "topic");
        }

        public static void SendMessageAdvanced()
        {
            advanced.Publish(exchange, ROUTING_KEY, true, new Message<Person>(rafa));
        }
        public static void GetMessageAdvanced()
        {
            advanced.Consume<Person>(queue, (msg,info) => 
            {
                var json = JsonConvert.SerializeObject(msg.Body);
                Console.WriteLine(json);
            });
        }
        public static void SendMessage()
        {
            try
            {
                bus.PubSub.PublishAsync(rafa).GetAwaiter();
                Console.ReadLine();
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
                bus.PubSub.SubscribeAsync<Person>("marketing", msg => {
                    var json = JsonConvert.SerializeObject(msg);
                    Console.WriteLine(json);
                }).GetAwaiter();
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
