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
using System.Text.Json.Serialization;

namespace KintoneDeSql.Responses.Apps.Settings;

/// <summary>
/// https://cybozu.dev/ja/kintone/docs/rest-api/apps/settings/get-app-plugins/
/// </summary>
internal class AppPluginValue : BaseToData
{
    //plugins[].id	文字列	プラグインのID
    [JsonPropertyName("id")]
    [ColumnEx("id", Order = 10, TypeName = "TEXT", IsUnique = true)]
    public string Id { get; set; } = string.Empty;

    //plugins[].name	文字列	プラグインの名前
    [JsonPropertyName("name")]
    [ColumnEx("name", Order = 11, TypeName = "TEXT")]
    public string Name { get; set; } = string.Empty;

    //plugins[].enabled	真偽値	プラグインが有効かどうか
    [JsonPropertyName("enabled")]
    [ColumnEx("enabled", Order = 12, TypeName = "NUMERIC")]
    public bool Enabled { get; set; } = false;
}
