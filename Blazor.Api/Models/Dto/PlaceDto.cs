namespace Blazor.Api.Models.Dto;

public class PlaceDto
{
    public string Name { get; set; }
    public IEnumerable<string> Address { get; set; }
    public string City { get; set; }
    public string Region { get; set; }
    public int NumberOfPeople { get; set; }
}