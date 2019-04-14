using Balans.Controllers.DTOs;
using Balans.Database;
using Balans.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;

namespace Balans.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly AccountContext context;
        private readonly IConfiguration configuration;
        private readonly string path;

        public AccountController(AccountContext context, IConfiguration configuration)
        {
            this.context = context;
            this.configuration = configuration;
            this.path = this.configuration.GetSection("dbPath").Value;
        }

        [HttpGet("{user}")]
        public IActionResult GetAccount(string user)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var account = this.context.Accounts.Include(a => a.Entities).FirstOrDefault(a => a.Username == user);

            if (account == null)
            {
                return this.NotFound(user);
            }

            var entityDtos = account.Entities.Select(e => new EntityDto { Name = e.Name, Amount = e.Amount }).ToList();

            var accountDto = new AccountDto { Username = account.Username, Entities = entityDtos };

            return this.Ok(accountDto);
        }

        [HttpGet("entities/{user}")]
        public IActionResult GetEntities(string user)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var account = this.context.Accounts.Include(a => a.Entities).FirstOrDefault(a => a.Username == user);

            if (account == null)
            {
                return this.NotFound($"Account with the Id: {account} not found.");
            }

            var result = account.Entities.Select(e => new EntityDto { Name = e.Name, Amount = e.Amount });

            return this.Ok(result);
        }

        [HttpGet("balance/{username}")]
        public IActionResult GetBalance(string username)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var account = this.context.Accounts.Include(a => a.Entities).FirstOrDefault(a => a.Username == username);

            if (account == null)
            {
                return this.NotFound("User not found.");
            }

            float balance = account.Entities.Sum(e => e.Amount);

            return this.Ok(balance);
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] AccountDto accountDto)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            if (this.context.Accounts.Any(a => a.Username == accountDto.Username))
            {
                return this.Forbid($"{accountDto.Username} already exists.");
            }

            var account = new Account { Username = accountDto.Username, CreateTime = DateTime.Now };
            this.context.Accounts.Add(account);
            this.context.SaveChanges();

            return this.Ok();
        }

        [HttpPost("addentity/{username}")]
        public IActionResult AddEntity(string username, [FromBody] EntityDto entityDto)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var account = this.context.Accounts.FirstOrDefault(a => a.Username == username);

            if (account == null)
            {
                return this.NotFound("User not found.");
            }

            var entity = new Entity { Name = entityDto.Name, Amount = entityDto.Amount, CreateTime = DateTime.Now };

            account.Entities.Add(entity);

            this.context.SaveChanges();

            return this.Ok();
        }
    }
}