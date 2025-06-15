/*
 * Kintone De Sql
 * Copyright (c) 2025 noz-23
 *  https://github.com/noz-23/
 * 
 * Licensed under the MIT License 
 * 
 */
using KintoneDeSql.Attributes;
using KintoneDeSql.Responses.Records;
using System.Text.Json.Serialization;

namespace KintoneDeSql.Responses.Cybozu.Groups;
internal class UserGroupValue : GroupValue
{
    // グループに含まれるユーザー情報
}

/// <summary>
/// https://cybozu.dev/ja/common/docs/user-api/overview/data-structure/#group
/// </summary>
internal class GroupValue : CodeNameValue
{
    //code    文字列 グループコード
    //name 文字列 グループ名
    // -> base

    //id ID型 グループID
    [JsonPropertyName("id")]
    [ColumnEx("id", Order = 1, TypeName = "TEXT", IsUnique = true)]
    public string Id { get; set; } = string.Empty;

    //description 文字列 メモ
    [JsonPropertyName("description")]
    [ColumnEx("description", Order = 2, TypeName = "TEXT")]
    public string Description { get; set; } = string.Empty;
}
