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
using KintoneDeSql.Managers;
using System.ComponentModel.DataAnnotations.Schema;

namespace KintoneDeSql.Responses.Apps.Settings;

/// <summary>
/// https://cybozu.dev/ja/kintone/docs/rest-api/apps/settings/get-process-management-settings/
/// </summary>
[Table($"{SQLiteManager.SUB_DATABASE}.appStatus_Action")]
internal class AppStatusResponseAction : AppStatusResponseEntity
{
    #region ISqlTable
    public static new string TableName(bool withCamma_) => typeof(AppStatusResponseAction).TableName(withCamma_);
    public static new IEnumerable<string> ListCreateHeader(bool withCamma_) => MemberInfoExtension.ListCreateHeader(_listColumn(), withCamma_);
    public static new IEnumerable<string> ListInsertHeader(bool withCamma_) => MemberInfoExtension.ListInsertHeader(_listColumn(), withCamma_);
    private static IEnumerable<ColumnData> _listColumn()
    {
        const int _SORT = 100;
        var rtn = typeof(AppStatusResponseBase).ListColumn();
        rtn.AddRange(typeof(ActionValue).ListColumn(string.Empty, _SORT));
        return rtn;
    }
    public override IEnumerable<IEnumerable<string>> ListInsertValue(bool withCamma_)=> ListInsertValueAction(withCamma_);

    public IEnumerable<IEnumerable<string>> ListInsertValueAction(bool withCamma_)
    {
        var rtn = new List<IEnumerable<string>>();

        foreach (var action in ListAction)
        {
            //
            var list = base.ListValue(withCamma_,typeof(AppStatusResponseBase));
            list.AddRange(action.ListValue(withCamma_));
            rtn.Add(list);
        }
        return rtn;
    }
    #endregion
}
