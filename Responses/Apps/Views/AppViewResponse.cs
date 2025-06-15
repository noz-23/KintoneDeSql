/*
 * Kintone De Sql
 * Copyright (c) 2025 noz-23
 *  https://github.com/noz-23/
 * 
 * Licensed under the MIT License 
 * 
 */
using KintoneDeSql.Attributes;
using KintoneDeSql.Data;
using KintoneDeSql.Extensions;
using KintoneDeSql.Interface;
using KintoneDeSql.Managers;
using KintoneDeSql.Responses.Apps.Views;
using KintoneDeSql.Responses.Commons;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using System.Windows.Documents;

namespace KintoneDeSql.Responses.Apps;

/// <summary>
/// https://cybozu.dev/ja/kintone/docs/rest-api/apps/view/get-views/
/// </summary>

[Table($"{SQLiteManager.SUB_DATABASE}.appViews")]
internal class AppViewResponse: AppViewResponseField, ISqlTable
{
    //
    #region ISqlTable
    public static new string TableName(bool withCamma_) => typeof(AppViewResponse).TableName(withCamma_);
    public static new IEnumerable<string> ListCreateHeader(bool withCamma_) => MemberInfoExtension.ListCreateHeader(_listColumn(), withCamma_);
    public static new IEnumerable<string> ListInsertHeader(bool withCamma_) => MemberInfoExtension.ListInsertHeader(_listColumn(), withCamma_);
    private static IEnumerable<ColumnData> _listColumn()
    {
        const int _SORT = 100;
        var rtn = typeof(AppViewResponse).ListColumn();
        rtn.AddRange(typeof(AppViewValue).ListColumn(string.Empty, _SORT));
        return rtn;
    }
    public override IEnumerable<IEnumerable<string>> ListInsertValue(bool withCamma_)
    {
        var rtn = new List<IEnumerable<string>>();
        foreach (var field in ListView)
        {
            field.Value.Key = field.Key;
            //
            foreach (var str in field.Value.ListField)
            {
                var list = this.ListValue(withCamma_);
                list.AddRange(field.Value.ListValue(withCamma_));
                rtn.Add(list);
            }
        }
        return rtn;
    }
    #endregion
}
