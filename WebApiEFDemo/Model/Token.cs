using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EFDemo.Model
{
    public class Token : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(250)]
        [Display(Name = "Authentication Token")]
        public string AuthToken { get; set; }

        public DateTime IssuedOn { get; set; }

        public DateTime ExpiredOn { get; set; }

        public virtual User user { get; set; }
    }
}
