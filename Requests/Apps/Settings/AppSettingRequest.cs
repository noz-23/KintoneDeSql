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
using KintoneDeSql.Responses.Apps.Settings;
using System.Net.Http;
using System.Text.Json;

namespace KintoneDeSql.Requests.Apps.Settings;
/// <summary>
/// https://cybozu.dev/ja/kintone/docs/rest-api/apps/settings/get-general-notification-settings/
/// </summary>
internal class AppSettingRequest : BaseSingleton<AppSettingRequest>
{
    private const string _COMMAND = "app/settings.json";
    public async Task<AppSettingResponse?> Get(string appId_)
    {
        var query = string.Empty;
        var paramater = JsonSerializer.Serialize(new { app = appId_ });
        var response = await KintoneManager.Instance.KintoneGet<AppSettingResponse?>(HttpMethod.Get, _COMMAND, query, paramater);
        //
        if (response != null)
        {
            response.AppId = appId_;
        }
        return response;
    }

    public async Task<AppSettingResponse?> Insert(string appId_)
    {
        var response = await Get(appId_);
        if (response != null)
        {
            SQLiteManager.Instance.InsertTable(AppSettingResponse.TableName(false), AppSettingResponse.ListInsertHeader(true), response.ListInsertValue(true));
        }

        return response;
    }
}