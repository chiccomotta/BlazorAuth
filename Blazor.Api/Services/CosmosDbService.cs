using Blazor.Api.Models.Cosmos;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Options;

namespace Blazor.Api.Services;

public class CosmosDbService : ICosmosDbService
{
    private readonly CosmosClient _cosmosClient;
    private readonly Container _container;

    public CosmosDbService(IOptions<CosmosDbSettings> options)
    {
        var settings = options.Value;
        _cosmosClient = new CosmosClient(settings.Account, settings.Key);
        _container = _cosmosClient.GetContainer(settings.DatabaseName, settings.ContainerName);
    }

    public async Task<ItemResponse<T>> AddItemAsync<T>(T item, string partitionKey) where T : class
    {
        return await _container.CreateItemAsync(item, new PartitionKey(partitionKey));
    }

    public async Task<T?> GetItemAsync<T>(string id, string partitionKey) where T : class
    {
        try
        {
            var response = await _container.ReadItemAsync<T>(id, new PartitionKey(partitionKey));
            return response.Resource;
        }
        catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return null;
        }
    }

    public async Task<IEnumerable<T>> GetItemsAsync<T>(string query) where T : class
    {
        var queryDef = new QueryDefinition(query);
        var resultSet = _container.GetItemQueryIterator<T>(queryDef);
        List<T> results = new();

        while (resultSet.HasMoreResults)
        {
            var response = await resultSet.ReadNextAsync();
            results.AddRange(response);
        }

        return results;
    }
}