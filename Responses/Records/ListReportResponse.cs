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
using KintoneDeSql.Extensions;
using KintoneDeSql.Interface;
using KintoneDeSql.Managers;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace KintoneDeSql.Responses.Records;


[Table($"{SQLiteManager.SUB_DATABASE}.reports")]
internal class ListReportResponse:BaseToData,ISqlTable
{
    // reports	オブジェクト	グラフの情報
    [JsonPropertyName("reports")]
    public Dictionary<string, ReportResponse> ListReport { get; set; } = new ();

    // revision	文字列	アプリの設定のリビジョン番号
    [JsonPropertyName("revision")]
    [ColumnEx("revision", Order = 2, TypeName = "TEXT")]
    public string Revision { get; set; } = string.Empty;

    /// <summary>
    /// 上位のアプリ ID
    /// </summary>
    [ColumnEx("appId", Order = 1, TypeName = "TEXT", IsUnique = true)]
    public string AppId { get; set; } = string.Empty;

    /// <summary>
    /// reports のKey
    /// </summary>
    [ColumnEx("report_key", Order = 3, TypeName = "TEXT", IsPrimary = true)]
    public string ReportKey { get; set; } = string.Empty;  // 実利用はなし

    #region ISqlTable
    public static string TableName(bool withCamma_) => typeof(ListReportResponse).TableName(withCamma_);

    public static List<string> ListCreateHeader(bool withCamma_)
    {
        var list = typeof(ListReportResponse).ListColumn();
        list.AddRange(typeof(ReportResponse).ListColumn());
        return MemberInfoExtension.ListCreateHeader(list, withCamma_);
    }
    public static List<string> ListInsertHeader(bool withCamma_)
    {
        var list = typeof(ListReportResponse).ListColumn();
        list.AddRange(typeof(ReportResponse).ListColumn());
        return MemberInfoExtension.ListInsertHeader(list, withCamma_);
    }
    public List<List<string>> ListInsertValue(bool withCamma_)
    {
        var rtn = new List<List<string>>();

        foreach (var report in ListReport)
        {
            ReportKey = report.Key;
            var list = this.ListValue(withCamma_);
            //
            list.AddRange(report.Value.ListValue(withCamma_));
            //
            rtn.Add(list);
        }

        return rtn;
    }
    #endregion
}
