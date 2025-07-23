using System;
using System.Collections.Generic;

namespace Blazor.Data.Models;

public partial class AlbumArtist
{
    public int Id { get; set; }

    public int AlbumId { get; set; }

    public int ArtistId { get; set; }

    public virtual Album Album { get; set; } = null!;

    public virtual Artist Artist { get; set; } = null!;
}
