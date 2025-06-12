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
using KintoneDeSql.Responses.Apis;
using KintoneDeSql.Responses.Apps;
using System.Net.Http;
using System.Text.Json;

namespace KintoneDeSql.Requests.Apps;

/// <summary>
/// https://cybozu.dev/ja/kintone/docs/rest-api/apps/get-apps/
/// </summary>
internal class AppsRequest : BaseSingleton<AppsRequest>
{
    //private const string _COMMAND = "app.json";
    //private const string _COMMAND_S = "apps.json";
    private const string _COMMAND = "apps.json";

    public async Task<ListAppResponse?> Get()
    {
        return await KintoneManager.Instance.KintoneGet<ListAppResponse?>(HttpMethod.Get, _COMMAND);
    }

    //public async Task<AppResponse?> Get(string appId_, string appToken_ = "")
    //{
    //    var query = string.Empty;
    //    var paramater = JsonSerializer.Serialize(new { id= appId_});
    //    return await KintoneManager.Instance.KintoneGet<AppResponse?>(HttpMethod.Get, _COMMAND,query, paramater, appToken_);
    //}
    public async Task<ListAppResponse?> Insert()
    {
        var response = await Get();

        if (response != null)
        {
            SQLiteManager.Instance.InsertTable(ListAppResponse.TableName(false), ListAppResponse.ListInsertHeader(true), response.ListInsertValue(true));
        }

        return response;
    }
}
