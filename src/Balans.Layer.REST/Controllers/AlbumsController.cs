using Balans.Layer.DAO.Database;
using Balans.Layer.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Balans.Layer.REST.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class AlbumsController : ControllerBase
  {
    private readonly BalansContext balansContext;

    public AlbumsController(BalansContext balansContext)
    {
      this.balansContext = balansContext ?? throw new ArgumentNullException("balansContext");
      if (!this.balansContext.GenreItems.Any())
      {
        // Create a new TodoItem if collection is empty,
        // which means you can't delete all TodoItems.
        //this.todoContext.TodoItems.Add(new TodoItem { Name = "Item1" });
        //this.todoContext.SaveChanges();
      }
    }

    // GET: api/Todo
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Album>>> GetAlbumItems()
    {
      return await this.balansContext.AlbumItems.ToListAsync();
    }
  }
}