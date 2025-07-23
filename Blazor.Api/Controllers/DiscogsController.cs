using Blazor.Data;
using Blazor.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blazor.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DiscogsController : ControllerBase
{
    private readonly AppDbContext _dbContext;

    public DiscogsController(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
        
    [HttpPost]
    [Route("create-album")]
    public async Task<ActionResult> CreateAlbum([FromBody] AlbumDto request)
    {
        // Handle the case where the artist doesn't exist.
        var artist = await _dbContext.Artists.FirstOrDefaultAsync(a => a.Id == request.ArtistId);
        if (artist == null)
        {
            throw new ArgumentException($"Artist with ID {request.ArtistId} does not exist.");
        }

        var album = new Album
        {
            Title = request.Title,
            Description = request.Description,
            ReleaseYear = request.ReleaseYear,
            Genre = request.Genre,
            Songs = request.Songs,
        };

        // Add the album-artist relationship.
        var albumArtist = new AlbumArtist { Album = album, Artist = artist };
        _dbContext.AlbumArtists.Add(albumArtist);
        _dbContext.Albums.Add(album);
        
        await _dbContext.SaveChangesAsync();
        return Ok(album);
    }
}

public class AlbumDto
{
    public string Title { get; set; }
    public string? Description { get; set; }
    public int ReleaseYear { get; set; }
    public string Genre { get; set; }
    // List of songs associated with the album.
    public List<Song> Songs { get; set; } = [];
    public int ArtistId { get; set; }
}