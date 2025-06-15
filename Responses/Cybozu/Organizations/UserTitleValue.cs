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
using KintoneDeSql.Responses.Cybozu.Users;
using System.Text.Json.Serialization;

namespace KintoneDeSql.Responses.Cybozu.Organizations;

/// <summary>
/// https://cybozu.dev/ja/common/docs/user-api/organizations/get-organizations-users/
/// https://cybozu.dev/ja/common/docs/user-api/overview/data-structure/#user-title
/// </summary>
internal class UserTitleValue: BaseToData
{
    //user User型   ユーザ情報
    [JsonPropertyName("user")]
    [ColumnEx("user", Order = 100, TypeName = "TEXT", IsExtract = true)]
    public UserValue User { get; set; } = new();

    //title   Title型 役職情報
    [JsonPropertyName("title")]
    [ColumnEx("title", Order = 200, TypeName = "TEXT", IsExtract = true)]
    public TitleValue Title { get; set; } = new();
}
