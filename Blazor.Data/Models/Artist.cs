using System.ComponentModel.DataAnnotations;
using Blazor.Data.Models;

public class Artist
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Biography { get; set; }
    public ICollection<AlbumArtist> AlbumArtists { get; set; } = new List<AlbumArtist>();
}