using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Balans.Layer.Entity
{
  [Table("Genres")]
  public class Genre
  {
    [Key]
    public int GenreId { get; set; }

    public string Name { get; set; }
  }
}