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
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using System.Windows.Documents;

namespace KintoneDeSql.Responses.Apps;

/// <summary>
/// https://cybozu.dev/ja/kintone/docs/rest-api/apps/view/get-views/
/// </summary>

[Table($"{SQLiteManager.SUB_DATABASE}.appViews")]
internal class ListAppViewResponse: BaseToData, ISqlTable
{
    // views	オブジェクト	一覧の設定
    [JsonPropertyName("views")]
    public Dictionary<string, AppViewResponse> ListView { get; set; } = new();

    // revision	文字列	アプリの設定のリビジョン番号
    [JsonPropertyName("revision")]
    [ColumnEx("revision", Order = 10, TypeName = "TEXT")]
    public string Revision { get; set; } = string.Empty;

    #region NoJson
    [ColumnEx("views_key", Order = 3, TypeName = "TEXT")]
    public string ViewsKey { get; set; } = string.Empty;

    /// <summary>
    /// 上位のアプリ ID
    /// </summary>
    [ColumnEx("appId", Order = 1, TypeName = "TEXT")]
    public string AppId { get; set; } = string.Empty;

    #endregion
    //
    #region ISqlTable
    public static string TableName(bool withCamma_) => typeof(ListAppViewResponse).TableName(withCamma_);
    public static List<string> ListCreateHeader(bool withCamma_)
    {
        var list = typeof(ListAppViewResponse).ListColumn();
        list.AddRange(typeof(AppViewResponse).ListColumn());
        return MemberInfoExtension.ListCreateHeader(list, withCamma_);
    }

    public static List<string> ListInsertHeader(bool withCamma_)
    {
        var list = typeof(ListAppViewResponse).ListColumn();
        list.AddRange(typeof(AppViewResponse).ListColumn());
        return MemberInfoExtension.ListInsertHeader(list, withCamma_);
    }

    //public List<List<string>> ListInsertValue(bool withCamma_) => _listInsertValue(base.ListValue(withCamma_), ListView, withCamma_);

    //private List<List<string>> _listInsertValue(List<string> listApps_, Dictionary<string, AppViewResponse> listView_, bool withCamma_, string subFieldKey_ = "")
    //{
    //    var rtn = new List<List<string>>();
    //    foreach (var field in listView_)
    //    {
    //        ViewsKey = (string.IsNullOrEmpty(subFieldKey_) == true) ? field.Key : subFieldKey_;
    //        field.Value.FieldKey = (string.IsNullOrEmpty(subFieldKey_) == true) ? string.Empty : field.Key;

    //        var list = base.ListValue(withCamma_);
    //        list.AddRange(field.Value.ListValue(withCamma_));
    //        rtn.Add(list);

    //        //if (field.Value.Type == "SUBTABLE")
    //        //{
    //        //    //rtn.AddRange(_listInsertValue(listApps_, field.Value.ListField, withCamma_, mainFiledKey));
    //        //    rtn.AddRange(_listInsertValue(listApps_, field.Value.ListField, withCamma_, ViewsKey));

    //        //}
    //    }

    //    return rtn;
    //}
    public List<List<string>> ListInsertValue(bool withCamma_)
    {
        var rtn = new List<List<string>>();
        foreach (var field in ListView)
        {
            ViewsKey = field.Key;
            //
            var list = base.ListValue(withCamma_);
            list.AddRange(field.Value.ListValue(withCamma_));
            rtn.Add(list);

            //if (field.Value.Type == "SUBTABLE")
            //{
            //    //rtn.AddRange(_listInsertValue(listApps_, field.Value.ListField, withCamma_, mainFiledKey));
            //    rtn.AddRange(_listInsertValue(listApps_, field.Value.ListField, withCamma_, ViewsKey));

            //}
        }

        return rtn;
    }
    #endregion
}
