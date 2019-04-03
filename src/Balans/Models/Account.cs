using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Balans.Models
{
  public class Account
  {
    public int Id { get; set; }

    public string Username { get; set; }

    public ICollection<Entity> Entities { get; set; }

    public Account()
    {
      this.Entities = new Collection<Entity>();
    }
  }
}