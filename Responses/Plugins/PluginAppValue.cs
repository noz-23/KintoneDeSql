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

namespace KintoneDeSql.Responses.Plugins;

/// <summary>
/// https://cybozu.dev/ja/kintone/docs/rest-api/plugins/get-plugin-apps/
/// </summary>
internal class PluginAppValue : IdNameValue
{
    [JsonPropertyName("id")]
    [ColumnEx("id", Order = 1, TypeName = "TEXT", IsUnique = true)]
    public override string Id { get; set; } = string.Empty;

}
