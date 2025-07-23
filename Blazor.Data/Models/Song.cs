using System.ComponentModel.DataAnnotations;
using Blazor.Data.Models;

public class Song
{
    [Key]
    public int Id { get; set; }
    public string Title { get; set; }
    public int DurationInSeconds { get; set; } 
    public int AlbumId { get; set; } // Chiave esterna per l'album
    public bool IsCover { get; set; }
    public Album? Album { get; set; } // Navigazione inversa
}