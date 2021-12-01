using BusinessServices.Interfaces;
using BusinessServices.Services;
using EFDemo.Options;
using EFDemo.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private IMessageService _messageService;

        public MessageController()
        {
            _messageService = new MessageService();
        }

        [HttpGet("hello")]
        public string GetHello()
        {
            var result = _messageService.GetHelloMessageAsync();
            return result;
        }

        [HttpGet("goodbye")]
        public string GetGoodbye()
        {
            var result = _messageService.GetGoodbyeMessageAsync();
            return result;
        }
    }
}
