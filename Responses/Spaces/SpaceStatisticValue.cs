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

namespace KintoneDeSql.Responses.Spaces;

/// <summary>
/// https://cybozu.dev/ja/kintone/docs/rest-api/spaces/get-spaces-statistics/
/// </summary>
internal class SpaceStatisticValue : BaseToData
{
    // spaces[].id 数値または文字列    スペースID
    [JsonPropertyName("id")]
    [ColumnEx("id", Order = 1, TypeName = "TEXT")]
    public string Id { get; set; } = string.Empty;

    // spaces[].name 文字列 スペース名
    [JsonPropertyName("name")]
    [ColumnEx("name", Order = 2, TypeName = "TEXT")]
    public string Name { get; set; } = string.Empty;

    // spaces[].administratorCount 数値または文字列    スペースの管理者の数
    [JsonPropertyName("administratorCount")]
    [ColumnEx("administratorCount", Order = 3, TypeName = "TEXT")]
    public string AdministratorCount { get; set; } = string.Empty;

    // spaces[].memberCount 数値または文字列    スペースに所属するメンバーの数
    [JsonPropertyName("memberCount")]
    [ColumnEx("memberCount", Order = 3, TypeName = "TEXT")]
    public string MemberCount { get; set; } = string.Empty;

    // spaces[].isPrivate 真偽値 スペースが非公開かどうか
    [JsonPropertyName("isPrivate")]
    [ColumnEx("isPrivate", Order = 4, TypeName = "NUMERIC")]
    public bool IsPrivate { get; set; } = false;

    // spaces[].isGuest 真偽値 ゲストスペースかどうか
    [JsonPropertyName("isGuest")]
    [ColumnEx("isGuest", Order = 5, TypeName = "NUMERIC")]
    public bool IsGuest { get; set; } = false;

    // spaces[].creator オブジェクト  スペースの作成者の情報
    // spaces[].creator.code 文字列 作成者のログイン名
    // spaces[].creator.name 文字列 作成者の表示名
    [JsonPropertyName("creator")]
    [ColumnEx("creator", Order = 101, TypeName = "TEXT", IsExtract = true)]
    public CreatorValue Creator { get; set; } = new();


    // spaces[].createdAt 文字列 スペースの作成日時
    [JsonPropertyName("createdAt")]
    [ColumnEx("createdAt", Order = 100, TypeName = "TEXT")]
    public string CreatedAt { get; set; } = string.Empty;

    // spaces[].modifier オブジェクト  スペース設定の最終更新者の情報
    // spaces[].modifier.code 文字列 更新者のログイン名
    // spaces[].modifier.name 文字列 更新者の表示名
    [JsonPropertyName("modifier")]
    [ColumnEx("modifier", Order = 201, TypeName = "TEXT", IsExtract = true)]
    public ModifierValue Modifier { get; set; } = new();

    // spaces[].modifiedAt 文字列 スペース設定の最終更新日時
    [JsonPropertyName("modifiedAt")]
    [ColumnEx("modifiedAt", Order = 200, TypeName = "TEXT")]
    public string ModifiedAt { get; set; } = string.Empty;
}