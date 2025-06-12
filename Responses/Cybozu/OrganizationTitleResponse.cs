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
/// https://cybozu.dev/ja/common/docs/user-api/overview/data-structure/#organization-title
/// </summary>
internal class OrganizationTitleResponse:BaseToData
{
    //organization Organization型   組織情報

    [JsonPropertyName("organization")]
    [ColumnEx("organization", Order = 1, IsExtract =true)]
    public OrganizationResponse Organization { get; set; } = new ();

    //title   Title型 役職情報

    [JsonPropertyName("title")]
    [ColumnEx("title", Order = 2, IsExtract = true)]
    public TitleResponse Title { get; set; } = new();
}
