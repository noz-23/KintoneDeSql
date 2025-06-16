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
using System.Text.Json.Serialization;

namespace KintoneDeSql.Responses.Apps.Views;

/// <summary>
/// https://cybozu.dev/ja/kintone/docs/rest-api/apps/report/get-graph-settings/
/// </summary>
internal class ReportSortValue : BaseToData
{
    //reports.グラフ名.sorts[].by	文字列	ソートの対象
    [JsonPropertyName("by")]
    [ColumnEx("by", Order = 1, TypeName = "TEXT", IsUnique = true)]
    public string By { get; set; } = string.Empty;

    //reports.グラフ名.sorts[].order	文字列	ソートの並び順
    [JsonPropertyName("order")]
    [ColumnEx("order", Order = 2, TypeName = "TEXT")]
    public string Order { get; set; } = string.Empty;

    public override string ToString()
    {
        return $"{By.ToString()} {Order.ToString()}";
    }
}