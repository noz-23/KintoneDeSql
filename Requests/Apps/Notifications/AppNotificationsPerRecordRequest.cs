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

namespace KintoneDeSql.Requests.Apps;

/// <summary>
/// https://cybozu.dev/ja/kintone/docs/rest-api/apps/settings/get-per-record-notification-settings/
/// </summary>
//internal class AppNotificationsPerRecordRequest : BaseSingleton<AppNotificationsPerRecordRequest>
internal class AppNotificationsPerRecordRequest : BaseRequest<AppNotificationsPerRecordRequest, AppNotificationsPerRecordResponse>
{
    //private const string _COMMAND = "app/notifications/perRecord.json";
    //public async Task<AppNotificationsPerRecordResponse?> Get(string appId_)
    //{
    //    var query = string.Empty;
    //    var paramater = JsonSerializer.Serialize(new { app = appId_ });
    //    var response = await KintoneManager.Instance.KintoneGet<AppNotificationsPerRecordResponse?>(HttpMethod.Get, _COMMAND, query, paramater);
    //    //
    //    if (response != null)
    //    {
    //        response.AppId = appId_;
    //    }
    //    return response;
    //}

    //public async Task<AppNotificationsPerRecordResponse?> Insert(string appId_)
    //{
    //    var response = await Get(appId_);
    //    if (response != null)
    //    {
    //        SQLiteManager.Instance.InsertTable(AppNotificationsPerRecordResponse.TableName(false), AppNotificationsPerRecordResponse.ListInsertHeader(true), response.ListInsertValue(true));
    //    }

    //    return response;
    //}
    protected override string _Command { get; } = "app/notifications/perRecord.json";
    public override void Insert(AppNotificationsPerRecordResponse? response_)
    {
        if (response_ != null)
        {
            SQLiteManager.Instance.InsertTable(AppNotificationsPerRecordResponse.TableName(false), AppNotificationsPerRecordResponse.ListInsertHeader(true), response_.ListInsertValue(true));
        }
    }
}
