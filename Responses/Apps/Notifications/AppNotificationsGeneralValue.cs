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
using KintoneDeSql.Responses.Commons;
using System.Text.Json.Serialization;

namespace KintoneDeSql.Responses.Apps.Notifications;

/// <summary>
/// https://cybozu.dev/ja/kintone/docs/rest-api/apps/settings/get-general-notification-settings/
/// </summary>
internal class AppNotificationsGeneralValue:BaseToData
{
    //notifications[].includeSubs	真偽値	設定を下位組織に継承するかどうか
    [JsonPropertyName("includeSubs")]
    [ColumnEx("includeSubs", Order = 10, TypeName = "NUMERIC")]
    public bool IncludeSubs { get; set; } = false;

    //notifications[].recordAdded	真偽値	レコード追加で通知するかどうか
    [JsonPropertyName("recordAdded")]
    [ColumnEx("recordAdded", Order = 11, TypeName = "NUMERIC")]
    public bool RecordAdded { get; set; } = false;

    //notifications[].recordEdited	真偽値	レコード編集で通知するかどうか
    [JsonPropertyName("recordEdited")]
    [ColumnEx("recordEdited", Order = 12, TypeName = "NUMERIC")]
    public bool RecordEdited { get; set; } = false;

    //notifications[].commentAdded	真偽値	コメントの書き込みで通知するかどうか
    [JsonPropertyName("commentAdded")]
    [ColumnEx("commentAdded", Order = 13, TypeName = "NUMERIC")]
    public bool CommentAdded { get; set; } = false;

    //notifications[].statusChanged	真偽値	ステータスの更新で通知するかどうか
    [JsonPropertyName("statusChanged")]
    [ColumnEx("statusChanged", Order = 14, TypeName = "NUMERIC")]
    public bool StatusChanged { get; set; } = false;

    //notifications[].fileImported	真偽値	ファイル読み込みで通知するかどうか
    [JsonPropertyName("fileImported")]
    [ColumnEx("fileImported", Order = 15, TypeName = "NUMERIC")]
    public bool FileImported { get; set; } = false;

    //notifications[].entity	オブジェクト	条件通知の設定の対象を表すオブジェクト
    //notifications[].entity.type	文字列	条件通知の設定対象の種類
    //notifications[].entity.code	文字列	条件通知の設定対象のコード
    [JsonPropertyName("entity")]
    [ColumnEx("entity", Order = 50, TypeName = "TEXT", IsExtract = true)]
    public EntityValue Entity { get; set; } = new();
}
