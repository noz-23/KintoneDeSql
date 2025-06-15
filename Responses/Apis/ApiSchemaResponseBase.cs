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
using System.Text.Json.Serialization;

namespace KintoneDeSql.Responses.Apis;

/// <summary>
/// https://cybozu.dev/ja/kintone/docs/rest-api/apis/get-apis/
/// </summary>
internal class ApiSchemaResponseBase: BaseToData
{
    //id	文字列	kintone REST APIのAPIのID
    [JsonPropertyName("id")]
    [ColumnEx("id", Order = 1, TypeName = "TEXT", IsUnique = true)]
    public string Id { get; set; } = string.Empty;

    //httpMethod	文字列	APIを実行するためのHTTPメソッド
    [JsonPropertyName("httpMethod")]
    [ColumnEx("httpMethod", Order = 2, TypeName = "TEXT", IsUnique = true)]
    public string HttpMethod { get; set; } = string.Empty;
}
