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
using KintoneDeSql.Responses.Plugin;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace KintoneDeSql.Responses.Plugins;

/// <summary>
/// https://cybozu.dev/ja/kintone/docs/rest-api/plugins/get-required-plugins/
/// </summary>
[Table($"{SQLiteManager.SUB_DATABASE}.pluginRequired")]
class PluginRequiredResponse : BaseToData, ISqlTable
{
    //plugins	配列（オブジェクト）	インストールが必要なプラグインの一覧
    [JsonPropertyName("plugins")]
    public List<PluginRequiredValue> ListPlugin { get; set; } = new();

    #region ISqlTable
    public static string TableName(bool withCamma_) => typeof(PluginRequiredResponse).TableName(withCamma_);
    public static IEnumerable<string> ListCreateHeader(bool withCamma_) => typeof(PluginRequiredValue).ListCreateHeader(withCamma_);
    public static IEnumerable<string> ListInsertHeader(bool withCamma_) => typeof(PluginRequiredValue).ListInsertHeader(withCamma_);
    public IEnumerable<IEnumerable<string>> ListInsertValue(bool withCamma_)
    {
        var rtn = new List<IList<string>>();
        ListPlugin.ForEach(plugin_ => rtn.Add(plugin_.ListValue(withCamma_)));
        return rtn;
    }
    #endregion
}
