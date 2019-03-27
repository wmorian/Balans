using Balans.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
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
        private readonly IConfiguration configuration;
        private readonly string path;

        public AccountController(IConfiguration configuration)
        {
            this.configuration = configuration;
            this.path = this.configuration.GetSection("dbPath").Value;
        }


        [HttpGet("entities/{accountId:int}")]
        public IActionResult GetEntities(int accountId)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            IList<Entity> entities = this.GetEntitiesFormDatabase(accountId);

            return this.Ok(entities);
        }

        

        [HttpGet("balance/{accountId:int}")]
        public IActionResult GetBalance(int accountId)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            IList<Entity> entities = this.GetEntitiesFormDatabase(accountId);

            float balance = entities.Sum(e => e.Amount);

            return this.Ok(balance);
        }

        [HttpPost("createaccount")]
        public IActionResult CreateAccount([FromBody] Account account)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            this.SaveAccount(account);

            return this.Ok();
        }

        [HttpPost("addentity")]
        public IActionResult AddEntity([FromBody] Entity entity)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            IList<Account> accounts = this.GetAccountsFromDatabase();

            if (accounts.Count <= 0)
            {
                return this.UnprocessableEntity($"DB Empty.");
            }
            var account = accounts.FirstOrDefault(a => a.Id == entity.AccountId);

            if (account == null)
            {
                return this.UnprocessableEntity($"Account with the Id {entity.AccountId} not found.");
            }
            account.Entities.Add(entity);

            var accountJson = JsonConvert.SerializeObject(accounts);

            System.IO.File.WriteAllText(this.path, accountJson);

            return this.Ok();
        }

        private void SaveAccount(Account account)
        {
            IList<Account> accounts = this.GetAccountsFromDatabase();

            accounts.Add(account);

            var accountJson = JsonConvert.SerializeObject(accounts);


            System.IO.File.WriteAllText(this.path, accountJson);
        }

        private IList<Account> GetAccountsFromDatabase()
        {
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

            return accounts;
        }

        private IList<Entity> GetEntitiesFormDatabase(int accountId)
        {
            var lines = System.IO.File.ReadAllText(this.path);

            IList<Entity> entities = new List<Entity>();

            if (!string.IsNullOrWhiteSpace(lines))
            {
                var accounts = JsonConvert.DeserializeObject<List<Account>>(lines);
                entities = accounts?.FirstOrDefault(a => a.Id == accountId).Entities.ToList();
            }

            return entities;
        }
    }
}
