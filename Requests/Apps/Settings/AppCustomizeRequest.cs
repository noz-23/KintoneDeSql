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
/// https://cybozu.dev/ja/kintone/docs/rest-api/apps/settings/get-customization/
/// </summary>
internal class AppCustomizeRequest : BaseSingleton<AppCustomizeRequest>
{
    private const string _COMMAND = "app/customize.json";
    public async Task<AppCustomizeResponse?> Get(string appId_)
    {
        var query = string.Empty;
        var paramater = JsonSerializer.Serialize(new { app = appId_ });
        var response = await KintoneManager.Instance.KintoneGet<AppCustomizeResponse?>(HttpMethod.Get, _COMMAND, query, paramater);
        //
        if (response != null)
        {
            response.AppId = appId_;
        }
        return response;
    }

    public async Task<AppCustomizeResponse?> Insert(string appId_)
    {
        var response = await Get(appId_);
        if (response != null)
        {
            SQLiteManager.Instance.InsertTable(AppCustomizeResponse.TableName(false), AppCustomizeResponse.ListInsertHeader(true), response.ListInsertValue(true));
        }

        return response;
    }
}
