using System.Reflection;
using Blazor.Api.Models.Cosmos;

namespace Blazor.Api.Models;

/// <summary>
/// Base class for entities with dynamic partition key resolution using attributes.
/// </summary>
public abstract class EntityBase
{
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
