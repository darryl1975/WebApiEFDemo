using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EFDemo.Model
{
    public class User : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter a user name")]
        [MaxLength(30)]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [MaxLength(255)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [MaxLength(255)]
        [Display(Name = "Full Name")]
        public string Name { get; set; }
    }
}
