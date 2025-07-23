using Blazor.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Blazor.Data;

public class AppDbContext : DbContext
{
    public virtual DbSet<Album> Albums { get; set; }
    public virtual DbSet<Song> Songs { get; set; }
    public virtual DbSet<Artist> Artists { get; set; }
    public virtual DbSet<AlbumArtist> AlbumArtists { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}