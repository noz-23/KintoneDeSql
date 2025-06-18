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
using KintoneDeSql.Responses.Commons;
using System.Text.Json.Serialization;

namespace KintoneDeSql.Responses.Apps.Settings;

/// <summary>
/// https://cybozu.dev/ja/kintone/docs/rest-api/apps/settings/get-app-plugins/
/// </summary>
internal class AppPluginValue : IdNameValue
{
    //plugins[].enabled	真偽値	プラグインが有効かどうか
    [JsonPropertyName("enabled")]
    [ColumnEx("enabled", Order = 12, TypeName = "NUMERIC")]
    public bool Enabled { get; set; } = false;
    public override string ToString()
    {
        return Id.ToString();
    }
}
