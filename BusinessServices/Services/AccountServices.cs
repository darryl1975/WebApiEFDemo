using BusinessServices.Interfaces;
using EFDemo.Helpers;
using EFDemo.Model;
using EFDemo.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices.Services
{
    public class AccountServices : IAccountServices
    {
        private UnitOfWork _unitOfWork;
        private ISortHelper<Account> _sortHelper;

        /// <summary>  
        /// Public constructor.  
        /// </summary>  
        public AccountServices()
        {
            _unitOfWork = new UnitOfWork();
            _sortHelper = new SortHelper<Account>();
        }

        public Account GetAccountByOwner(Guid ownerId, Guid id)
        {
            var account = _unitOfWork.AccountRepository.GetManyQueryable(a => a.OwnerId.Equals(ownerId) && a.Id.Equals(id)).SingleOrDefault();
            return account;
        }

        public PagedList<Account> GetAccountsByOwner(Guid ownerId, AccountParameters parameters)
        {
            var accounts = _unitOfWork.AccountRepository.GetManyQueryable(a => a.OwnerId.Equals(ownerId));

            var sortedAccounts = _sortHelper.ApplySort(accounts, parameters.OrderBy);

            return PagedList<Account>.ToPagedList(sortedAccounts,
                parameters.PageNumber,
                parameters.PageSize);
        }
    }
}
