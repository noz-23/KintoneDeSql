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
using KintoneDeSql.Responses.Cybozu.Organizations;
using System.Text.Json.Serialization;

namespace KintoneDeSql.Responses.Cybozu.Users;

/// <summary>
/// https://cybozu.dev/ja/common/docs/user-api/users/get-users-organizations/
/// https://cybozu.dev/ja/common/docs/user-api/overview/data-structure/#organization-title
/// </summary>
internal class OrganizationTitleValue:BaseToData
{
    //organization Organization型   組織情報

    [JsonPropertyName("organization")]
    [ColumnEx("organization", Order = 100, TypeName = "TEXT", IsExtract =true)]
    public OrganizationValue Organization { get; set; } = new ();

    //title   Title型 役職情報

    [JsonPropertyName("title")]
    [ColumnEx("title", Order = 200, TypeName = "TEXT", IsExtract = true)]
    public TitleValue Title { get; set; } = new();
}
