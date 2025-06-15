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
internal class ReportPeriodicReportValue : BaseToData
{
    //reports.グラフ名.periodicReport.active	真偽値	定期レポートの実行状態
    [JsonPropertyName("active")]
    [ColumnEx("active", Order = 400, TypeName = "NUMERIC")]
    public bool Active { get; set; } = false;

    //reports.グラフ名.periodicReport.period	オブジェクト	定期レポートの集計間隔
    [JsonPropertyName("period")]
    [ColumnEx("period", Order = 401, TypeName = "TEXT", IsExtract = true)]
    public ReportPeriodicReportPeriodValue Period { get; set; } = new();
}