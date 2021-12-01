using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFDemo.Model
{
    public class AccountParameters : QueryStringParameters
    {
        public AccountParameters()
        {
            OrderBy = "Type";
        }
    }
}
