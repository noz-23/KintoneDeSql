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

namespace KintoneDeSql.Responses.Cybozu.Users;

/// <summary>
/// https://cybozu.dev/ja/common/docs/user-api/users/get-users-organizations/
/// https://cybozu.dev/ja/common/docs/user-api/overview/data-structure/#title
/// </summary>
internal class TitleValue : BaseToData
{
    //id ID型 役職ID
    [JsonPropertyName("id")]
    [ColumnEx("id", Order = 1, TypeName = "TEXT")]
    public string Id { get; set; } = string.Empty;

    //code    文字列 役職コード
    [JsonPropertyName("code")]
    [ColumnEx("code", Order = 1, TypeName = "TEXT")]
    public string Code { get; set; } = string.Empty;

    //name 文字列 役職名
    [JsonPropertyName("name")]
    [ColumnEx("name", Order = 1, TypeName = "TEXT")]
    public string Name { get; set; } = string.Empty;

    //description 文字列 メモ
    [JsonPropertyName("description")]
    [ColumnEx("description", Order = 1, TypeName = "TEXT")]
    public string Description { get; set; } = string.Empty;
}
