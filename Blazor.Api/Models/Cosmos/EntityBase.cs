using System.Reflection;

namespace Blazor.Api.Models.Cosmos;

/// <summary>
/// Base class for entities with dynamic partition key resolution using attributes.
/// </summary>
public abstract class EntityBase
{ 
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public virtual string PartitionKey
    {
        get
        {
            var type = GetType();
            var attribute = type.GetCustomAttribute<PartitionKeyAttribute>();
     
            if (attribute != null)
            {
                var targetProperty = type.GetProperty(attribute.PropertyName);
                if (targetProperty != null)
                {
                    return targetProperty.GetValue(this)?.ToString();
                }
            }
            
            throw new InvalidOperationException("PartitionKey attribute is not configured correctly.");
        }
    }
}
