/*
 * Kintone De Sql
 * Copyright (c) 2025 noz-23
 *  https://github.com/noz-23/
 * 
 * Licensed under the MIT License 
 * 
 */
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;

namespace KintoneDeSql.Responses;

internal class ValueConvert : JsonConverter<string>
{
    /// <summary>
    /// 一旦文字列(JSON)に戻す
    /// </summary>
    /// <param name="reader_"></param>
    /// <param name="typeToConvert_"></param>
    /// <param name="options_"></param>
    /// <returns></returns>
    public override string Read(ref Utf8JsonReader reader_, Type typeToConvert_, JsonSerializerOptions options_)
    {
        // Implement the logic to read and convert JSON to an object.  
        // For now, returning a placeholder value.  
        using (var doc = JsonDocument.ParseValue(ref reader_))
        {
            return JsonSerializer.Serialize(doc.RootElement, new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
                WriteIndented = true
            });
        }
    }
    public override void Write(Utf8JsonWriter writer_, string value_, JsonSerializerOptions options_)
    {
        // Implement the logic to write an object as JSON.  
        // For now, writing a placeholder value.  
        JsonSerializer.Serialize(writer_, value_, options_);
    }
}
