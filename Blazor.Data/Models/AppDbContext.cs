using Microsoft.EntityFrameworkCore;

namespace Blazor.Data.Models;

public partial class AppDbContext : DbContext
{
    public DbSet<Artist> Artists { get; set; }
    public DbSet<Album> Albums { get; set; }
    public DbSet<AlbumArtist> AlbumArtists { get; set; }
    public DbSet<Song> Songs { get; set; }
    
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
}
