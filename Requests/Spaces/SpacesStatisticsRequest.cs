/*
 * Kintone De Sql
 * Copyright (c) 2025 noz-23
 *  https://github.com/noz-23/
 * 
 * Licensed under the MIT License 
 * 
 */
using KintoneDeSql.Managers;
using KintoneDeSql.Responses.Spaces;

namespace KintoneDeSql.Requests.Spaces;

/// <summary>
/// https://cybozu.dev/ja/kintone/docs/rest-api/spaces/get-spaces-statistics/
/// </summary>
//internal class SpacesStatisticsRequest : BaseSingleton<SpacesStatisticsRequest>
internal class SpacesStatisticsRequest : BaseRequest<SpacesStatisticsRequest, SpaceStatisticResponse>
{
    //private const string _COMMAND = "spaces/statistics.json";
    ////public async Task<SpaceStatisticResponse> Get()
    ////{
    ////    var rtn = new SpaceStatisticResponse();

    ////    var offset = 0;
    ////    var count = 0;
    ////    var limit = KintoneManager.CYBOZU_LIMIT;

    ////    do
    ////    {
    ////        var query = $"query=limit {limit} offset {offset}";
    ////        var paramater = string.Empty;
    ////        var response = await KintoneManager.Instance.KintoneGet<SpaceStatisticResponse?>(HttpMethod.Get, _COMMAND, query, paramater);
    ////        if (response == null)
    ////        {
    ////            break;
    ////        }
    ////        //LogFile.Instance.WriteLine($"{response.ListSpace.Count}");
    ////        //
    ////        rtn.ListSpace.AddRange(response.ListSpace);
    ////        //

    ////        count = response.ListSpace.Count;

    ////        offset += count;
    ////    } while (count == limit);

    ////    return rtn;
    ////}
    ////public async Task<SpaceStatisticResponse> Insert()
    ////{
    ////    var response = await Get();
    ////    //if (response != null)
    ////    //{
    ////        SQLiteManager.Instance.InsertTable(SpaceStatisticResponse.TableName(false), SpaceStatisticResponse.ListInsertHeader(true), response.ListInsertValue(true));
    ////    //}
    ////    //
    ////    return response;
    ////}
    //public async Task<SpaceStatisticResponse?> Get(int offset_, int limit_ = KintoneManager.CYBOZU_LIMIT)
    //{
    //    var query = string.Empty;
    //    var paramater = JsonSerializer.Serialize(new { offset = offset_, limit = limit_ });
    //    var response = await KintoneManager.Instance.KintoneGet<SpaceStatisticResponse?>(HttpMethod.Get, _COMMAND, query, paramater);

    //    return response;
    //}

    //public async Task<SpaceStatisticResponse?> Insert(int offset_, int limit_ = KintoneManager.CYBOZU_LIMIT)
    //{
    //    var response = await Get( offset_, limit_);
    //    if (response != null)
    //    {
    //        SQLiteManager.Instance.InsertTable(SpaceStatisticResponse.TableName(false), SpaceStatisticResponse.ListInsertHeader(true), response.ListInsertValue(true));
    //    }
    //    return response;
    //}
    protected override string _Command { get; } = "spaces/statistics.json";
    public override void Insert(SpaceStatisticResponse? response_)
    {
        if (response_ != null)
        {
            SQLiteManager.Instance.InsertTable(SpaceStatisticResponse.TableName(false), SpaceStatisticResponse.ListInsertHeader(true), response_.ListInsertValue(true));
        }
    }
}
