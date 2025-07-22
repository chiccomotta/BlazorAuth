using System.ComponentModel.DataAnnotations;

public class Song
{
    [Key]
    public int Id { get; set; }
    public string Title { get; set; }
    public int DurationInSeconds { get; set; } // Durata in secondi
    public int AlbumId { get; set; } // Chiave esterna per l'album
    public bool IsCover { get; set; }
    public Album Album { get; set; } // Navigazione inversa
}