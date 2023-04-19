using EShopTI.Product.Common.Events;
using EShopTI.Product.Common.Events.Category;
using EShopTI.Product.Common.Events.Product;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EShopTI.Product.Query.Infrastructure.Converters;

public class EventJsonConverter : JsonConverter<BaseEvent>
{
    public override bool CanConvert(Type type)
    {
        return type.IsAssignableFrom(typeof(BaseEvent));
    }

    public override BaseEvent Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (!JsonDocument.TryParseValue(ref reader, out var doc))
        {
            throw new JsonException($"Failed to parse {nameof(JsonDocument)}");
        }

        if (!doc.RootElement.TryGetProperty("Type", out var type))
        {
            throw new JsonException("Could not detect the Type discriminator property!");
        }

        var typeDiscriminator = type.GetString();
        var json = doc.RootElement.GetRawText();

        return typeDiscriminator switch
        {
            nameof(CategoryCreateEvent) => JsonSerializer.Deserialize<CategoryCreateEvent>(json, options),
            nameof(CategoryEditEvent) => JsonSerializer.Deserialize<CategoryEditEvent>(json, options),
            nameof(CategoryRemoveEvent) => JsonSerializer.Deserialize<CategoryRemoveEvent>(json, options),
            nameof(ProductCreateEvent) => JsonSerializer.Deserialize<ProductCreateEvent>(json, options),
            nameof(ProductEditEvent) => JsonSerializer.Deserialize<ProductEditEvent>(json, options),
            nameof(ProductRemoveEvent) => JsonSerializer.Deserialize<ProductRemoveEvent>(json, options),
            nameof(ProductVariantCreateEvent) => JsonSerializer.Deserialize<ProductVariantCreateEvent>(json, options),
            nameof(ProductVariantEditEvent) => JsonSerializer.Deserialize<ProductVariantEditEvent>(json, options),
            nameof(ProductVariantRemoveEvent) => JsonSerializer.Deserialize<ProductVariantRemoveEvent>(json, options),
            
            _ => throw new JsonException($"{typeDiscriminator} is not supported yet!")
        };
    }

    public override void Write(Utf8JsonWriter writer, BaseEvent value, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }
}