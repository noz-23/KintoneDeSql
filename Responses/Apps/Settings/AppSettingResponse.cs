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

namespace KintoneDeSql.Responses.Apps.Settings;

/// <summary>
/// https://cybozu.dev/ja/kintone/docs/rest-api/apps/settings/get-general-settings/
/// </summary>
[Table($"{SQLiteManager.SUB_DATABASE}.appSetting")]
internal class AppSettingResponse : BaseToData, ISqlTable, IAppTableId
{
    //revision	文字列	アプリの設定のリビジョン番号
    [JsonPropertyName("revision")]
    [ColumnEx("revision", Order = 2, TypeName = "TEXT")]
    public string Revision { get; set; } = string.Empty;

    //name	文字列	アプリ名
    [JsonPropertyName("name")]
    [ColumnEx("name", Order = 10, TypeName = "TEXT")]
    public string Name { get; set; } = string.Empty;

    //description	文字列	アプリの説明
    [JsonPropertyName("description")]
    [ColumnEx("description", Order = 11, TypeName = "TEXT")]
    public string Description { get; set; } = string.Empty;

    //enableThumbnails	真偽値	サムネイルを表示するかどうか
    [JsonPropertyName("enableThumbnails")]
    [ColumnEx("enableThumbnails", Order = 14, TypeName = "NUMERIC")]
    public bool EnableThumbnails { get; set; } = false;

    //enableBulkDeletion	真偽値	レコード一括削除を有効にするかどうか
    [JsonPropertyName("enableBulkDeletion")]
    [ColumnEx("enableBulkDeletion", Order = 15, TypeName = "NUMERIC")]
    public bool EnableBulkDeletion { get; set; } = false;

    //enableComments	真偽値	レコードのコメント機能を有効にするかどうか
    [JsonPropertyName("enableComments")]
    [ColumnEx("enableComments", Order = 16, TypeName = "NUMERIC")]
    public bool EnableComments { get; set; } = false;

    //enableDuplicateRecord	真偽値	「レコードを再利用する」機能を有効にするかどうか
    [JsonPropertyName("enableDuplicateRecord")]
    [ColumnEx("enableDuplicateRecord", Order = 17, TypeName = "NUMERIC")]
    public bool EnableDuplicateRecord { get; set; } = false;

    //enableInlineRecordEditing	真偽値	レコード一覧でのインライン編集を有効にするかどうか
    [JsonPropertyName("enableInlineRecordEditing")]
    [ColumnEx("enableInlineRecordEditing", Order = 18, TypeName = "NUMERIC")]
    public bool EnableInlineRecordEditing { get; set; } = false;

    //firstMonthOfFiscalYear	文字列	第一四半期の開始月
    [JsonPropertyName("firstMonthOfFiscalYear")]
    [ColumnEx("firstMonthOfFiscalYear", Order = 21, TypeName = "TEXT")]
    public string FirstMonthOfFiscalYear { get; set; } = string.Empty;

    //icon	オブジェクト	アプリのアイコンの情報
    [JsonPropertyName("icon")]
    [ColumnEx("icon", Order = 100, TypeName = "TEXT", IsExtract = true)]
    public IconFileValue Icon { get; set; } = new();

    //titleField	オブジェクト	レコードのタイトルの情報
    [JsonPropertyName("titleField")]
    [ColumnEx("titleField", Order = 200, TypeName = "TEXT", IsExtract = true)]
    public TitleFieldValue TitleField { get; set; } = new();
    //numberPrecision	オブジェクト	数値と計算の精度   
    [JsonPropertyName("numberPrecision")]
    [ColumnEx("numberPrecision", Order = 300, TypeName = "TEXT", IsExtract = true)]
    public NumberPrecisionValue NumberPrecision { get; set; } = new();

    #region NoJson
    /// <summary>
    /// 上位のアプリ ID
    /// </summary>
    [ColumnEx("appId", Order = 1, TypeName = "TEXT", IsUnique = true)]
    public string AppId { get; set; } = string.Empty;
    //
    #endregion

    #region ISqlTable
    public static string TableName(bool withCamma_) => typeof(AppSettingResponse).TableName(withCamma_);
    public static IEnumerable<string> ListCreateHeader(bool withCamma_)=> typeof(AppSettingResponse).ListCreateHeader(withCamma_);
    public static IEnumerable<string> ListInsertHeader(bool withCamma_)=> typeof(AppSettingResponse).ListInsertHeader(withCamma_);
    public IEnumerable<IEnumerable<string>> ListInsertValue(bool withCamma_)=> new List<List<string>>() { ListValue(withCamma_).ToList() };
    #endregion
}
