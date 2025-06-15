/*
 * Kintone De Sql
 * Copyright (c) 2025 noz-23
 *  https://github.com/noz-23/
 * 
 * Licensed under the MIT License 
 * 
 */
using KintoneDeSql.Data;
using KintoneDeSql.Managers;
using KintoneDeSql.Responses.Apps.Notifications;
using System.Net.Http;
using System.Text.Json;

namespace KintoneDeSql.Requests.Apps.Notifications;

/// <summary>
/// https://cybozu.dev/ja/kintone/docs/rest-api/apps/settings/get-app-permissions/
/// </summary>
internal class AppNotificationsGeneralRequest : BaseSingleton<AppNotificationsGeneralRequest>
{
    private const string _COMMAND = "app/notifications/general.json";
    public async Task<AppNotificationsGeneralResponse?> Get(string appId_)
    {
        var query = string.Empty;
        var paramater = JsonSerializer.Serialize(new { app = appId_ });
        var response = await KintoneManager.Instance.KintoneGet<AppNotificationsGeneralResponse?>(HttpMethod.Get, _COMMAND, query, paramater);
        //
        if (response != null)
        {
            response.AppId = appId_;
        }
        return response;
    }

    public async Task<AppNotificationsGeneralResponse?> Insert(string appId_)
    {
        var response = await Get(appId_);
        if (response != null)
        {
            SQLiteManager.Instance.InsertTable(AppNotificationsGeneralResponse.TableName(false), AppNotificationsGeneralResponse.ListInsertHeader(true), response.ListInsertValue(true));
        }

        return response;
    }
}
