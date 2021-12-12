using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleGraphQL.Models
{
    public class UserDetailsOutPutModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string PostCode { get; set; }
        public string Suburb { get; set; }
        public string City { get; set; }
        public string State { get; set; }

    }
   
}
