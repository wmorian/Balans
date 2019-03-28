using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Balans.Controllers.DTOs
{
    public class AccountDto
    {
        public string Username { get; set; }

        public ICollection<EntityDto> Entities { get; set; }

        public AccountDto()
        {
            this.Entities = new Collection<EntityDto>();
        }
    }
}
