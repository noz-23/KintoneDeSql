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
using KintoneDeSql.Responses.Apps.Permissions;
using System.Text.Json.Serialization;

namespace KintoneDeSql.Responses.Apps.Notifications;

/// <summary>
/// https://cybozu.dev/ja/kintone/docs/rest-api/apps/settings/get-per-record-notification-settings/
/// </summary>
internal class AppNotificationsPerRecordValue: BaseToData
{
    //notifications[].filterCond	文字列	レコードの条件
    [JsonPropertyName("filterCond")]
    [ColumnEx("filterCond", Order = 10, TypeName = "TEXT")]
    public string FilterCond { get; set; } = string.Empty;

    //notifications[].title	文字列	通知内容
    [JsonPropertyName("title")]
    [ColumnEx("title", Order = 11, TypeName = "TEXT")]
    public string Title { get; set; } = string.Empty;

    //notifications[].targets	配列	通知先の対象の一覧
    [JsonPropertyName("targets")]
    public List<NotificationValue> ListTarget { get; set; } = new();
}
