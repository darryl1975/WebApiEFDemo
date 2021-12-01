using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFDemo.Repository
{
    public interface IMessageRepository
    {
        string GetHelloMessage();
        string GetGoodbyeMessage();
    }
}
