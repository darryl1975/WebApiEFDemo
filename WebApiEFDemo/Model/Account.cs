using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EFDemo.Model
{
    //[Table("Account")]
    public class Account
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Account type is required")]
        public TypeOfAccount Type { get; set; }

        public string Description { get; set; }

        //[ForeignKey(nameof(Owner))]
        [ForeignKey("OwnerId")]
        public Guid OwnerId { get; set; }
        public Owner Owner { get; set; }
    }
}
