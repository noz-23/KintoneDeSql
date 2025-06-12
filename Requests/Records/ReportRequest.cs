/*
 * Kintone De Sql
 * Copyright (c) 2025 noz-23
 *  https://github.com/noz-23/
 * 
 * Licensed under the MIT License 
 * 
 */
using KintoneDeSql.Data;
using KintoneDeSql.Files;
using KintoneDeSql.Managers;
using KintoneDeSql.Responses.Apps;
using KintoneDeSql.Responses.Records;
using System.Net.Http;
using System.Text.Json;

namespace KintoneDeSql.Requests.Apps;

/// <summary>
/// https://cybozu.dev/ja/kintone/docs/rest-api/apps/report/get-graph-settings/
/// </summary>
internal class ReportRequest : BaseSingleton<ReportRequest>
{
    private const string _COMMAND = "app/reports.json";
    public async Task<ListReportResponse?> Get(string appId_)
    {
        var query = string.Empty;
        var paramater = JsonSerializer.Serialize(new { app = appId_ });
        //var query = $"app={appId_}";
        //var paramater = string.Empty;
        var response = await KintoneManager.Instance.KintoneGet<ListReportResponse?>(HttpMethod.Get, _COMMAND, query, paramater);
        if (response != null)
        {
            response.AppId = appId_;
        }
        return response;
    }

    public async Task<ListReportResponse?> Insert(string appId_)
    {
        var response = await Get(appId_);
        if (response != null)
        {
            SQLiteManager.Instance.InsertTable(ListReportResponse.TableName(false), ListReportResponse.ListInsertHeader(true), response.ListInsertValue(true));
        }
        return response;
    }
}
