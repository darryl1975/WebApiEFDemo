using EFDemo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class OwnerDto: LinkResourceBase
    {
        public Guid Id { get; set; }
 
        public string Name { get; set; }
        
        public DateTime DateOfBirth { get; set; }
        
        public string Address { get; set; }
        
        public ICollection<Account> Accounts { get; set; }
    }
}
