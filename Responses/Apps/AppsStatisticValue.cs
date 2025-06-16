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
using KintoneDeSql.Interface;
using KintoneDeSql.Responses.Commons;
using KintoneDeSql.Responses.Records;
using System.Text.Json.Serialization;

namespace KintoneDeSql.Responses.Apps;

/// <summary>
/// https://cybozu.dev/ja/kintone/docs/rest-api/apps/get-apps-statistics/
/// </summary>
internal class AppsStatisticValue : BaseToData
{
    //apps[].id	数値または文字列	アプリID
    [JsonPropertyName("id")]
    [ColumnEx("id", Order = 1, TypeName = "TEXT", IsPrimary = true)]
    public string AppId { get; set; } = string.Empty;

    //apps[].name	文字列	アプリ名
    [JsonPropertyName("name")]
    [ColumnEx("name", Order = 2, TypeName = "TEXT")]
    public string Name { get; set; } = string.Empty;

    //apps[].space	オブジェクト	所属スペースの情報
    //apps[].space.id	数値または文字列	スペースID
    //apps[].space.name	文字列	スペース名
    [JsonPropertyName("space")]
    [ColumnEx("space", Order = 10, TypeName = "TEXT", IsExtract = true)]
    public IdNameValue Space { get; set; } = new();
   
    //apps[].appGroup	文字列	アプリグループ
    [JsonPropertyName("appGroup")]
    [ColumnEx("appGroup", Order = 20, TypeName = "TEXT")]
    public string AppGroup { get; set; } = string.Empty;

    //apps[].status	文字列	アプリのステータス
    [JsonPropertyName("status")]
    [ColumnEx("status", Order = 21, TypeName = "TEXT")]
    public string Status { get; set; } = string.Empty;

    //apps[].recordUpdatedAt	文字列	最後に追加または編集されたレコードの日付
    [JsonPropertyName("recordUpdatedAt")]
    [ColumnEx("recordUpdatedAt", Order = 22, TypeName = "TEXT")]
    public string RecordUpdatedAt { get; set; } = string.Empty;

    //apps[].recordCount	数値または文字列	アプリ内のレコード数
    [JsonPropertyName("recordCount")]
    [ColumnEx("recordCount", Order = 23, TypeName = "TEXT")]
    public string RecordCount { get; set; } = string.Empty;

    //apps[].fieldCount	数値または文字列	アプリ内のフィールド数
    [JsonPropertyName("fieldCount")]
    [ColumnEx("fieldCount", Order = 24, TypeName = "TEXT")]
    public string FieldCount { get; set; } = string.Empty;

    //apps[].dailyRequestCount	数値または文字列	1日のAPIリクエスト数
    [JsonPropertyName("dailyRequestCount")]
    [ColumnEx("dailyRequestCount", Order = 25, TypeName = "TEXT")]
    public string DailyRequestCount { get; set; } = string.Empty;

    //apps[].storageUsage	数値または文字列	添付ファイルフィールドに添付されているファイルの合計サイズ
    [JsonPropertyName("storageUsage")]
    [ColumnEx("storageUsage", Order = 26, TypeName = "TEXT")]
    public string StorageUsage { get; set; } = string.Empty;

    //apps[].customized	真偽値	アプリをカスタマイズしているかどうか
    [JsonPropertyName("customized")]
    [ColumnEx("customized", Order = 27, TypeName = "NUMERIC")]
    public bool Customized { get; set; } = false;

    //apps[].creator	オブジェクト	作成者の情報
    //apps[].creator.code	文字列	作成者のログイン名
    //apps[].creator.name	文字列	作成者の表示名
    [JsonPropertyName("creator")]
    [ColumnEx("creator", Order = 101, TypeName = "TEXT", IsExtract = true)]
    public CreatorValue Creator { get; set; } = new();

    //apps[].createdAt	文字列	作成日時
    [JsonPropertyName("createdAt")]
    [ColumnEx("createdAt", Order = 100, TypeName = "TEXT")]
    public string CreatedAt { get; set; } = string.Empty;

    //apps[].modifier	オブジェクト	アプリ設定の最終更新者の情報
    //apps[].modifier.code	文字列	更新者のログイン名
    //apps[].modifier.name	文字列	更新者の表示名
    [JsonPropertyName("modifier")]
    [ColumnEx("modifier", Order = 201, TypeName = "TEXT", IsExtract = true)]
    public ModifierValue Modifier { get; set; } = new();

    //apps[].modifiedAt	文字列	アプリ設定の最終更新日時
    [JsonPropertyName("modifiedAt")]
    [ColumnEx("modifiedAt", Order = 200, TypeName = "TEXT")]
    public string ModifiedAt { get; set; } = string.Empty;
    public override string ToString()
    {
        return AppId.ToString();
    }
}
