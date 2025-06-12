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

namespace KintoneDeSql.Responses.Cybozu;

/// <summary>
/// https://cybozu.dev/ja/common/docs/user-api/overview/data-structure/#group
/// </summary>
internal class UserGroupResponse : GroupResponse
{
    // グループに含まれるユーザー情報
}

internal class GroupResponse : ItemValue
{
    //id ID型 グループID
    [JsonPropertyName("id")]
    [ColumnEx("id", Order = 10, TypeName = "TEXT", IsUnique = true)]
    public string Id { get; set; } = string.Empty;

    //code    文字列 グループコード -> base
    //name 文字列 グループ名 -> base

    //description 文字列 メモ
    [JsonPropertyName("description")]
    [ColumnEx("description", Order = 11, TypeName = "TEXT")]
    public string Description { get; set; } = string.Empty;
}
