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

namespace KintoneDeSql.Responses.Apps.Settings;

/// <summary>
/// https://cybozu.dev/ja/kintone/docs/rest-api/apps/settings/get-customization/
/// </summary>
[Table($"{SQLiteManager.SUB_DATABASE}.appCustomize")]
internal class AppCustomizeResponse:BaseToData,ISqlTable, IAppTableId
{
    //scope	文字列	カスタマイズの適用範囲
    [JsonPropertyName("scope")]
    [ColumnEx("scope", Order = 10, TypeName = "TEXT")]
    public string Scope { get; set; } = string.Empty;

    //desktop	オブジェクト	PCで読み込まれるファイルの情報
    [JsonPropertyName("desktop")]
    public DeskTopFileValue DeskTop { get; set; } = new();

    //mobile	オブジェクト	モバイルで読み込まれるファイルの情報
    [JsonPropertyName("mobile")]
    public MobileFileValue Mobile { get; set; } = new();

    //revision	文字列	アプリの設定のリビジョン番号
    [JsonPropertyName("revision")]
    [ColumnEx("revision", Order = 11, TypeName = "TEXT")]
    public string Revision { get; set; } = string.Empty;

    #region NoJson
    /// <summary>
    /// 上位のアプリID
    /// </summary>
    [ColumnEx("appId", Order = 1, TypeName = "TEXT", IsUnique = true)]
    public string AppId { get; set; } = string.Empty;
    #endregion

    #region ISqlTable
    public static string TableName(bool withCamma_) => typeof(AppCustomizeResponse).TableName(withCamma_);
    public static IEnumerable<string> ListCreateHeader(bool withCamma_) => MemberInfoExtension.ListCreateHeader(_listColumn(), withCamma_);
    public static IEnumerable<string> ListInsertHeader(bool withCamma_) => MemberInfoExtension.ListInsertHeader(_listColumn(), withCamma_);
    private static IEnumerable<ColumnData> _listColumn()
    {
        const int _SORT = 100;
        var rtn = typeof(AppCustomizeResponse).ListColumn();
        rtn.AddRange(typeof(JsCssFileValue).ListColumn("client_", _SORT));
        rtn.AddRange(typeof(CustomFileValue).ListColumn("client_type_", _SORT+ _SORT)); 
        return rtn;
    }
    public IEnumerable<IEnumerable<string>> ListInsertValue(bool withCamma_)
    {
        var rtn = new List<IList<string>>();
        //
        foreach (var desktop in DeskTop.ListJs)
        {
            var list = ListValue(withCamma_);
            list.AddRange(DeskTop.ListValue(withCamma_));
            list.AddRange(desktop.ListValue(withCamma_));
            rtn.Add(list);
        }

        //DeskTop.FileType = "css";
        foreach (var desktop in DeskTop.ListCss)
        {
            var list = ListValue(withCamma_);
            list.AddRange(DeskTop.ListValue(withCamma_));
            list.AddRange(desktop.ListValue(withCamma_));
            rtn.Add(list);
        }
        //
        //FileType = "mobile";
        //Mobile.FileType = "js";
        foreach (var moblie in Mobile.ListJs)
        {
            var list = ListValue(withCamma_);
            list.AddRange(Mobile.ListValue(withCamma_));
            list.AddRange(moblie.ListValue(withCamma_));
            rtn.Add(list);
        }

        //Mobile.FileType = "css";
        foreach (var moblie in Mobile.ListCss)
        {
            var list = ListValue(withCamma_);
            list.AddRange(Mobile.ListValue(withCamma_));
            list.AddRange(moblie.ListValue(withCamma_));
            rtn.Add(list);
        }

        return rtn;
    }
    #endregion
}
