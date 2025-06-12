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
internal class RecordsRequest : BaseSingleton<RecordsRequest>
{    public WaitWindow.ProgressCountCallBack? ProgresssBarCount { get; set; } = null;

    private const string _COMMAND = "records.json";
    public  async Task<ListRecordResponse> Get(string appId_, string appToken_="")
    {
        var rtn = new ListRecordResponse();

        var offset = 0;
        var count = 0;
        var limit = KintoneManager.RECORD_LIMIT;

        do
        {
            //var query = $"app={appId_}&query=limit {limit} offset {offset}&totalCount=true";
            //var paramater = string.Empty;
            var query = string.Empty;
            var paramater = JsonSerializer.Serialize(new { app = appId_, query = $"limit { limit} offset {offset}", totalCount = true });

            var response = await KintoneManager.Instance.KintoneGet<ListRecordResponse?>(HttpMethod.Get, _COMMAND, query, paramater);
            if (response == null)
            {
                break;
            }
            response.AppId = appId_;
            count = response.ListRecord.Count;
            //
            var totalCount = Convert.ToInt32(response.TotalCount);

            if (offset == 0 && count == limit)
            {
                var apiCount = totalCount / limit;

                if (MessageBox.Show($"Get All {totalCount} records?\nUse Api Count {apiCount} times", "Attention", MessageBoxButton.YesNo) == MessageBoxResult.No)
                {
                    return response;
                }
            }

            if (totalCount == 0)
            {
                break;
            }
            //

            offset += count;
        } while (count == limit);
        rtn.AppId = appId_;

        return rtn;
    }
    public async Task<int> Insert(string appId_, string appToken_ = "")
    {
        LogFile.Instance.WriteLine($"START  Insert Record[{appId_.ToString()}]");
        //
        var offset = 0;
        var count = 0;
        var limit = KintoneManager.RECORD_LIMIT;
        var lastId = "0";
        // レコードは数が多いため分割してInsert
        do
        {
            // 方法1：レコードIDを利用する方法
            // https://cybozu.dev/ja/id/7b342fa8f1aa22a1bb9cca4a/#use-id
            // https://cybozu.dev/ja/kintone/tips/best-practices/data-acquisition-operation/kintone-get-records-with-offset-limitation/#use-id

            //var query = $"app={appId_}&query=limit {limit} offset {offset}&totalCount=true";
            //var paramater = string.Empty;
            var query = string.Empty;
            //var paramater = JsonSerializer.Serialize(new { app = appId_, query = $"limit {limit} offset {offset}", totalCount = true });
            var paramater = JsonSerializer.Serialize(new { app = appId_, query = $"{Resource.COLUMN_MAIN_TABLE_ID} > {lastId} order by {Resource.COLUMN_MAIN_TABLE_ID} asc limit {limit}", totalCount = true });

            var response = await KintoneManager.Instance.KintoneGet<ListRecordResponse?>(HttpMethod.Get, _COMMAND, query, paramater);
            if (response == null)
            {
                break;
            }
            response.AppId = appId_;
            count = response.ListRecord.Count;

            var lastData = response.ListRecord.Last();
            if (lastData != null)
            {
                if (lastData.TryGetValue(Resource.COLUMN_MAIN_TABLE_ID, out var val))
                {
                    lastId = val. Value?.ToString()??"0";
                    LogFile.Instance.WriteLine($"LAST ID[{lastId}]");
                }
            }
            //
            var totalCount = Convert.ToInt32(response.TotalCount);

            if (offset == 0 && count == limit)
            {
                var apiCount = totalCount / limit;

                if (MessageBox.Show($"Get All {totalCount} records?\nUse Api Count {apiCount} times", "Attention", MessageBoxButton.YesNo) == MessageBoxResult.No)
                {
                    LogFile.Instance.WriteLine($"CANCEL Insert Record[{appId_.ToString()}]");
                    return offset;
                }
                //LogFile.Instance.WriteLine($"TotalCount[{totalCount}]");
                ProgresssBarCount?.Invoke(offset, totalCount, $"{SettingManager.Instance.TableName(appId_)} Records");
            }

            if (totalCount == 0)
            {
                break;
            }
            //
            SQLiteManager.Instance.InsertTable(response.TableName(true), response.ListInsertHeader(true), response.ListInsertValue(true));
            foreach (var subTable in response.ListSubTable())
            {
                SQLiteManager.Instance.DeleteTable(subTable.TableNameBase(false), $"'{Resource.COLUMN_APP_ID}'={appId_}");
                SQLiteManager.Instance.InsertTable(subTable.TableNameBase(false), subTable.ListInsertHeaderBase(true), subTable.ListInsertValueBase(true));
            }

            offset += count;
            ProgresssBarCount?.Invoke(offset);
        } while (count == limit);


        LogFile.Instance.WriteLine($"END    Insert Record[{appId_.ToString()}]");
        return offset;
    }

}
