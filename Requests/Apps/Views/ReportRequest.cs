/*
 * Kintone De Sql
 * Copyright (c) 2025 noz-23
 *  https://github.com/noz-23/
 * 
 * Licensed under the MIT License 
 * 
 */
using KintoneDeSql.Managers;
using KintoneDeSql.Responses.Apps.Views;

namespace KintoneDeSql.Requests.Apps.Views;

/// <summary>
/// https://cybozu.dev/ja/kintone/docs/rest-api/apps/report/get-graph-settings/
/// </summary>
internal class ReportRequest : BaseRequest<ReportRequest, ReportResponse>
{
    protected override string _Command { get; } = "app/reports.json";
    public override void Insert(ReportResponse? response_)
    {
        if (response_ != null)
        {
            SQLiteManager.Instance.InsertTable(ReportResponse.TableName(false), ReportResponse.ListInsertHeader(true), response_.ListInsertValue(true));
            SQLiteManager.Instance.InsertTable(ReportResponseAggregation.TableName(false), ReportResponseAggregation.ListInsertHeader(true), response_.ListInsertValueAggregation(true));
            SQLiteManager.Instance.InsertTable(ReportResponseGroup.TableName(false), ReportResponseGroup.ListInsertHeader(true), response_.ListInsertValueGroup(true));
            SQLiteManager.Instance.InsertTable(ReportResponseSort.TableName(false), ReportResponseSort.ListInsertHeader(true), response_.ListInsertValueSort(true));
        }
    }
}
