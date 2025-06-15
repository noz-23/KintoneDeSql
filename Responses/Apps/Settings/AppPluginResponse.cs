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
/// https://cybozu.dev/ja/kintone/docs/rest-api/apps/settings/get-app-plugins/
/// </summary>
[Table($"{SQLiteManager.SUB_DATABASE}.appPlugins")]
internal class AppPluginResponse : BaseToData,ISqlTable
{
    //plugins	配列（オブジェクト）	アプリに追加されているプラグインの情報
    [JsonPropertyName("plugins")]
    public List<AppPluginValue> ListPlugins { get; set; } = new();

    //revision	文字列	アプリの設定のリビジョン番号
    [JsonPropertyName("revision")]
    [ColumnEx("revision", Order = 2, TypeName = "TEXT")]
    public string Revision { get; set; } = string.Empty;

    #region NoJson
    /// <summary>
    /// 上位のアプリ ID
    /// </summary>
    [ColumnEx("appId", Order = 1, TypeName = "TEXT", IsUnique = true)]
    public string AppId { get; set; } = string.Empty;
    //
    #endregion

    #region ISqlTable
    public static string TableName(bool withCamma_) => typeof(AppPluginResponse).TableName(withCamma_);
    public static IEnumerable<string> ListCreateHeader(bool withCamma_) => MemberInfoExtension.ListCreateHeader(_listColumn(), withCamma_);
    public static IEnumerable<string> ListInsertHeader(bool withCamma_) => MemberInfoExtension.ListInsertHeader(_listColumn(), withCamma_);

    private static IEnumerable<ColumnData> _listColumn()
    {
        const int _SORT = 100;
        var rtn = typeof(AppPluginResponse).ListColumn();
        rtn.AddRange(typeof(AppPluginValue).ListColumn(string.Empty, _SORT));
        return rtn;
    }
    public IEnumerable<IEnumerable<string>> ListInsertValue(bool withCamma_)
    {
        var rtn = new List<IList<string>>();

        foreach (var right in ListPlugins)
        {
            var list = base.ListValue(withCamma_);
            list.AddRange(right.ListValue(withCamma_));
            rtn.Add(list);
        }

        return rtn;
    }
    #endregion
}
