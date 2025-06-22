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
internal class SpacesStatisticsRequest : BaseRequest<SpacesStatisticsRequest, SpaceStatisticResponse>
{
    protected override string _Command { get; } = "spaces/statistics.json";
    public override void Insert(SpaceStatisticResponse? response_)
    {
        if (response_ != null)
        {
            SQLiteManager.Instance.InsertTable(SpaceStatisticResponse.TableName(false), SpaceStatisticResponse.ListInsertHeader(true), response_.ListInsertValue(true));
        }
    }
}
