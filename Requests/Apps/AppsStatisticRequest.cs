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

namespace KintoneDeSql.Requests.Spaces;

/// <summary>
/// https://cybozu.dev/ja/kintone/docs/rest-api/spaces/get-spaces-statistics/
/// </summary>
internal class AppsStatisticRequest : BaseSingleton<AppsStatisticRequest>
{
    private const string _COMMAND = "apps/statistics.json";
    public async Task<AppsStatisticResponse?> Get(int offset_, int limit_ = KintoneManager.CYBOZU_LIMIT)
    {
        //var rtn = new AppsStatisticResponseList();

        //var offset = 0;
        //var count = 0;
        //var limit = KintoneManager.CYBOZU_LIMIT;

        //do
        //{
        //    var query = $"query=limit {limit} offset {offset}";
        //    var paramater = string.Empty;
        //    var response = await KintoneManager.Instance.KintoneGet<AppsStatisticResponseList?>(HttpMethod.Get, _COMMAND, query, paramater);
        //    if (response == null)
        //    {
        //        break;
        //    }
        //    //LogFile.Instance.WriteLine($"{response.ListSpace.Count}");
        //    //
        //    rtn.ListApp.AddRange(response.ListApp);
        //    //

        //    count = response.ListApp.Count;

        //    offset += count;
        //} while (count == limit);
        var query = string.Empty;
        var paramater = JsonSerializer.Serialize(new { limit = limit_, offset = offset_ });

        return await KintoneManager.Instance.KintoneGet<AppsStatisticResponse?>(HttpMethod.Get, _COMMAND, query, paramater);
    }
    public async Task<AppsStatisticResponse?> Insert(int offset_, int limit_ = KintoneManager.CYBOZU_LIMIT)
    {
        var response = await Get(offset_, limit_);
        if (response != null)
        {
            SQLiteManager.Instance.InsertTable(AppsStatisticResponse.TableName(false), AppsStatisticResponse.ListInsertHeader(true), response.ListInsertValue(true));
        }
        return response;
    }
}
