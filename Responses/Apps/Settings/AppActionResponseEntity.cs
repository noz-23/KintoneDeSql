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
using KintoneDeSql.Responses.Commons;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace KintoneDeSql.Responses.Apps.Settings;

/// <summary>
/// https://cybozu.dev/ja/kintone/docs/rest-api/apps/settings/get-action-settings/
/// </summary>
[Table($"{SQLiteManager.SUB_DATABASE}.appAction_Entity")]
internal class AppActionResponseEntity : AppActionResponseBase, ISqlTable
{
    //actions	オブジェクト	アクションの設定
    //actions.アクション名	オブジェクト	各アクションの設定
    [JsonPropertyName("actions")]
    public Dictionary<string, AppActionValue> ListAction { get; set; } = new();

    #region ISqlTable
    public static string TableName(bool withCamma_) => typeof(AppActionResponseEntity).TableName(withCamma_);
    public static IEnumerable<string> ListCreateHeader(bool withCamma_) => MemberInfoExtension.ListCreateHeader(_listColumn(), withCamma_);
    public static IEnumerable<string> ListInsertHeader(bool withCamma_) => MemberInfoExtension.ListInsertHeader(_listColumn(), withCamma_);
    private static IEnumerable<ColumnData> _listColumn()
    {
        const int _SORT = 100;
        var rtn = typeof(AppActionResponseBase).ListColumn();
        rtn.AddRange(typeof(AppActionValueBase).ListColumn(string.Empty, _SORT));
        rtn.AddRange(typeof(EntityValue).ListColumn(string.Empty, _SORT+ _SORT));
        return rtn;
    }
    public virtual IEnumerable<IEnumerable<string>> ListInsertValue(bool withCamma_)=> ListInsertValueEntity(withCamma_);
    public IEnumerable<IEnumerable<string>> ListInsertValueEntity(bool withCamma_)
    {
        var rtn = new List<IList<string>>();
        foreach (var action in ListAction)
        {
            action.Value.Key = action.Key;
            //
            foreach (var entity in action.Value.ListEntity)
            {
                var list = ListValue(withCamma_,typeof(AppActionResponseBase));
                list.AddRange(action.Value.ListValue(withCamma_,typeof(AppActionValueBase)));
                list.AddRange(entity.ListValue(withCamma_));
                rtn.Add(list);
            }
        }
        return rtn;
    }
    #endregion
}
