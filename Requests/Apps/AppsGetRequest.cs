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
using KintoneDeSql.Responses.Apps;
using System.Net.Http;

namespace KintoneDeSql.Requests.Apps;

/// <summary>
/// https://cybozu.dev/ja/kintone/docs/rest-api/apps/get-apps/
/// </summary>
internal class AppsGetRequest : BaseSingleton<AppsGetRequest>
{
    private const string _COMMAND = "apps.json";
    public async Task<AppGetResponse?> Get()
    {
        return await KintoneManager.Instance.KintoneGet<AppGetResponse?>(HttpMethod.Get, _COMMAND);
    }
    public async Task<AppGetResponse?> Insert()
    {
        var response = await Get();

        if (response != null)
        {
            SQLiteManager.Instance.InsertTable(AppGetResponse.TableName(false), AppGetResponse.ListInsertHeader(true), response.ListInsertValue(true));
        }

        return response;
    }
}
