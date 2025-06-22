/*
 * Kintone De Sql
 * Copyright (c) 2025 noz-23
 *  https://github.com/noz-23/
 * 
 * Licensed under the MIT License 
 * 
 */
using KintoneDeSql.Managers;
using KintoneDeSql.Responses.Apps;

namespace KintoneDeSql.Requests.Spaces;

/// <summary>
/// https://cybozu.dev/ja/kintone/docs/rest-api/spaces/get-spaces-statistics/
/// </summary>
internal class AppsStatisticRequest : BaseRequest<AppsStatisticRequest, AppsStatisticResponse>
{
    protected override string _Command { get; } = "apps/statistics.json";
    public override void Insert(AppsStatisticResponse? response_)
    {
        if (response_ != null)
        {
            SQLiteManager.Instance.InsertTable(AppsStatisticResponse.TableName(false), AppsStatisticResponse.ListInsertHeader(true), response_.ListInsertValue(true));
        }
    }

}
