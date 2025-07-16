using Microsoft.Azure.Cosmos;

namespace Blazor.Api.Services;

public interface ICosmosDbService
{
    Task<ItemResponse<T>> AddItemAsync<T>(T item, string partitionKey) where T : class;
    Task<T?> GetItemAsync<T>(string id, string partitionKey) where T : class;
    Task<IEnumerable<T>> GetItemsAsync<T>(string query) where T : class;
}