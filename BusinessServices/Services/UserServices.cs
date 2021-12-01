using BusinessServices.Interfaces;
using EFDemo.Model;
using EFDemo.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices.Services
{
    public class UserServices : IUserServices
    {
        private UnitOfWork _unitOfWork;

        private List<User> _users = new List<User>();

        /// <summary>  
        /// Public constructor.  
        /// </summary>  
        public UserServices()
        {
            _unitOfWork = new UnitOfWork();
        }

        /// <summary>  
        /// Public method to authenticate user by user name and word.  
        /// </summary>  
        /// <param name="userName"></param>  
        /// <param name="word"></param>  
        /// <returns></returns>  
        public bool Authenticate(string userName, string word)
        {
            var user = _unitOfWork.UserRepository.Get(u => u.UserName == userName && u.Password == word);
            if (user != null && user.Id > 0)
            {
                return true;
            }
            return false;
        }

        public async Task<User> AuthenticateUser(string userName, string word)
        {
            // wrapped in "await Task.Run" to mimic fetching user from a db
            var user = await Task.Run(() => _unitOfWork.UserRepository.Get(u => u.UserName == userName && u.Password == word));

            // return null if user not found
            if (user == null)
                return null;

            // authentication successful so return user details
            return user;
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            // wrapped in "await Task.Run" to mimic fetching users from a db
            return await Task.Run(() => _unitOfWork.UserRepository.GetAll());
        }
    }
}
