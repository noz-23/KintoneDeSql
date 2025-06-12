/*
 * Kintone De Sql
 * Copyright (c) 2025 noz-23
 *  https://github.com/noz-23/
 * 
 * Licensed under the MIT License 
 * 
 */
using KintoneDeSql.Attributes;
using KintoneDeSql.Data;
using KintoneDeSql.Responses.Commons;
using System.Text.Json.Serialization;

namespace KintoneDeSql.Responses.Apis;

/// <summary>
/// https://cybozu.dev/ja/kintone/docs/rest-api/apis/get-api-schema/
/// </summary>
internal class ApiJsonResponse : BaseToData
{
    [JsonPropertyName("type")]
    [ColumnEx("type", Order = 10, TypeName = "TEXT")]
    public string Type { get; set; } = string.Empty;

    [JsonPropertyName("format")]
    [ColumnEx("format", Order = 11, TypeName = "TEXT")]
    public string Format { get; set; } = string.Empty;

    [JsonPropertyName("properties")]
    public Dictionary<string, ApiJsonResponse> ListProperty { get; set; } = new ();

    [ColumnEx("key", Order = 1, TypeName = "TEXT")]
    public string PropertiesKey { get; set; } = string.Empty;

    [JsonPropertyName("required")]
    [ColumnEx("required", Order = 20, TypeName = "TEXT")]
    public ListStringResponse ListRequired { get; set; } = new();
}
