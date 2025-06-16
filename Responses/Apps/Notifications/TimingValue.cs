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

namespace KintoneDeSql.Responses.Apps.Notifications;

/// <summary>
/// https://cybozu.dev/ja/kintone/docs/rest-api/apps/settings/get-reminder-notification-settings/
/// </summary>
internal class TimingValue : BaseToData
{
    //notifications[].timing.code	文字列	通知のタイミングの基準日時となるフィールドのフィールドコード
    [JsonPropertyName("code")]
    [ColumnEx("code", Order = 10, TypeName = "TEXT")]
    public string Code { get; set; } = string.Empty;

    //notifications[].timing.daysLater	文字列	基準日時から何日前または何日後に通知するか
    [JsonPropertyName("daysLater")]
    [ColumnEx("daysLater", Order = 11, TypeName = "TEXT")]
    public string DaysLater { get; set; } = string.Empty;

    //notifications[].timing.hoursLater	文字列	基準日時にtiming.daysLaterを足した日時から、何時間後または何時間前に通知するか
    [JsonPropertyName("hoursLater")]
    [ColumnEx("hoursLater", Order = 12, TypeName = "TEXT")]
    public string HoursLater { get; set; } = string.Empty;

    //notifications[].timing.time	文字列	基準日時にtiming.daysLaterを足した日付から、いつ通知するか
    [JsonPropertyName("time")]
    [ColumnEx("time", Order = 13, TypeName = "TEXT")]
    public string Time { get; set; } = string.Empty;

    public override string ToString()
    {
        return Code.ToString();
    }
}
