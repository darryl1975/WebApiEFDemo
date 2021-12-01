using BusinessServices.Interfaces;
using EFDemo.Repository;
using Polly;
using Polly.CircuitBreaker;
using Polly.Retry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices.Services
{
    public class MessageService : IMessageService
    {
        private IMessageRepository _messageRepository;
        private RetryPolicy _retryPolicy;
        private CircuitBreakerPolicy _circuitBreakerPolicy;

        //public MessageService(IMessageRepository messageRepository)
        public MessageService()
        {
            _messageRepository =  new MessageRepository();
            _retryPolicy = Policy
                .Handle<Exception>()
                .WaitAndRetry(2, retryAttempt => {
                    var timeToWait = TimeSpan.FromSeconds(Math.Pow(2, retryAttempt));
                    Console.WriteLine($"Waiting {timeToWait.TotalSeconds} seconds");
                    return timeToWait;
                }
                );

            _circuitBreakerPolicy = Policy.Handle<Exception>()
                .CircuitBreaker(1, TimeSpan.FromMinutes(1),
                (ex, t) =>
                {
                    Console.WriteLine("Circuit broken!");
                },
                () =>
                {
                    Console.WriteLine("Circuit Reset!");
                });
        }

        public string GetHelloMessageAsync()
        {
            return _retryPolicy.Execute<string>(() => _messageRepository.GetHelloMessage());
        }

        public string GetGoodbyeMessageAsync()
        {
            try
            {
                Console.WriteLine($"Circuit State: {_circuitBreakerPolicy.CircuitState}");
                return  _circuitBreakerPolicy.Execute<string>(() =>
                {
                    return _messageRepository.GetGoodbyeMessage();
                });
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
