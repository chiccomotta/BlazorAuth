using System;
using System.Collections.Generic;

namespace Blazor.Data.Models;

public partial class Album
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public int ReleaseYear { get; set; }

    public string Genre { get; set; } = null!;

    public virtual ICollection<AlbumArtist> AlbumArtists { get; set; } = new List<AlbumArtist>();

    public virtual ICollection<Song> Songs { get; set; } = new List<Song>();
}
