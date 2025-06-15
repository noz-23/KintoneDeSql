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
/// https://cybozu.dev/ja/kintone/docs/rest-api/apps/settings/get-action-settings/
/// </summary>
internal class AppActionRequest : BaseSingleton<AppActionRequest>
{
    private const string _COMMAND = "app/actions.json";
    public async Task<AppActionResponse?> Get(string appId_)
    {
        var query = string.Empty;
        var paramater = JsonSerializer.Serialize(new { app = appId_ });
        var response = await KintoneManager.Instance.KintoneGet<AppActionResponse?>(HttpMethod.Get, _COMMAND, query, paramater);
        //
        if (response != null)
        {
            response.AppId = appId_;
        }
        return response;
    }

    public async Task<AppActionResponse?> Insert(string appId_)
    {
        var response = await Get(appId_);
        if (response != null)
        {
            SQLiteManager.Instance.InsertTable(AppActionResponse.TableName(false), AppActionResponse.ListInsertHeader(true), response.ListInsertValue(true));
            SQLiteManager.Instance.InsertTable(AppActionResponseMapping.TableName(false), AppActionResponseMapping.ListInsertHeader(true), response.ListInsertValueMapping(true));
            SQLiteManager.Instance.InsertTable(AppActionResponseEntity.TableName(false), AppActionResponseEntity.ListInsertHeader(true), response.ListInsertValueEntity(true));
        }
  
        return response;
    }
}
