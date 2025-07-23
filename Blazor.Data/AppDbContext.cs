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
        // Configura la chiave composta per AlbumArtist
        modelBuilder.Entity<AlbumArtist>()
            .HasKey(aa => aa.Id);
        
        // Configura le relazioni
        modelBuilder.Entity<AlbumArtist>()
            .HasOne(aa => aa.Album)
            .WithMany(a => a.AlbumArtists)
            .HasForeignKey(aa => aa.AlbumId);
        
        modelBuilder.Entity<AlbumArtist>()
            .HasOne(aa => aa.Artist)
            .WithMany(a => a.AlbumArtists)
            .HasForeignKey(aa => aa.ArtistId);
    }
}