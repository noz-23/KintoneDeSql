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
//internal class AppNotificationsReminderRequest : BaseSingleton<AppNotificationsReminderRequest>
internal class AppNotificationsReminderRequest : BaseRequest<AppNotificationsReminderRequest, AppNotificationsReminderResponse>
{
    //private const string _COMMAND = "app/notifications/reminder.json";
    //public async Task<AppNotificationsReminderResponseList?> Get(string appId_)
    //{
    //    var query = string.Empty;
    //    var paramater = JsonSerializer.Serialize(new { app = appId_ });
    //    var response = await KintoneManager.Instance.KintoneGet<AppNotificationsReminderResponseList?>(HttpMethod.Get, _COMMAND, query, paramater);
    //    //
    //    if (response != null)
    //    {
    //        response.AppId = appId_;
    //    }
    //    return response;
    //}

    //public async Task<AppNotificationsReminderResponseList?> Insert(string appId_)
    //{
    //    var response = await Get(appId_);
    //    if (response != null)
    //    {
    //        SQLiteManager.Instance.InsertTable(AppNotificationsReminderResponseList.TableName(false), AppNotificationsReminderResponseList.ListInsertHeader(true), response.ListInsertValue(true));
    //    }

    //    return response;
    //}
    protected override string _Command { get; } = "app/notifications/reminder.json";
    public override void Insert(AppNotificationsReminderResponse? response_)
    {
        if (response_ != null)
        {
            SQLiteManager.Instance.InsertTable(AppNotificationsReminderResponse.TableName(false), AppNotificationsReminderResponse.ListInsertHeader(true), response_.ListInsertValue(true));
        }
    }
}
