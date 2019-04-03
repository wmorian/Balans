using Balans.Layer.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Balans.Layer.DAO.Database
{
  public class BalansContext : DbContext
  {
    public BalansContext(DbContextOptions<BalansContext> options) : base(options)
    {
    }

    public DbSet<Album> AlbumItems { get; set; }

    public DbSet<Genre> GenreItems { get; set; }
  }
}