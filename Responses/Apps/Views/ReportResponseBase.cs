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
internal class ReportResponseBase: BaseToData
{
    // reports	オブジェクト	グラフの情報
    [JsonPropertyName("reports")]
    public Dictionary<string, ReportValue> ListReport { get; set; } = new();

    // revision	文字列	アプリの設定のリビジョン番号
    [JsonPropertyName("revision")]
    [ColumnEx("revision", Order = 2, TypeName = "TEXT")]
    public string Revision { get; set; } = string.Empty;

    #region NoJson
    /// <summary>
    /// 上位のアプリ ID
    /// </summary>
    [ColumnEx("appId", Order = 1, TypeName = "TEXT", IsUnique = true)]
    public string AppId { get; set; } = string.Empty;
    #endregion
}
