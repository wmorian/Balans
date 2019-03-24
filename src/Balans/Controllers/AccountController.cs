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
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private string path = @"C:\Projects\Balans\src\Balans\Database\database.json";

        [HttpGet("{accountId:int}")]
        public IActionResult GetEntities(int accountId)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var lines = System.IO.File.ReadAllText(this.path);

            IList<Entity> entities = new List<Entity>();

            if (!string.IsNullOrWhiteSpace(lines))
            {
                var accounts = JsonConvert.DeserializeObject<List<Account>>(lines);
                entities = accounts?.FirstOrDefault(a => a.Id == accountId).Entities.ToList();
            }

            return this.Ok(entities);
        }

        [HttpPost]
        public IActionResult CreateAccount([FromBody] Account account)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var lines = System.IO.File.ReadAllText(this.path);

            IList<Account> accounts;

            if (!string.IsNullOrWhiteSpace(lines))
            {
                accounts = JsonConvert.DeserializeObject<List<Account>>(lines);
            }
            else
            {
                accounts = new List<Account>();
            }
            

            accounts.Add(account);

            var accountJson = JsonConvert.SerializeObject(accounts);


            System.IO.File.WriteAllText(this.path, accountJson);

            return this.Ok();
        }
    }
}
