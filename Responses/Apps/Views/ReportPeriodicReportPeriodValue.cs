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
internal class ReportPeriodicReportPeriodValue : BaseToData
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