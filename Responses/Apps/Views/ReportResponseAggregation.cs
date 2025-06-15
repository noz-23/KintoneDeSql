/*
 * Kintone De Sql
 * Copyright (c) 2025 noz-23
 *  https://github.com/noz-23/
 * 
 * Licensed under the MIT License 
 * 
 */
using KintoneDeSql.Data;
using KintoneDeSql.Extensions;
using KintoneDeSql.Interface;
using KintoneDeSql.Managers;
using System.ComponentModel.DataAnnotations.Schema;

namespace KintoneDeSql.Responses.Apps.Views;

/// <summary>
/// https://cybozu.dev/ja/kintone/docs/rest-api/apps/report/get-graph-settings/
/// </summary>
[Table($"{SQLiteManager.SUB_DATABASE}.reports_Aggregation")]
internal class ReportResponseAggregation:ReportResponseGroup, ISqlTable
{
    #region ISqlTable
    public static new string TableName(bool withCamma_) => typeof(ReportResponseAggregation).TableName(withCamma_);
    public static new IEnumerable<string> ListCreateHeader(bool withCamma_) => MemberInfoExtension.ListCreateHeader(_listColumn(), withCamma_);
    public static new IEnumerable<string> ListInsertHeader(bool withCamma_) => MemberInfoExtension.ListInsertHeader(_listColumn(), withCamma_);
    private static IEnumerable<ColumnData> _listColumn()
    {
        const int _SORT = 100;
        var rtn = typeof(ReportResponseBase).ListColumn();
        rtn.AddRange(typeof(ReportValueBase).ListColumn(string.Empty, _SORT));
        rtn.AddRange(typeof(ReportAggregationValue).ListColumn(string.Empty, _SORT + _SORT));
        return rtn;
    }
    public override IEnumerable<IEnumerable<string>> ListInsertValue(bool withCamma_) => ListInsertValueAggregation(withCamma_);
    public IEnumerable<IEnumerable<string>> ListInsertValueAggregation(bool withCamma_)
    {
        var rtn = new List<IEnumerable<string>>();
        foreach (var report in ListReport)
        {
            report.Value.Key = report.Key;
            //
            foreach (var aggregation in report.Value.ListAggregation)
            {
                var list = ListValue(withCamma_, typeof(ReportResponseBase));
                list.AddRange(report.Value.ListValue(withCamma_, typeof(ReportValueBase)));
                list.AddRange(aggregation.ListValue(withCamma_));
                rtn.Add(list);
            }
        }
        return rtn;
    }
    #endregion
}
