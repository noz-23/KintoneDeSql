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
/// https://cybozu.dev/ja/kintone/docs/rest-api/apps/settings/get-process-management-settings/
/// </summary>
internal class ActionValue : BaseToData
{
    //actions[].name	文字列	アクションの名前
    [JsonPropertyName("name")]
    [ColumnEx("name", Order = 1, TypeName = "TEXT")]
    public string Name { get; set; } = string.Empty;

    //actions[].from	文字列	アクション実行前のステータス名
    [JsonPropertyName("from")]
    [ColumnEx("from", Order = 2, TypeName = "TEXT")]
    public string From { get; set; } = string.Empty;

    //actions[].to	文字列	アクション実行後のステータス名
    [JsonPropertyName("to")]
    [ColumnEx("to", Order = 3, TypeName = "TEXT")]
    public string To { get; set; } = string.Empty;

    //actions[].filterCond	文字列	アクションの実行条件
    [JsonPropertyName("filterCond")]
    [ColumnEx("filterCond", Order = 4, TypeName = "TEXT")]
    public string FilterCond { get; set; } = string.Empty;
}
