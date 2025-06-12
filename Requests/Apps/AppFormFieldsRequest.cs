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
using System.Text.Json;

namespace KintoneDeSql.Requests.Apps;

/// <summary>
/// https://cybozu.dev/ja/kintone/docs/rest-api/apps/form/get-form-fields/
/// </summary>
internal class AppFormFieldsRequest: BaseSingleton<AppFormFieldsRequest>
{
    private const string _COMMAND = "app/form/fields.json";
    public async Task<ListAppFormFieldResponse?> Get(string appId_, string appToken_="")
    {
        //var query = $"app={appId_}";
        //var paramater = string.Empty;
        var query = string.Empty;
        var paramater = JsonSerializer.Serialize(new { app = appId_ });
        var response = await KintoneManager.Instance.KintoneGet<ListAppFormFieldResponse?>(HttpMethod.Get, _COMMAND, query, paramater, appToken_);
        if (response != null)
        {
            response.AppId = appId_;
        }
        return response;
    }
    public async Task<ListAppFormFieldResponse?> Insert(string appId_, string appToken_ = "")
    {
        var response = await Get(appId_, appToken_);

        if (response != null)
        {
            SQLiteManager.Instance.InsertTable(ListAppFormFieldResponse.TableName(false), ListAppFormFieldResponse.ListInsertHeader(true), response.ListInsertValue(true));
        }

        return response;
    }
}
