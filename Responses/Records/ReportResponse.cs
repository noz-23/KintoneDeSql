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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace KintoneDeSql.Responses.Records;

internal class ReportGroupResponse : BaseToData
{
    //reports.グラフ名.groups[].code	文字列	分類する項目のフィールドコード
    [JsonPropertyName("code")]
    [ColumnEx("code", Order = 100, TypeName = "TEXT")]
    public string Code { get; set; } = string.Empty;

    //reports.グラフ名.groups[].per	文字列	分類する項目の時間単位
    [JsonPropertyName("per")]
    [ColumnEx("per", Order = 101, TypeName = "TEXT")]
    public string Per { get; set; } = string.Empty;
}
internal class ReportAggregationResponse : BaseToData
{
    //reports.グラフ名.aggregations[].type	文字列	集計方法の種類
    [JsonPropertyName("type")]
    [ColumnEx("type", Order = 200, TypeName = "TEXT")]
    public string Type { get; set; } = string.Empty;

    //reports.グラフ名.aggregations[].code	文字列	集計対象のフィールドコード
    [JsonPropertyName("code")]
    [ColumnEx("code", Order = 201, TypeName = "TEXT")]
    public string Code { get; set; } = string.Empty;

}

internal class ReportSortResponse : BaseToData
{
    //reports.グラフ名.sorts[].by	文字列	ソートの対象
    [JsonPropertyName("by")]
    [ColumnEx("by", Order = 300, TypeName = "TEXT")]
    public string By { get; set; } = string.Empty;

    //reports.グラフ名.sorts[].order	文字列	ソートの並び順
    [JsonPropertyName("order")]
    [ColumnEx("order", Order = 301, TypeName = "TEXT")]
    public string Order { get; set; } = string.Empty;

}

internal class ReportPeriodicReportResponse:BaseToData
{
    //reports.グラフ名.periodicReport.active	真偽値	定期レポートの実行状態
    [JsonPropertyName("active")]
    [ColumnEx("active", Order = 400, TypeName = "NUMERIC")]
    public bool Active { get; set; } = false;

    //reports.グラフ名.periodicReport.period	オブジェクト	定期レポートの集計間隔
    [JsonPropertyName("period")]
    [ColumnEx("period", Order = 401, IsExtract =true)]
    public ReportPeriodicReportPeriodResponse Period { get; set; } = new ();
}
internal class ReportPeriodicReportPeriodResponse : BaseToData
{
    //reports.グラフ名.periodicReport.period.every	文字列	定期レポートの集計間隔の種類
    [JsonPropertyName("every")]
    [ColumnEx("every", Order = 1000, TypeName = "TEXT")]
    public string Every { get; set; } = string.Empty;

    //reports.グラフ名.periodicReport.period.month	文字列	集計を実施する月
    [JsonPropertyName("month")]
    [ColumnEx("month", Order = 1001, TypeName = "TEXT")]
    public string Month { get; set; } = string.Empty;

    //reports.グラフ名.periodicReport.period.time	文字列	集計を実施する時刻
    [JsonPropertyName("time")]
    [ColumnEx("time", Order = 1002, TypeName = "TEXT")]
    public string Time { get; set; } = string.Empty;

    //reports.グラフ名.periodicReport.period.pattern	文字列	四半期の集計を実施する月
    [JsonPropertyName("pattern")]
    [ColumnEx("pattern", Order = 1003, TypeName = "TEXT")]
    public string Pattern { get; set; } = string.Empty;

    //reports.グラフ名.periodicReport.period.dayOfMonth	文字列	集計を実施する日
    [JsonPropertyName("dayOfMonth")]
    [ColumnEx("dayOfMonth", Order = 1004, TypeName = "TEXT")]
    public string DayOfMonth { get; set; } = string.Empty;

    //reports.グラフ名.periodicReport.period.dayOfWeek	文字列	集計を実施する曜日
    [JsonPropertyName("dayOfWeek")]
    [ColumnEx("dayOfWeek", Order = 1005, TypeName = "TEXT")]
    public string DayOfWeek { get; set; } = string.Empty;

    //reports.グラフ名.periodicReport.period.minute	文字列	集計を実施する分
    [JsonPropertyName("minute")]
    [ColumnEx("minute", Order = 1006, TypeName = "TEXT")]
    public string Minute { get; set; } = string.Empty;
}


/// <summary>
/// https://cybozu.dev/ja/kintone/docs/rest-api/apps/report/get-graph-settings/
/// </summary>
internal class ReportResponse : BaseToData
{
    //reports.グラフ名.chartType	文字列	グラフの種類
    [JsonPropertyName("chartType")]
    [ColumnEx("chartType", Order = 10, TypeName = "TEXT")]
    public string ChartType { get; set; } = string.Empty;

    //reports.グラフ名.chartMode	文字列	グラフの表示モード
    [JsonPropertyName("chartMode")]
    [ColumnEx("chartMode", Order = 11, TypeName = "TEXT")]
    public string ChartMode { get; set; } = string.Empty;

    //reports.グラフ名.id	文字列	グラフID
    [JsonPropertyName("id")]
    [ColumnEx("id", Order = 1, TypeName = "TEXT")]
    public string Id { get; set; } = string.Empty;

    //reports.グラフ名.name	文字列	グラフ名
    [JsonPropertyName("name")]
    [ColumnEx("name", Order = 2, TypeName = "TEXT")]
    public string Name { get; set; } = string.Empty;

    //reports.グラフ名.index	文字列	グラフの並び順
    [JsonPropertyName("index")]
    [ColumnEx("index", Order = 20, TypeName = "TEXT")]
    public string index { get; set; } = string.Empty;

    //reports.グラフ名.groups	配列	分類する項目の一覧
    [JsonPropertyName("groups")]
    public List<ReportGroupResponse> ListGroup { get; set; } = new ();

    // reports.グラフ名.filterCond	文字列	絞り込み条件
    [JsonPropertyName("filterCond")]
    [ColumnEx("filterCond", Order = 21, TypeName = "TEXT")]
    public string filterCond { get; set; } = string.Empty;

    //reports.グラフ名.aggregations	配列	集計方法の一覧
    [JsonPropertyName("aggregations")]
    public List<ReportAggregationResponse> ListAggregation { get; set; } = new();

    //reports.グラフ名.sorts	配列	ソートの一覧
    [JsonPropertyName("sorts")]
    public List<ReportSortResponse> ListSort { get; set; } = new();

    //reports.グラフ名.periodicReport	オブジェクト	定期レポートの設定
    [JsonPropertyName("periodicReport")]
    [ColumnEx("periodicReport", Order = 50, IsExtract =true)]
    public ReportPeriodicReportResponse PeriodicReport { get; set; } = new ();

}
