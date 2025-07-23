using System;
using System.Collections.Generic;

namespace Blazor.Data.Models;

public partial class Artist
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Biography { get; set; }

    public virtual ICollection<AlbumArtist> AlbumArtists { get; set; } = new List<AlbumArtist>();
}
