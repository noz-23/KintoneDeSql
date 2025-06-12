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

namespace KintoneDeSql.Responses.Apps;

/// <summary>
/// https://cybozu.dev/ja/kintone/docs/rest-api/apps/get-app-admin-notes/
/// </summary>
[Table($"{SQLiteManager.SUB_DATABASE}.adminNotes")]
internal class AdminNotesResponse:BaseToData,ISqlTable
{
    // content	文字列	アプリの管理者用メモ本文
    [JsonPropertyName("content")]
    [ColumnEx("content", Order = 10, TypeName = "TEXT")]
    public string Content { get; set; } = string.Empty;

    // includeInTemplateAndDuplicates	真偽値	アプリテンプレートやアプリの再利用時にこのメモの内容を含めるかどうか
    [JsonPropertyName("includeInTemplateAndDuplicates")]
    [ColumnEx("includeInTemplateAndDuplicates", Order = 11, TypeName = "NUMERIC")]
    public bool IncludeInTemplateAndDuplicates { get; set; } = false;

    // revision	文字列	アプリの設定のリビジョン番号
    [JsonPropertyName("revision")]
    [ColumnEx("revision", Order = 2, TypeName = "TEXT")]
    public string Revision { get; set; } = string.Empty;

    /// <summary>
    /// 上位のApp ID
    /// </summary>
    [ColumnEx("appId", Order = 1, TypeName = "TEXT", IsPrimary =true)]
    public string AppId { get; set; } = string.Empty;

    #region ISqlTable
    public static string TableName(bool withCamma_) => typeof(AdminNotesResponse).TableName(withCamma_);

    public static List<string> ListCreateHeader(bool withCamma_)=>typeof(AdminNotesResponse).ListCreateHeader(withCamma_);

    public static List<string> ListInsertHeader(bool withCamma_) => typeof(AdminNotesResponse).ListInsertHeader(withCamma_);

    public List<List<string>> ListInsertValue(bool withCamma_) => new List<List<string>>() { this.ListValue(withCamma_)};

    #endregion
}
