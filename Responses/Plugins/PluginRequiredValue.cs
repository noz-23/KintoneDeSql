/*
 * Kintone De Sql
 * Copyright (c) 2025 noz-23
 *  https://github.com/noz-23/
 * 
 * Licensed under the MIT License 
 * 
 */
using KintoneDeSql.Attributes;
using KintoneDeSql.Responses.Commons;
using System.Text.Json.Serialization;

namespace KintoneDeSql.Responses.Plugin;

/// <summary>
/// https://cybozu.dev/ja/kintone/docs/rest-api/plugins/get-required-plugins/
/// </summary>
internal class PluginRequiredValue: PluginAppValue
{
    //plugins[].id	文字列	プラグインID
    //plugins[].name	文字列	プラグインの名前
    // -> base

    //plugins[].isMarketPlugin	真偽値	プラグインストアのプラグインかどうか
    [JsonPropertyName("isMarketPlugin")]
    [ColumnEx("isMarketPlugin", Order = 101, TypeName = "NUMERIC")]
    public bool IsMarketPlugin { get; set; } = false;
}
