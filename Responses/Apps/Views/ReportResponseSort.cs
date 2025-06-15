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
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KintoneDeSql.Responses.Apps.Views;

/// <summary>
/// https://cybozu.dev/ja/kintone/docs/rest-api/apps/report/get-graph-settings/
/// </summary>
[Table($"{SQLiteManager.SUB_DATABASE}.reports_Sort")]
internal class ReportResponseSort: ReportResponseBase,ISqlTable
{
    #region ISqlTable
    public static string TableName(bool withCamma_) => typeof(ReportResponseSort).TableName(withCamma_);
    public static IEnumerable<string> ListCreateHeader(bool withCamma_) => MemberInfoExtension.ListCreateHeader(_listColumn(), withCamma_);
    public static IEnumerable<string> ListInsertHeader(bool withCamma_) => MemberInfoExtension.ListInsertHeader(_listColumn(), withCamma_);
    private static IEnumerable<ColumnData> _listColumn()
    {
        const int _SORT = 100;
        var rtn = typeof(ReportResponseBase).ListColumn();
        rtn.AddRange(typeof(ReportValueBase).ListColumn(string.Empty, _SORT));
        rtn.AddRange(typeof(ReportSortValue).ListColumn(string.Empty, _SORT + _SORT));
        return rtn;
    }
    public virtual IEnumerable<IEnumerable<string>> ListInsertValue(bool withCamma_) => ListInsertValueSort(withCamma_);
    public IEnumerable<IEnumerable<string>> ListInsertValueSort(bool withCamma_)
    {
        var rtn = new List<IList<string>>();
        foreach (var report in ListReport)
        {
            report.Value.Key = report.Key;
            //
            foreach (var sort in report.Value.ListSort)
            {
                var list = ListValue(withCamma_,typeof(ReportResponseBase));
                list.AddRange(report.Value.ListValue(withCamma_, typeof(ReportValueBase)));
                list.AddRange(sort.ListValue(withCamma_));
                rtn.Add(list);
            }
        }
        return rtn;
    }
    #endregion
}
