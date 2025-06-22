/*
 * Kintone De Sql
 * Copyright (c) 2025 noz-23
 *  https://github.com/noz-23/
 * 
 * Licensed under the MIT License 
 * 
 */
using KintoneDeSql.Managers;
using KintoneDeSql.Responses.Apps.Notifications;

namespace KintoneDeSql.Requests.Apps.Notifications;

/// <summary>
/// https://cybozu.dev/ja/kintone/docs/rest-api/apps/settings/get-reminder-notification-settings/
/// </summary>
internal class AppNotificationsReminderRequest : BaseRequest<AppNotificationsReminderRequest, AppNotificationsReminderResponse>
{
    protected override string _Command { get; } = "app/notifications/reminder.json";
    public override void Insert(AppNotificationsReminderResponse? response_)
    {
        if (response_ != null)
        {
            SQLiteManager.Instance.InsertTable(AppNotificationsReminderResponse.TableName(false), AppNotificationsReminderResponse.ListInsertHeader(true), response_.ListInsertValue(true));
        }
    }
}
