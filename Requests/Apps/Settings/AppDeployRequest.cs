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
/// https://cybozu.dev/ja/kintone/docs/rest-api/apps/settings/deploy-app-settings/
/// </summary>
internal class AppDeployRequest : BaseSingleton<AppDeployRequest>
{
    private const string _COMMAND = "preview/app/deploy.json";
    public async Task<AppDeployResponse?> Get(IEnumerable<string> listApp_)
    {
        var query = string.Empty;
        var paramater = JsonSerializer.Serialize(new { apps = listApp_ });
        return await KintoneManager.Instance.KintoneGet<AppDeployResponse?>(HttpMethod.Get, _COMMAND, query, paramater);
    }

    public async Task<AppDeployResponse?> Insert(IEnumerable<string> listApp_)
    {
        var response = await Get(listApp_);
        if (response != null)
        {
            SQLiteManager.Instance.InsertTable(AppDeployResponse.TableName(false), AppDeployResponse.ListInsertHeader(true), response.ListInsertValue(true));
        }

        return response;
    }
}
