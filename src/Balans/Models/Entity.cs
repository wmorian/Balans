using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Balans.Models
{
    public class Entity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public float Amount { get; set; }
        
        //public DateTime ExpireDate { get; set; }

        public int AccountId { get; set; }
    }
}
