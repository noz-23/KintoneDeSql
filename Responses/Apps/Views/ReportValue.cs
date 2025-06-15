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
internal class ReportValue : ReportValueBase
{
     //reports.グラフ名.name	文字列	グラフ名
    [JsonPropertyName("name")]
    [ColumnEx("name", Order = 10, TypeName = "TEXT")]
    public string Name { get; set; } = string.Empty;

   //reports.グラフ名.chartType	文字列	グラフの種類
    [JsonPropertyName("chartType")]
    [ColumnEx("chartType", Order = 11, TypeName = "TEXT")]
    public string ChartType { get; set; } = string.Empty;

    //reports.グラフ名.chartMode	文字列	グラフの表示モード
    [JsonPropertyName("chartMode")]
    [ColumnEx("chartMode", Order = 12, TypeName = "TEXT")]
    public string ChartMode { get; set; } = string.Empty;


    //reports.グラフ名.index	文字列	グラフの並び順
    [JsonPropertyName("index")]
    [ColumnEx("index", Order = 20, TypeName = "TEXT")]
    public string index { get; set; } = string.Empty;

    // reports.グラフ名.filterCond	文字列	絞り込み条件
    [JsonPropertyName("filterCond")]
    [ColumnEx("filterCond", Order = 21, TypeName = "TEXT")]
    public string filterCond { get; set; } = string.Empty;

    //reports.グラフ名.periodicReport	オブジェクト	定期レポートの設定
    [JsonPropertyName("periodicReport")]
    [ColumnEx("periodicReport", Order = 50, TypeName = "TEXT", IsExtract = true)]
    public ReportPeriodicReportValue PeriodicReport { get; set; } = new();
}
