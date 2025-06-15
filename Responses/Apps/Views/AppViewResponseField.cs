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
using KintoneDeSql.Responses.Commons;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace KintoneDeSql.Responses.Apps.Views;

/// <summary>
/// https://cybozu.dev/ja/kintone/docs/rest-api/apps/view/get-views/
/// </summary>
[Table($"{SQLiteManager.SUB_DATABASE}.appViews_Field")]
internal class AppViewResponseField : BaseToData, ISqlTable
{
    // views	オブジェクト	一覧の設定
    [JsonPropertyName("views")]
    public Dictionary<string, AppViewValue> ListView { get; set; } = new();

    // revision	文字列	アプリの設定のリビジョン番号
    [JsonPropertyName("revision")]
    [ColumnEx("revision", Order = 10, TypeName = "TEXT")]
    public string Revision { get; set; } = string.Empty;

    #region NoJson
    /// <summary>
    /// 上位のアプリ ID
    /// </summary>
    [ColumnEx("appId", Order = 1, TypeName = "TEXT", IsUnique = true)]
    public string AppId { get; set; } = string.Empty;

    #endregion

    //
    #region ISqlTable
    public static string TableName(bool withCamma_) => typeof(AppViewResponseField).TableName(withCamma_);
    public static IEnumerable<string> ListCreateHeader(bool withCamma_) => MemberInfoExtension.ListCreateHeader(_listColumn(), withCamma_);
    public static IEnumerable<string> ListInsertHeader(bool withCamma_) => MemberInfoExtension.ListInsertHeader(_listColumn(), withCamma_);
    private static IEnumerable<ColumnData> _listColumn()
    {
        const int _SORT = 100;
        var rtn = typeof(AppViewResponseField).ListColumn();
        rtn.AddRange(typeof(AppViewValueBase).ListColumn(string.Empty, _SORT));
        rtn.AddRange(typeof(FieldValueList).ListColumn(string.Empty, _SORT+ _SORT));
        return rtn;
    }
    public virtual IEnumerable<IEnumerable<string>> ListInsertValue(bool withCamma_) => ListInsertValueField(withCamma_);
    public IEnumerable<IEnumerable<string>> ListInsertValueField(bool withCamma_)
    {
        var rtn = new List<IList<string>>();
        foreach (var view in ListView)
        {
            view.Value.Key = view.Key;
            //
            foreach (var field in view.Value.ListField.ListValue(withCamma_))
            {
                var list = ListValue(withCamma_, typeof(AppViewResponseField));
                list.AddRange(view.Value.ListValue(withCamma_,typeof(AppViewValueBase)));
                list.Add(field);
                rtn.Add(list);
            }
        }

        return rtn;
    }
    #endregion

}
