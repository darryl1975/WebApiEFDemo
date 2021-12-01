using EFDemo.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Configuration;
using System.IO;

namespace EFDemo.Repository
{
    public class MessageRepository : IMessageRepository
    {
        private MessageOptions _messageOptions;

        private IConfigurationBuilder builder2;
        private IConfiguration configuration;
        private IConfigurationSection configurationSection;
        public MessageRepository()
        {
            //var builder = new ConfigurationBuilder()
            //  .SetBasePath(Directory.GetCurrentDirectory())
            //  .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            //  .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true);

            //configuration = builder.Build();

            // configurationSection.Key => FilePath
            // configurationSection.Value => C:\\temp\\logs\\output.txt
            //configurationSection = configuration.GetSection("Message").GetSection("GoodbyeMessage");

            var config = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json").Build();


            var section = config.GetSection("Message");
            var messageOptions = section.Get<MessageOptions>();

            _messageOptions = messageOptions;
        }

        public string GetHelloMessage()
        {
            Console.WriteLine("MessageRepository GetHelloMessage running");
            ThrowRandomException();

            return _messageOptions.HelloMessage;
        }

        public string GetGoodbyeMessage()
        {
            Console.WriteLine("MessageRepository GetGoodbyeMessage running");
            ThrowRandomException();

            return _messageOptions.GoodbyeMessage;
        }

        private void ThrowRandomException()
        {
            var diceRoll = new Random().Next(0, 10);

            if (diceRoll > 5)
            {
                Console.WriteLine("ERROR! Throwing Exception");
                throw new Exception("Exception in MessageRepository");
            }
        }
    }
}
