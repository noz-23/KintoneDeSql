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
using KintoneDeSql.Extensions;
using KintoneDeSql.Interface;
using KintoneDeSql.Managers;
using KintoneDeSql.Responses.Apps.Permissions;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace KintoneDeSql.Responses.Apps.Notifications;

/// <summary>
/// https://cybozu.dev/ja/kintone/docs/rest-api/apps/settings/get-reminder-notification-settings/
/// </summary>
[Table($"{SQLiteManager.SUB_DATABASE}.appNotificationsReminder")]
internal class AppNotificationsReminderResponse:BaseToData,ISqlTable,IAppTableId
{
    //notifications	配列	リマインダーの条件通知の設定
    [JsonPropertyName("notifications")]
    public List<AppNotificationsReminderValue> ListNotification { get; set; } = new();

    //revision	文字列	アプリ設定のリビジョン番号
    [JsonPropertyName("revision")]
    [ColumnEx("revision", Order = 2, TypeName = "TEXT")]
    public string Revision { get; set; } = string.Empty;

    #region NoJson
    /// <summary>
    /// 上位のアプリ ID
    /// </summary>
    [ColumnEx("appId", Order = 1, TypeName = "TEXT")]
    public string AppId { get; set; } = string.Empty;
    //
    #endregion

    #region ISqlTable
    public static string TableName(bool withCamma_) => typeof(AppNotificationsReminderResponse).TableName(withCamma_);
    public static IEnumerable<string> ListCreateHeader(bool withCamma_) => MemberInfoExtension.ListCreateHeader(_listColumn(), withCamma_);
    public static IEnumerable<string> ListInsertHeader(bool withCamma_) => MemberInfoExtension.ListInsertHeader(_listColumn(), withCamma_);
    private static IEnumerable<ColumnData> _listColumn()
    {
        const int _SORT = 100;
        var rtn = typeof(AppNotificationsReminderResponse).ListColumn();
        rtn.AddRange(typeof(AppNotificationsReminderValue).ListColumn(string.Empty, _SORT));
        rtn.AddRange(typeof(NotificationValue).ListColumn(string.Empty, _SORT+ _SORT));
        return rtn;
    }

    public IEnumerable<IEnumerable<string>> ListInsertValue(bool withCamma_)
    {
        var rtn = new List<IList<string>>();

        foreach (var notif in ListNotification)
        {
            foreach (var target in notif.ListTarget)
            {
                var list = ListValue(withCamma_);
                list.AddRange(notif.ListValue(withCamma_));
                list.AddRange(target.ListValue(withCamma_));
                rtn.Add(list);
            }
        }

        return rtn;
    }
    #endregion
}
