/*
 * Kintone De Sql
 * Copyright (c) 2025 noz-23
 *  https://github.com/noz-23/
 * 
 * Licensed under the MIT License 
 * 
 */
using KintoneDeSql.Attributes;
using System.Text.Json.Serialization;

namespace KintoneDeSql.Responses.Apis;

/// <summary>
/// https://cybozu.dev/ja/kintone/docs/rest-api/apis/get-api-schema/
/// </summary>
internal class ApiSchemaValue : ApiSchemaValueRequired
{
    [JsonPropertyName("type")]
    [ColumnEx("type", Order = 30, TypeName = "TEXT")]
    public string Type { get; set; } = string.Empty;

    [JsonPropertyName("format")]
    [ColumnEx("format", Order = 31, TypeName = "TEXT")]
    public string Format { get; set; } = string.Empty;

    public override string ToString()
    {
        return Format.ToString();
    }
}
