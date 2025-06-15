/*
 * Kintone De Sql
 * Copyright (c) 2025 noz-23
 *  https://github.com/noz-23/
 * 
 * Licensed under the MIT License 
 * 
 */
using KintoneDeSql.Attributes;
using System.Text.Json.Serialization;

namespace KintoneDeSql.Responses.Cybozu.Users;

/// <summary>
/// https://cybozu.dev/ja/common/docs/user-api/users/get-users/
/// https://cybozu.dev/ja/common/docs/user-api/groups/get-groups-users/
/// https://cybozu.dev/ja/common/docs/user-api/overview/data-structure/#user
/// </summary>
internal class UserValue : UserCustomItemValue
{
    //id ID型 ユーザーID
    [JsonPropertyName("id")]
    [ColumnEx("id", Order = 1, TypeName = "TEXT", IsUnique = true)]
    public string Id { get; set; } = string.Empty;

    //ctime 文字列 作成日時
    [JsonPropertyName("ctime")]
    [ColumnEx("ctime", Order = 11, TypeName = "TEXT")]
    public string CreateTime { get; set; } = string.Empty;

    //mtime 文字列 更新日時
    [JsonPropertyName("mtime")]
    [ColumnEx("mtime", Order = 12, TypeName = "TEXT")]
    public string ModiTime { get; set; } = string.Empty;

    //valid 真偽値 使用可能ユーザーかどうか
    [JsonPropertyName("valid")]
    [ColumnEx("valid", Order = 13, TypeName = "TEXT")]
    public bool Valid { get; set; } = false;

    //surName 文字列 姓
    [JsonPropertyName("surName")]
    [ColumnEx("surName", Order = 14, TypeName = "TEXT")]
    public string SurName { get; set; } = string.Empty;

    //givenName   文字列 名
    [JsonPropertyName("givenName")]
    [ColumnEx("givenName", Order = 15, TypeName = "TEXT")]
    public string GivenName { get; set; } = string.Empty;

    //surNameReading 文字列 よみがな（姓）
    [JsonPropertyName("surNameReading")]
    [ColumnEx("surNameReading", Order = 16, TypeName = "TEXT")]
    public string SurNameReading { get; set; } = string.Empty;

    //givenNameReading 文字列 よみがな（名）
    [JsonPropertyName("givenNameReading")]
    [ColumnEx("givenNameReading", Order = 17, TypeName = "TEXT")]
    public string GivenNameReading { get; set; } = string.Empty;

    //localName 文字列 別言語での表示名
    [JsonPropertyName("localName")]
    [ColumnEx("localName", Order = 18, TypeName = "TEXT")]
    public string LocalName { get; set; } = string.Empty;

    //localNameLocale 文字列	「別言語での表示名」で使用する言語
    [JsonPropertyName("localNameLocale")]
    [ColumnEx("localNameLocale", Order = 19, TypeName = "TEXT")]
    public string LocalNameLocale { get; set; } = string.Empty;

    //timezone    文字列 タイムゾーンのID
    [JsonPropertyName("timezone")]
    [ColumnEx("timezone", Order = 20, TypeName = "TEXT")]
    public string TimeZone { get; set; } = string.Empty;

    //locale 文字列 ロケール
    [JsonPropertyName("locale")]
    [ColumnEx("locale", Order = 21, TypeName = "TEXT")]
    public string Locale { get; set; } = string.Empty;

    //description 文字列 メモ
    [JsonPropertyName("description")]
    [ColumnEx("description", Order = 22, TypeName = "TEXT")]
    public string Description { get; set; } = string.Empty;

    //phone 文字列 電話番号
    [JsonPropertyName("phone")]
    [ColumnEx("phone", Order = 23, TypeName = "TEXT")]
    public string Phone { get; set; } = string.Empty;

    //mobilePhone 文字列 携帯電話番号
    [JsonPropertyName("mobilePhone")]
    [ColumnEx("mobilePhone", Order = 24, TypeName = "TEXT")]
    public string MobilePhone { get; set; } = string.Empty;

    //extensionNumber 文字列 内線番号
    [JsonPropertyName("extensionNumber")]
    [ColumnEx("extensionNumber", Order = 25, TypeName = "TEXT")]
    public string ExtensionNumber { get; set; } = string.Empty;

    //email   文字列 メールアドレス
    [JsonPropertyName("email")]
    [ColumnEx("email", Order = 26, TypeName = "TEXT")]
    public string EMail { get; set; } = string.Empty;

    //callto 文字列 SkypeID
    [JsonPropertyName("callto")]
    [ColumnEx("callto", Order = 27, TypeName = "TEXT")]
    public string CallTo { get; set; } = string.Empty;

    //url 文字列 URL
    [JsonPropertyName("url")]
    [ColumnEx("url", Order = 28, TypeName = "TEXT")]
    public string Url { get; set; } = string.Empty;

    //employeeNumber 文字列 従業員番号
    [JsonPropertyName("employeeNumber")]
    [ColumnEx("employeeNumber", Order = 29, TypeName = "TEXT")]
    public string EmployeeNumber { get; set; } = string.Empty;

    //birthDate   文字列 誕生日
    [JsonPropertyName("birthDate")]
    [ColumnEx("birthDate", Order = 30, TypeName = "TEXT")]
    public string BirthDate { get; set; } = string.Empty;

    //joinDate 文字列 入社日
    [JsonPropertyName("joinDate")]
    [ColumnEx("joinDate", Order = 31, TypeName = "TEXT")]
    public string JoinDate { get; set; } = string.Empty;

    //primaryOrganization 数値  優先する組織
    [JsonPropertyName("primaryOrganization")]
    [ColumnEx("primaryOrganization", Order = 32, TypeName = "TEXT")]
    public string PrimaryOrganization { get; set; } = string.Empty;

    //sortOrder 数値  表示優先度
    [JsonPropertyName("sortOrder")]
    [ColumnEx("sortOrder", Order = 33, TypeName = "TEXT")]
    public string SortOrder { get; set; } = string.Empty;
}
