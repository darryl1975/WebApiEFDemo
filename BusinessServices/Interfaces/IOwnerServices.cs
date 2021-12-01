using EFDemo.Helpers;
using EFDemo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices.Interfaces
{
    public interface IOwnerServices
    {
        Owner GetOwnerById(Guid ownerId);
        IEnumerable<Owner> GetAllOwners();
        PagedList<Owner> GetOwners(OwnerParameters ownerParameters);
        Guid CreateOwner(Owner ownerEntity);
        bool UpdateOwner(Guid ownerId, Owner ownerEntity);
        bool DeleteOwner(Guid ownerId);
    }
}
