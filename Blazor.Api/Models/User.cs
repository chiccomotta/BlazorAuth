using Blazor.Api.Models.Cosmos;

namespace Blazor.Api.Models;

public class User : IPartitionKey
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public string City { get; set; }
    public string PartitionKey
    {
        get => City;
        set => City = value;
    }
}