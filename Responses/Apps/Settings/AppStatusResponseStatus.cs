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
using KintoneDeSql.Responses.Apps.Permissions;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace KintoneDeSql.Responses.Apps.Settings;

/// <summary>
/// https://cybozu.dev/ja/kintone/docs/rest-api/apps/settings/get-process-management-settings/
/// </summary>
[Table($"{SQLiteManager.SUB_DATABASE}.appStatus_Status")]
internal class AppStatusResponseStatus : AppStatusResponseBase, ISqlTable
{
    //enable	真偽値	プロセス管理が有効かどうか
    [JsonPropertyName("enabled")]
    [ColumnEx("enabled", Order = 3, TypeName = "NUMERIC")]
    public bool Enabled { get; set; } = false;

    //states	オブジェクト	ステータスの情報
    [JsonPropertyName("states")]
    public Dictionary<string, AppStatusValue> ListState { get; set; } = new ();

    //actions	配列	アクションの情報の一覧
    [JsonPropertyName("actions")]
    public List<ActionValue> ListAction { get; set; } = new();

    #region ISqlTable
    public static string TableName(bool withCamma_) => typeof(AppStatusResponseStatus).TableName(withCamma_);
    public static IEnumerable<string> ListCreateHeader(bool withCamma_) => MemberInfoExtension.ListCreateHeader(_listColumn(), withCamma_);
    public static IEnumerable<string> ListInsertHeader(bool withCamma_) => MemberInfoExtension.ListInsertHeader(_listColumn(), withCamma_);
    private static IEnumerable<ColumnData> _listColumn()
    {
        const int _SORT = 100;
        var rtn = typeof(AppStatusResponseBase).ListColumn();
        rtn.AddRange(typeof(AppStatusValue).ListColumn(string.Empty,_SORT));
        return rtn;
    }

    public virtual IEnumerable<IEnumerable<string>> ListInsertValue(bool withCamma_) => ListInsertValueState(withCamma_);
    public IEnumerable<IEnumerable<string>> ListInsertValueState(bool withCamma_)
    {
        var rtn = new List<IEnumerable<string>>();
        foreach (var state in ListState)
        {
            state.Value.Key = state.Key;
            //
            var list = base.ListValue(withCamma_, typeof(AppStatusResponseBase));
            list.AddRange(state.Value.ListValue(withCamma_, typeof(AppStatusValue)));
            rtn.Add(list);
        }
        return rtn;
    }
    #endregion
}

