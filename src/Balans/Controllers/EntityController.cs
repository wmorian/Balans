using Balans.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Balans.Controllers
{
  /// <summary>
  /// To manage Entity information queries.
  /// </summary>
  /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
  [Route("api/[controller]")]
  public class EntityController : ControllerBase
  {
    [HttpPost]
    public void AddEntity([FromBody] Entity entity)
    {
      //TODO
      var path = @"C:\Projects\Balans\src\Balans\Database\database.json";

      var lines = System.IO.File.ReadAllText(path);

      IList<Account> accounts;

      if (!string.IsNullOrWhiteSpace(lines))
      {
        accounts = JsonConvert.DeserializeObject<List<Account>>(lines);
      }
      else
      {
        return;
      }

      var account = accounts.FirstOrDefault(a => a.Id == entity.AccountId);
      account.Entities.Add(entity);

      var accountJson = JsonConvert.SerializeObject(accounts);

      System.IO.File.WriteAllText(@"C:\Projects\Balans\src\Balans\Database\database.json", accountJson);
    }
  }
}