using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EFDemo.Model
{
    public class BaseEntity
    {
        [MaxLength(200)]
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }

        [MaxLength(200)]
        public string UpdatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
    }
}
