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

namespace KintoneDeSql.Responses.Cybozu.Organizations;

/// <summary>
/// https://cybozu.dev/ja/common/docs/user-api/organizations/get-organizations/
/// https://cybozu.dev/ja/common/docs/user-api/overview/data-structure/#organization
/// </summary>
internal class OrganizationValue : CodeNameValue
{
    //id ID型 組織ID
    [JsonPropertyName("id")]
    [ColumnEx("id", Order = 10, TypeName = "TEXT", IsUnique = true)]
    public string Id { get; set; } = string.Empty;

    //code    文字列 組織コード -> base
    //name 文字列 組織名 -> base

    //localName   文字列 別言語での表示名
    [JsonPropertyName("localName")]
    [ColumnEx("localName", Order = 11, TypeName = "TEXT")]
    public string LocalName { get; set; } = string.Empty;

    //localNameLocale 文字列	「別言語での表示名」で使用する言語
    [JsonPropertyName("localNameLocale")]
    [ColumnEx("localNameLocale", Order = 12, TypeName = "TEXT")]
    public string LocalNameLocale { get; set; } = string.Empty;

    //parentCode  文字列 親組織のコード
    [JsonPropertyName("parentCode")]
    [ColumnEx("parentCode", Order = 13, TypeName = "TEXT")]
    public string ParentCode { get; set; } = string.Empty;


    //description 文字列 メモ
    [JsonPropertyName("description")]
    [ColumnEx("description", Order = 14, TypeName = "TEXT")]
    public string Description { get; set; } = string.Empty;
}
