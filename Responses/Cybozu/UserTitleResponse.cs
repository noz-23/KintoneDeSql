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

namespace KintoneDeSql.Responses.Cybozu;

/// <summary>
/// https://cybozu.dev/ja/common/docs/user-api/overview/data-structure/#user-title
/// </summary>
internal class UserTitleRespons: BaseToData
{
    //user User型   ユーザ情報
    [JsonPropertyName("user")]
    [ColumnEx("user", Order = 1, IsExtract = true)]
    public UserResponse User { get; set; } = new();

    //title   Title型 役職情報
    [JsonPropertyName("title")]
    [ColumnEx("title", Order = 2, IsExtract = true)]
    public TitleResponse Title { get; set; } = new();
}
