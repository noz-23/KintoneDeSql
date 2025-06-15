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
using KintoneDeSql.Responses.Records;
using System.Text.Json.Serialization;

namespace KintoneDeSql.Responses.Apps;

/// <summary>
/// https://cybozu.dev/ja/kintone/docs/rest-api/apps/get-apps/
/// </summary>
internal class AppGetValue : BaseToData
{
    //apps[].appId 文字列 アプリID
    [JsonPropertyName("appId")]
    [ColumnEx("appId", Order = 10, TypeName = "TEXT", IsPrimary = true)]
    public string AppId { get; set; } = string.Empty;

    //apps[].code 文字列 アプリコード
    [JsonPropertyName("code")]
    [ColumnEx("code", Order = 11, TypeName = "TEXT")]
    public string Code { get; set; } = string.Empty;

    //apps[].name 文字列 アプリ名
    [JsonPropertyName("name")]
    [ColumnEx("name", Order = 12, TypeName = "TEXT")]
    public string Name { get; set; } = string.Empty;

    //apps[].description 文字列 アプリの説明
    [JsonPropertyName("description")]
    [ColumnEx("description", Order = 13, TypeName = "TEXT")]
    public string Description { get; set; } = string.Empty;

    //apps[].spaceId 文字列 スペースID
    [JsonPropertyName("spaceId")]
    [ColumnEx("spaceId", Order = 14, TypeName = "TEXT")]
    public string SpaceId { get; set; } = string.Empty;

    //apps[].threadId 文字列 スレッドID
    [JsonPropertyName("threadId")]
    [ColumnEx("threadId", Order = 15, TypeName = "TEXT")]
    public string ThreadId { get; set; } = string.Empty;

    //apps[].createdAt 文字列 作成日時
    [JsonPropertyName("createdAt")]
    [ColumnEx("createdAt", Order = 101, TypeName = "TEXT")]
    public string CreatedAt { get; set; } = string.Empty;

    //apps[].creator オブジェクト  作成者情報
    [JsonPropertyName("creator")]
    [ColumnEx("creator", Order = 101, TypeName = "TEXT", IsExtract = true)]
    public CreatorValue Creator { get; set; } = new();

    //apps[].modifiedAt 文字列 更新日時
    [JsonPropertyName("modifiedAt")]
    [ColumnEx("modifiedAt", Order = 200, TypeName = "TEXT")]
    public string ModifiedAt { get; set; } = string.Empty;

    //apps[].modifier オブジェクト  更新者情報
    [JsonPropertyName("modifier")]
    [ColumnEx("modifier", Order = 201, TypeName = "TEXT", IsExtract = true)]
    public ModifierValue Modifier { get; set; } = new();

}

