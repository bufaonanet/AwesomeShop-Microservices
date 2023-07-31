using System.Text.Json;
using System.Text.Json.Serialization;

namespace AwesomeShop.Services.Orders.Infrastructure;

public static class JsonSerializerExtensions
{
    private static readonly JsonSerializerOptions DefaultJsonSerializerOptions = new JsonSerializerOptions
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
    };

    public static string ToJsonString<T>(this T obj)
    {
        return JsonSerializer.Serialize(obj, DefaultJsonSerializerOptions);
    }
}