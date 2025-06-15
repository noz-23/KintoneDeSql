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
using KintoneDeSql.Properties;
using KintoneDeSql.Responses.Apps;
using KintoneDeSql.Responses.Records;
using KintoneDeSql.Windows;
using System.Diagnostics.Metrics;
using System.Net.Http;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace KintoneDeSql.Requests.Records;

/// <summary>
/// https://cybozu.dev/ja/kintone/docs/rest-api/records/get-records/
/// offsetの制限値を考慮したkintoneのレコード一括取得について
/// https://cybozu.dev/ja/kintone/tips/best-practices/data-acquisition-operation/kintone-get-records-with-offset-limitation/
/// </summary>
internal class RecordRequest : BaseSingleton<RecordRequest>
{   
    private const string _COMMAND = "records.json";
    public async Task<RecordResponse?> Get(string appId_, string lastId_, int limit_= KintoneManager.RECORD_LIMIT, string appToken_ = "")
    {
        // 方法1：レコードIDを利用する方法
        // https://cybozu.dev/ja/id/7b342fa8f1aa22a1bb9cca4a/#use-id
        // https://cybozu.dev/ja/kintone/tips/best-practices/data-acquisition-operation/kintone-get-records-with-offset-limitation/#use-id
        var query = string.Empty;
        var paramater = JsonSerializer.Serialize(new { app = appId_, query = $"{Resource.COLUMN_MAIN_TABLE_ID} > {lastId_} order by {Resource.COLUMN_MAIN_TABLE_ID} asc limit {limit_}", totalCount = true });
        var response = await KintoneManager.Instance.KintoneGet<RecordResponse?>(HttpMethod.Get, _COMMAND, query, paramater);
        if (response != null)
        {
            response.AppId = appId_;
        }
        return response;

    }
    public async Task<RecordResponse?> Insert(string appId_, string lastId_, int limit_ = KintoneManager.RECORD_LIMIT, string appToken_ = "")
    {
        LogFile.Instance.WriteLine($"START  Insert Record[{appId_.ToString()}]");

        var response = await Get(appId_, lastId_, limit_, appToken_);
        if (response != null)
        {
            SQLiteManager.Instance.InsertTable(response.TableName(true), response.ListInsertHeader(true), response.ListInsertValue(true));
            foreach (var subTable in response.ListSubTable())
            {
                SQLiteManager.Instance.DeleteTable(subTable.SubTableName(false), $"'{Resource.COLUMN_APP_ID}'={appId_}");
                SQLiteManager.Instance.InsertTable(subTable.SubTableName(false), subTable.ListSubInsertHeader(true), subTable.ListSubValue(true));
            }
        }

        return response;
    }

}
