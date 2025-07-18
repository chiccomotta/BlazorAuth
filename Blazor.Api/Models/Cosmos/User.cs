namespace Blazor.Api.Models.Cosmos;

[PartitionKey(nameof(City))]
public class User : EntityBase
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public required string City { get; set; }
}