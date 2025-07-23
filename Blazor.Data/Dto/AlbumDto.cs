namespace Blazor.Data.Dto;

public class AlbumDto
{
    public string Title { get; set; }
    public string? Description { get; set; }
    public int ReleaseYear { get; set; }
    public string Genre { get; set; }
    // List of songs associated with the album.
    public List<SongDto> Songs { get; set; } = [];
    public int ArtistId { get; set; }
}