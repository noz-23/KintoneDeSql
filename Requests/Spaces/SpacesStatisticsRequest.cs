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
using KintoneDeSql.Responses.Spaces;
using System.Net.Http;

namespace KintoneDeSql.Requests.Spaces;

/// <summary>
/// https://cybozu.dev/ja/kintone/docs/rest-api/spaces/get-spaces-statistics/
/// </summary>
internal class SpacesStatisticsRequest : BaseSingleton<SpacesStatisticsRequest>
{
    private const string _COMMAND = "spaces/statistics.json";
    public async Task<ListSpacesStatisticResponse> Get()
    {
        var rtn = new ListSpacesStatisticResponse();

        var offset = 0;
        var count = 0;
        var limit = KintoneManager.CYBOZU_LIMIT;

        do
        {
            var query = $"query=limit {limit} offset {offset}";
            var paramater = string.Empty;
            var response = await KintoneManager.Instance.KintoneGet<ListSpacesStatisticResponse?>(HttpMethod.Get, _COMMAND, query, paramater);
            if (response == null)
            {
                break;
            }
            //LogFile.Instance.WriteLine($"{response.ListSpace.Count}");
            //
            rtn.ListSpace.AddRange(response.ListSpace);
            //

            count = response.ListSpace.Count;

            offset += count;
        } while (count == limit);

        return rtn;
    }
    public async Task<ListSpacesStatisticResponse> Insert()
    {
        var response = await Get();
        //if (response != null)
        //{
            SQLiteManager.Instance.InsertTable(ListSpacesStatisticResponse.TableName(false), ListSpacesStatisticResponse.ListInsertHeader(true), response.ListInsertValue(true));
        //}
        //
        return response;
    }
}
