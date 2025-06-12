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
/// https://cybozu.dev/ja/kintone/docs/rest-api/apps/get-app-admin-notes/
/// </summary>
internal class AdminNotesRequest : BaseSingleton<AdminNotesRequest>
{
    private const string _COMMAND = "app/adminNotes.json";
    public async Task<AdminNotesResponse?> Get(string appId_)
    {
        var query = string.Empty;
        var paramater = JsonSerializer.Serialize(new { app = appId_ });
        var response = await KintoneManager.Instance.KintoneGet<AdminNotesResponse?>(HttpMethod.Get, _COMMAND, query, paramater);

        if (response != null)
        {
            response.AppId = appId_;
        }
        return response;
    }

    public async Task<AdminNotesResponse?> Insert(string appId_)
    {
        var response = await Get(appId_);
        if (response != null)
        {
            SQLiteManager.Instance.InsertTable(AdminNotesResponse.TableName(false), AdminNotesResponse.ListInsertHeader(true), response.ListInsertValue(true));
        }

        return response;
    }
}
