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

namespace KintoneDeSql.Responses.Apps.Views;

/// <summary>
/// https://cybozu.dev/ja/kintone/docs/rest-api/apps/report/get-graph-settings/
/// </summary>
internal class ReportGroupValue : BaseToData
{
    //reports.グラフ名.groups[].code	文字列	分類する項目のフィールドコード
    [JsonPropertyName("code")]
    [ColumnEx("code", Order = 1, TypeName = "TEXT",IsUnique =true)]
    public string Code { get; set; } = string.Empty;

    //reports.グラフ名.groups[].per	文字列	分類する項目の時間単位
    [JsonPropertyName("per")]
    [ColumnEx("per", Order = 2, TypeName = "TEXT")]
    public string Per { get; set; } = string.Empty;
    public override string ToString()
    {
        return Code.ToString();
    }
}
