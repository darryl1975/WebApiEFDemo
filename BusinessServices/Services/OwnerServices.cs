using BusinessServices.Interfaces;
using EFDemo.Helpers;
using EFDemo.Model;
using EFDemo.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Linq.Dynamic.Core;

namespace BusinessServices.Services
{
    public class OwnerServices : IOwnerServices
    {
        private UnitOfWork _unitOfWork;
        private ISortHelper<Owner> _sortHelper;

        /// <summary>  
        /// Public constructor.  
        /// </summary>  
        public OwnerServices()
        {
            _unitOfWork = new UnitOfWork();
            _sortHelper = new SortHelper<Owner>();
        }

        public Guid CreateOwner(Owner ownerEntity)
        {
            using (var scope = new TransactionScope())
            {
                var owner = new Owner
                {
                    
                    Name = ownerEntity.Name,
                    //DateOfBirth = ownerEntity.DateOfBirth,
                    Address = ownerEntity.Address
                };
                _unitOfWork.OwnerRepository.Insert(owner);
                _unitOfWork.Save();
                scope.Complete();
                return owner.Id;
            }
        }

        public bool DeleteOwner(Guid ownerId)
        {
            var success = false;
            if (!string.IsNullOrEmpty(ownerId.ToString()))
            {
                using (var scope = new TransactionScope())
                {
                    var owner = _unitOfWork.OwnerRepository.GetByID(ownerId);
                    if (owner != null)
                    {

                        _unitOfWork.OwnerRepository.Delete(owner);
                        _unitOfWork.Save();
                        scope.Complete();
                        success = true;
                    }
                }
            }
            return success;
        }

        public IEnumerable<Owner> GetAllOwners()
        {
            var owners = _unitOfWork.OwnerRepository.GetAll().ToList();
            if (owners.Any())
            {
                //Mapper.CreateMap<Product, ProductEntity>();
                //var productsModel = Mapper.Map<List<Product>, List<ProductEntity>>(products);
                var ownersModel = owners;
                return ownersModel;
            }
            return null;
        }

        public PagedList<Owner> GetOwners(OwnerParameters ownerParameters)
        {
            var owners = _unitOfWork.OwnerRepository.GetManyQueryable(o => o.DateOfBirth.Year >= ownerParameters.MinYearOfBirth &&
                                                                        o.DateOfBirth.Year <= ownerParameters.MaxYearOfBirth);
            //.OrderBy(on => on.Name);

            //ApplySort(ref owners, ownerParameters.OrderBy);
            var sortedOwners = _sortHelper.ApplySort(owners, ownerParameters.OrderBy);

            return PagedList<Owner>.ToPagedList(sortedOwners,
                ownerParameters.PageNumber,
                ownerParameters.PageSize);

            //return PagedList<Owner>.ToPagedList(_unitOfWork.OwnerRepository.GetAll().AsQueryable<Owner>().OrderBy(on => on.Name),
            //    ownerParameters.PageNumber,
            //    ownerParameters.PageSize);
        }

        public Owner GetOwnerById(Guid ownerId)
        {
            var owner = _unitOfWork.OwnerRepository.GetByID(ownerId);
            if (owner != null)
            {
                var ownerModel = owner;
                return ownerModel;
            }
            return null;
        }

        public bool UpdateOwner(Guid ownerId, Owner ownerEntity)
        {
            var success = false;
            if (ownerEntity != null)
            {
                using (var scope = new TransactionScope())
                {
                    var owner = _unitOfWork.OwnerRepository.GetByID(ownerId);
                    if (owner != null)
                    {
                        owner.Name = ownerEntity.Name;
                        owner.Address = ownerEntity.Address;
                        owner.DateOfBirth = ownerEntity.DateOfBirth;
                        _unitOfWork.OwnerRepository.Update(owner);
                        _unitOfWork.Save();
                        scope.Complete();
                        success = true;
                    }
                }
            }
            return success;
        }

        private void ApplySort(ref IQueryable<Owner> owners, string orderByQueryString)
        {
            if (!owners.Any())
                return;
            if (string.IsNullOrWhiteSpace(orderByQueryString))
            {
                owners = owners.OrderBy(x => x.Name);
                return;
            }
            var orderParams = orderByQueryString.Trim().Split(',');
            var propertyInfos = typeof(Owner).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var orderQueryBuilder = new StringBuilder();
            foreach (var param in orderParams)
            {
                if (string.IsNullOrWhiteSpace(param))
                    continue;
                var propertyFromQueryName = param.Split(" ")[0];
                var objectProperty = propertyInfos.FirstOrDefault(pi => pi.Name.Equals(propertyFromQueryName, StringComparison.InvariantCultureIgnoreCase));
                if (objectProperty == null)
                    continue;
                var sortingOrder = param.EndsWith(" desc") ? "descending" : "ascending";
                orderQueryBuilder.Append($"{objectProperty.Name.ToString()} {sortingOrder}, ");
            }
            var orderQuery = orderQueryBuilder.ToString().TrimEnd(',', ' ');
            if (string.IsNullOrWhiteSpace(orderQuery))
            {
                owners = owners.OrderBy(x => x.Name);
                return;
            }
            owners = owners.OrderBy(orderQuery);
        }
    }
}
