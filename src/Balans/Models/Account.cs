using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Balans.Models
{
    public class Account
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public DateTime CreateTime { get; set; }

        public ICollection<Entity> Entities { get; set; }

        public Account()
        {
            this.Entities = new Collection<Entity>();
        }
    }
}