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

namespace KintoneDeSql.Responses.Plugin;

/// <summary>
/// https://cybozu.dev/ja/kintone/docs/rest-api/plugins/get-plugin-apps/
/// </summary>
[Table($"{SQLiteManager.SUB_DATABASE}.pluginApp")]

internal class PluginAppResponse:BaseToData,ISqlTable
{
    //apps	配列（オブジェクト）	プラグインが追加されているアプリ情報の配列
    [JsonPropertyName("apps")]
    public List<PluginAppValue> ListApp { get; set; } = new();

    #region ISqlTable
    public static string TableName(bool withCamma_) => typeof(PluginAppResponse).TableName(withCamma_);
    public static IEnumerable<string> ListCreateHeader(bool withCamma_) => typeof(PluginAppValue).ListCreateHeader(withCamma_);
    public static IEnumerable<string> ListInsertHeader(bool withCamma_) => typeof(PluginAppValue).ListInsertHeader(withCamma_);
    public IEnumerable<IEnumerable<string>> ListInsertValue(bool withCamma_)
    {
        var rtn = new List<IList<string>>();
        ListApp.ForEach(app_ => rtn.Add(app_.ListValue(withCamma_)));
        return rtn;
    }
    #endregion
}
