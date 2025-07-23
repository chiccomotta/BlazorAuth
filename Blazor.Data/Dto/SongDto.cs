namespace Blazor.Data.Dto;

public partial class SongDto
{
    public string Title { get; set; } = null!;
    public int DurationInSeconds { get; set; }
    public bool IsCover { get; set; }
}