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

namespace KintoneDeSql.Responses.Apps.Settings;

/// <summary>
/// https://cybozu.dev/ja/kintone/docs/rest-api/apps/settings/get-process-management-settings/
/// </summary>
internal class AppStatusValue : AppStatusValueBase
{
    //states.ステータス名.name	文字列	ステータス名
    [JsonPropertyName("name")]
    [ColumnEx("name", Order = 10, TypeName = "TEXT")]
    public string Name { get; set; } = string.Empty;

    //states.ステータス名.index	文字列	ステータスの順番
    [JsonPropertyName("index")]
    [ColumnEx("index", Order = 11, TypeName = "TEXT")]
    public string Index { get; set; } = string.Empty;
    public override string ToString()
    {
        return Name.ToString();
    }
}
