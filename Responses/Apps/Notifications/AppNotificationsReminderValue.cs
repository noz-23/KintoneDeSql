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
/// https://cybozu.dev/ja/kintone/docs/rest-api/apps/settings/get-reminder-notification-settings/
/// </summary>
internal class AppNotificationsReminderValue : BaseToData
{
    //notifications[].filterCond	文字列	リマインダーの条件通知を行う条件
    [JsonPropertyName("filterCond")]
    [ColumnEx("filterCond", Order = 10, TypeName = "TEXT")]
    public string FilterCond { get; set; } = string.Empty;

    //notifications[].title	文字列	リマインダーの条件通知で通知される内容
    [JsonPropertyName("title")]
    [ColumnEx("title", Order = 11, TypeName = "TEXT")]
    public string Title { get; set; } = string.Empty;

    //notifications[].timing	オブジェクト	通知のタイミング
    [JsonPropertyName("timing")]
    [ColumnEx("timing", Order = 50, TypeName = "TEXT", IsExtract =true)]
    public TimingValue Timing { get; set; } =new();

    //notifications[].targets	配列	通知先の対象の一覧
    [JsonPropertyName("targets")]
    public List<NotificationValue> ListTarget { get; set; } = new();
    //notifications[].targets[].entity	オブジェクト	通知先の対象を表すオブジェクト
    //notifications[].targets[].entity.type	文字列	通知先の対象の種類
    //notifications[].targets[].entity.code	文字列	通知先の対象のコード
    //notifications[].targets[].includeSubs	真偽値	設定を下位組織に継承するかどうか

}
