using EFDemo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices.Interfaces
{
    public interface IUserServices
    {
        bool Authenticate(string userName, string word);

        Task<User> AuthenticateUser(string userName, string word);

        Task<IEnumerable<User>> GetAll();
    }
}
