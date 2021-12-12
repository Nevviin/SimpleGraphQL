using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleGraphQL.Models
{
    public class UserDetailsInputModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
    }
}
