namespace Blazor.Api.Models.Cosmos;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class PartitionKeyAttribute(string propertyName) : Attribute
{
    public string PropertyName { get; } = propertyName;
}