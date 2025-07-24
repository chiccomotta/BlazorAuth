using Blazor.Data.Dto;
using Blazor.Data.Extensions;
using Blazor.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

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
    [Route("create-artist")]
    public async Task<ActionResult> CreateArtist([FromBody] ArtistDto request)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
        {
            throw new ArgumentException("Artist name cannot be empty.");
        }
        
        var artist = new Artist
        {
            Name = request.Name,
            Biography = request.Biography
        };
        
        await _dbContext.Artists.AddAsync(artist);
        await _dbContext.SaveChangesAsync();
        return Ok(artist);
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
            Songs = request.Songs.ToSongEntities(),
        };

        // Aggiungi la relazione con l'artista
        album.AlbumArtists.Add(new AlbumArtist
        {
            ArtistId = artist.Id,
            Album = album
        });
        
        // Aggiungi l'album al contesto
        await _dbContext.Albums.AddAsync(album);
        await _dbContext.SaveChangesAsync();
        return Ok(album);
    }

    [HttpGet]
    [Route("get-albums")]
    public async Task<ActionResult<List<Album>>> GetAlbums()
    {
        var albums = await _dbContext.Albums
            .Include(a => a.Songs)
            .ToListAsync();
        
        return Ok(albums);
    }
}