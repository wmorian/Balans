using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Balans.Models.Banks.DKB
{
    public class BankStatement
    {
        public string AccountNumber { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public IList<string> Headers { get; set; }

        public ICollection<DkbEntity> Entities { get; set; }

        public BankStatement()
        {
            this.Entities = new Collection<DkbEntity>();
        }
    }
}
