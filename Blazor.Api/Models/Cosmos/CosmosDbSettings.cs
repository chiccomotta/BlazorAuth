namespace Blazor.Api.Models.Cosmos;

public class CosmosDbSettings
{
    public string Account { get; set; } = null!;
    public string Key { get; set; } = null!;
    public string DatabaseName { get; set; } = null!;
    public string ContainerName { get; set; } = null!;
}