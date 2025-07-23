using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blazor.Data.Models;

public class AlbumArtist
{
    [Key]
    public int Id { get; set; }

    [ForeignKey(nameof(Album))]
    public int AlbumId { get; set; }
    public Album Album { get; set; } = null!;

    [ForeignKey(nameof(Artist))]
    public int ArtistId { get; set; }
    public Artist Artist { get; set; } = null!;
}