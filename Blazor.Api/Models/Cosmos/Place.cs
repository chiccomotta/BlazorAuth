namespace Blazor.Api.Models.Cosmos;

[PartitionKey(nameof(Region))]
public class Place : EntityBase
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Name { get; set; } = string.Empty;
    public IEnumerable<string> Address { get; set; } = [];
    public required string City { get; set; }
    public required string Region { get; set; }
    public int NumberOfPeople { get; set; }
}