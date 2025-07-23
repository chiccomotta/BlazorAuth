using System.ComponentModel.DataAnnotations;

namespace Blazor.Data.Models;

public class Album
{
    [Key]
    public int Id { get; set; }
    public string Title { get; set; }
    public string Artist { get; set; }
    public string? Description { get; set; }
    public int ReleaseYear { get; set; }
    public string Genre { get; set; }
    // List of songs associated with the album.
    public List<Song> Songs { get; set; } = new List<Song>();
    public ICollection<AlbumArtist> AlbumArtists { get; set; } = new List<AlbumArtist>();
}