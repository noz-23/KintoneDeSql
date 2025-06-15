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
internal class ReportValueBase : BaseToData
{
    //reports.グラフ名.id	文字列	グラフID
    [JsonPropertyName("id")]
    [ColumnEx("id", Order = 4, TypeName = "TEXT",IsUnique =true)]
    public string Id { get; set; } = string.Empty;


    //reports.グラフ名.aggregations	配列	集計方法の一覧
    [JsonPropertyName("aggregations")]
    public List<ReportAggregationValue> ListAggregation { get; set; } = new();

    //reports.グラフ名.sorts	配列	ソートの一覧
    [JsonPropertyName("sorts")]
    public List<ReportSortValue> ListSort { get; set; } = new();

    //reports.グラフ名.groups	配列	分類する項目の一覧
    [JsonPropertyName("groups")]
    public List<ReportGroupValue> ListGroup { get; set; } = new();

    #region NO JSON
    /// <summary>
    /// reports のKey
    /// </summary>
    [ColumnEx("key", Order = 3, TypeName = "TEXT", IsUnique = true)]
    public string Key { get; set; } = string.Empty;  // 実利用はなし
    #endregion
}
