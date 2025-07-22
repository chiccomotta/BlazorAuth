using Blazor.Api.Models.Cosmos;
using Microsoft.Azure.Cosmos;

namespace Blazor.Api.Services;

public interface ICosmosDbService
{
    Task<ItemResponse<T>> AddItemAsync<T>(T item, string partitionKey) where T : EntityBase;
    Task AddItemsAsync<T>(IEnumerable<T> items) where T : EntityBase;
    Task<T?> GetItemAsync<T>(string id, string partitionKey) where T : class;
    Task<IEnumerable<T>> GetItemsAsync<T>(string query) where T : class;

    // Place Repository Methods
    Task<ItemResponse<T>> AddPlaceAsync<T>(T item, string partitionKey) where T : EntityBase;
}