/*
 * Kintone De Sql
 * Copyright (c) 2025 noz-23
 *  https://github.com/noz-23/
 * 
 * Licensed under the MIT License 
 * 
 */
using KintoneDeSql.Data;
using KintoneDeSql.Interface;
using KintoneDeSql.Managers;
using KintoneDeSql.Responses.Apps;
using KintoneDeSql.Responses.Plugin;
using System.Net.Http;
using System.Text.Json;

namespace KintoneDeSql.Requests.Plugins;

/// <summary>
/// https://cybozu.dev/ja/kintone/docs/rest-api/plugins/get-plugin-apps/
/// </summary>
internal class PluginAppRequest : BaseSingleton<PluginAppRequest>
{
    private const string _COMMAND = "plugin/apps.json";
    public async Task<PluginAppResponse?> Get(string appId_)
    {
        var query = string.Empty;
        var paramater = JsonSerializer.Serialize(new { app = appId_ });
        return await KintoneManager.Instance.KintoneGet<PluginAppResponse?>(HttpMethod.Get, _COMMAND, query, paramater);
    }

    public async Task<PluginAppResponse?> Insert(string appId_)
    {
        var response = await Get(appId_);
        if (response != null)
        {
            SQLiteManager.Instance.InsertTable(PluginAppResponse.TableName(false), PluginAppResponse.ListInsertHeader(true), response.ListInsertValue(true));
        }

        return response;
    }
}
