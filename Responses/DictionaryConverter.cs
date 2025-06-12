using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using KintoneDeSql.Files;
using System.Reflection.PortableExecutable;

namespace KintoneDeSql.Responses;

/// <summary>
/// https://josef.codes/custom-dictionary-string-object-jsonconverter-for-system-text-json/
/// </summary>
/// <typeparam name="T"></typeparam>
internal class DictionaryConverter :JsonConverter<Dictionary<string, object>>
{
    public override bool CanConvert(Type typeToConvert_)
    {
        return typeToConvert_ == typeof(Dictionary<string, object>)
            || typeToConvert_ == typeof(Dictionary<string, object?>);
    }

    public override Dictionary<string, object?> Read(ref Utf8JsonReader reader_, Type typeToConvert_, JsonSerializerOptions options_)
    {
        if (reader_.TokenType != JsonTokenType.StartObject)
        {
            throw new JsonException($"JsonTokenType was of type {reader_.TokenType}, only objects are supported");
        }

        var dictionary = new Dictionary<string, object?>();
        while (reader_.Read())
        {
            if (reader_.TokenType == JsonTokenType.EndObject)
            {
                return dictionary;
            }

            if (reader_.TokenType != JsonTokenType.PropertyName)
            {
                throw new JsonException("JsonTokenType was not PropertyName");
            }

            var propertyName = reader_.GetString();

            if (string.IsNullOrWhiteSpace(propertyName))
            {
                throw new JsonException("Failed to get property name");
            }

            reader_.Read();

            dictionary.Add(propertyName!, ExtractValue(ref reader_, options_));
        }

        return dictionary;
    }

    public override void Write( Utf8JsonWriter writer_, Dictionary<string, object?> value_, JsonSerializerOptions options_)
    {
        // We don't need any custom serialization logic for writing the json.
        // Ideally, this method should not be called at all. It's only called if you
        // supply JsonSerializerOptions that contains this JsonConverter in it's Converters list.
        // Don't do that, you will lose performance because of the cast needed below.
        // Cast to avoid infinite loop: https://github.com/dotnet/docs/issues/19268
        JsonSerializer.Serialize(writer_, (IDictionary<string, object?>)value_, options_);
    }

    private object? ExtractValue(ref Utf8JsonReader reader_, JsonSerializerOptions options_)
    {
        switch (reader_.TokenType)
        {
            case JsonTokenType.String:
                if (reader_.TryGetDateTime(out var date))
                {
                    return date;
                }
                return reader_.GetString();
            case JsonTokenType.False:
                return false;
            case JsonTokenType.True:
                return true;
            case JsonTokenType.Null:
                return null;
            case JsonTokenType.Number:
                if (reader_.TryGetInt64(out var result))
                {
                    return result;
                }
                return reader_.GetDecimal();
            case JsonTokenType.StartObject:
                return Read(ref reader_, null!, options_);
            case JsonTokenType.StartArray:
                var list = new List<object?>();
                while (reader_.Read() && reader_.TokenType != JsonTokenType.EndArray)
                {
                    list.Add(ExtractValue(ref reader_, options_));
                }
                return list;
            default:
                throw new JsonException($"'{reader_.TokenType}' is not supported");
        }
    }
}
