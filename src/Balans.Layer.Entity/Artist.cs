namespace Balans.Layer.Entity
{
  using System.ComponentModel.DataAnnotations;
  using System.ComponentModel.DataAnnotations.Schema;

  [Table("Artist")]
  public class Artist
  {
    [Key]
    public int ArtistId { get; set; }

    public string Name { get; set; }
  }
}