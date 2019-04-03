namespace Balans.Layer.Entity
{
  using System;
  using System.ComponentModel.DataAnnotations;
  using System.ComponentModel.DataAnnotations.Schema;

  [Table("Albums")]
  public class Album
  {
    [Key]
    public int AlbumId { get; set; }

    public string Title { get; set; }
    public int ArtistId { get; set; }
  }
}