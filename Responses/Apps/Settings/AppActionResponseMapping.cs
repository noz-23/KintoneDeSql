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
using KintoneDeSql.Managers;
using System.ComponentModel.DataAnnotations.Schema;
using System.Windows.Documents;

namespace KintoneDeSql.Responses.Apps.Settings;

/// <summary>
/// https://cybozu.dev/ja/kintone/docs/rest-api/apps/settings/get-action-settings/
/// </summary>
[Table($"{SQLiteManager.SUB_DATABASE}.appAction_Mapping")]
internal class AppActionResponseMapping : AppActionResponseEntity
{
    #region ISqlTable
    public static new string TableName(bool withCamma_) => typeof(AppActionResponseMapping).TableName(withCamma_);
    public static new IEnumerable<string> ListCreateHeader(bool withCamma_) => MemberInfoExtension.ListCreateHeader(_listColumn(), withCamma_);
    public static new IEnumerable<string> ListInsertHeader(bool withCamma_) => MemberInfoExtension.ListInsertHeader(_listColumn(), withCamma_);
    private static IEnumerable<ColumnData> _listColumn()
    {
        const int _SORT = 100;
        var rtn = typeof(AppActionResponseBase).ListColumn();
        rtn.AddRange(typeof(AppActionValueBase).ListColumn(string.Empty, _SORT));
        rtn.AddRange(typeof(MappingValue).ListColumn(string.Empty, _SORT+ _SORT));
        return rtn;
    }

    public override IEnumerable<IEnumerable<string>> ListInsertValue(bool withCamma_) => ListInsertValueMapping(withCamma_);
    public IEnumerable<IEnumerable<string>> ListInsertValueMapping(bool withCamma_)
    {
        var rtn = new List<IList<string>>();
        foreach (var action in ListAction)
        {
            action.Value.Key = action.Key;
            //
            foreach (var mapping in action.Value.ListMapping)
            {
                var list = ListValue(withCamma_,typeof(AppActionResponseBase));
                list.AddRange(action.Value.ListValue(withCamma_,typeof(AppActionValueBase)));
                list.AddRange(mapping.ListValue(withCamma_));
                rtn.Add(list);
            }
        }
        return rtn;
    }
    #endregion

}

