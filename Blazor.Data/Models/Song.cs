using System;
using System.Collections.Generic;

namespace Blazor.Data.Models;

public partial class Song
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public int DurationInSeconds { get; set; }

    public int AlbumId { get; set; }

    public bool IsCover { get; set; }

    public virtual Album Album { get; set; } = null!;
}