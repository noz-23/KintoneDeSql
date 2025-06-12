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

namespace KintoneDeSql.Responses.Commons;

/// <summary>
/// https://cybozu.dev/ja/kintone/docs/rest-api/spaces/get-space/
/// </summary>
internal class SpaceValue : BaseToData
{
    //id	文字列	スペースID
    [JsonPropertyName("id")]
    [ColumnEx("id", Order = 1, TypeName = "TEXT", IsPrimary = true)]
    public string Id { get; set; } = string.Empty;

    //name	文字列	スペース名
    [JsonPropertyName("name")]
    [ColumnEx("name", Order = 2, TypeName = "TEXT")]
    public string Name { get; set; } = string.Empty;
}
