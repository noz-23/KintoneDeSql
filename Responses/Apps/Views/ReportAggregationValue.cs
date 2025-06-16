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
using System.Xml.Linq;

namespace KintoneDeSql.Responses.Apps.Views;

/// <summary>
/// https://cybozu.dev/ja/kintone/docs/rest-api/apps/report/get-graph-settings/
/// </summary>
internal class ReportAggregationValue : BaseToData
{
    //reports.グラフ名.aggregations[].code	文字列	集計対象のフィールドコード
    [JsonPropertyName("code")]
    [ColumnEx("code", Order = 1, TypeName = "TEXT", IsUnique = true)]
    public string Code { get; set; } = string.Empty;

    //reports.グラフ名.aggregations[].type	文字列	集計方法の種類
    [JsonPropertyName("type")]
    [ColumnEx("type", Order = 2, TypeName = "TEXT")]
    public string Type { get; set; } = string.Empty;
    public override string ToString()
    {
        return Code.ToString();
    }
}