using Microsoft.EntityFrameworkCore;

namespace Blazor.Data.Models;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Album> Albums { get; set; }
    public virtual DbSet<AlbumArtist> AlbumArtists { get; set; }
    public virtual DbSet<Artist> Artists { get; set; }
    public virtual DbSet<Song> Songs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        //        => optionsBuilder.UseNpgsql("Server=localhost;Port=5432;Database=discogs-db;Username=postgres;Password=postgres");
        
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Album>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_albums");

            entity.ToTable("albums");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Genre).HasColumnName("genre");
            entity.Property(e => e.ReleaseYear).HasColumnName("release_year");
            entity.Property(e => e.Title).HasColumnName("title");
        });

        modelBuilder.Entity<AlbumArtist>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_album_artists");

            entity.ToTable("album_artists");

            entity.HasIndex(e => e.AlbumId, "ix_album_artists_album_id");

            entity.HasIndex(e => e.ArtistId, "ix_album_artists_artist_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AlbumId).HasColumnName("album_id");
            entity.Property(e => e.ArtistId).HasColumnName("artist_id");

            entity.HasOne(d => d.Album).WithMany(p => p.AlbumArtists)
                .HasForeignKey(d => d.AlbumId)
                .HasConstraintName("fk_album_artists_albums_album_id");

            entity.HasOne(d => d.Artist).WithMany(p => p.AlbumArtists)
                .HasForeignKey(d => d.ArtistId)
                .HasConstraintName("fk_album_artists_artists_artist_id");
        });

        modelBuilder.Entity<Artist>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_artists");

            entity.ToTable("artists");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Biography).HasColumnName("biography");
            entity.Property(e => e.Name).HasColumnName("name");
        });

        modelBuilder.Entity<Song>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_songs");

            entity.ToTable("songs");

            entity.HasIndex(e => e.AlbumId, "ix_songs_album_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AlbumId).HasColumnName("album_id");
            entity.Property(e => e.DurationInSeconds).HasColumnName("duration_in_seconds");
            entity.Property(e => e.IsCover).HasColumnName("is_cover");
            entity.Property(e => e.Title).HasColumnName("title");

            entity.HasOne(d => d.Album).WithMany(p => p.Songs)
                .HasForeignKey(d => d.AlbumId)
                .HasConstraintName("fk_songs_albums_album_id");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
