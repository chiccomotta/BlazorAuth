using Blazor.Data.Dto;
using Blazor.Data.Models;

namespace Blazor.Data.Extensions;

public static class SongDtoExtensions
{
    public static List<Song> ToSongEntities(this IEnumerable<SongDto> songs)
    {
        return songs.Select(dto => new Song
        {
            Title = dto.Title,
            DurationInSeconds = dto.DurationInSeconds,
            IsCover = dto.IsCover
        }).ToList();
    }
}