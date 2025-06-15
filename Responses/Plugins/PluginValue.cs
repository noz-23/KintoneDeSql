/*
 * Kintone De Sql
 * Copyright (c) 2025 noz-23
 *  https://github.com/noz-23/
 * 
 * Licensed under the MIT License 
 * 
 */
using KintoneDeSql.Attributes;
using System.Text.Json.Serialization;

namespace KintoneDeSql.Responses.Plugin;

/// <summary>
/// https://cybozu.dev/ja/kintone/docs/rest-api/plugins/get-plugins/
/// </summary>
internal class PluginValue: PluginRequiredValue
{
    //plugins[].id	文字列	プラグインID
    //plugins[].name	文字列	プラグインの名前
    //plugins[].isMarketPlugin	真偽値	プラグインストアのプラグインかどうか
    // -> base

    //plugins[].description	文字列	プラグインの説明
    [JsonPropertyName("description")]
    [ColumnEx("description", Order = 100, TypeName = "TEXT")]
    public string Description { get; set; } = string.Empty;


    //plugins[].version	文字列	プラグインのバージョン
    [JsonPropertyName("version")]
    [ColumnEx("version", Order = 102, TypeName = "TEXT")]
    public string Version { get; set; } = string.Empty;

}
