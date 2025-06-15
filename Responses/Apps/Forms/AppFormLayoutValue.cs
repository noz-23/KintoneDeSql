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

namespace KintoneDeSql.Responses.Apps.Forms;

/// <summary>
/// https://cybozu.dev/ja/kintone/docs/rest-api/apps/form/get-form-layout/
/// </summary>
internal class AppFormLayoutValue : BaseToData
{
    // layout[].code	文字列	テーブル、またはグループのコード
    [JsonPropertyName("code")]
    [ColumnEx("code", Order = 20, TypeName = "TEXT",IsUnique =true)]
    public string Code { get; set; } = string.Empty;

    // layout[].type	文字列	行の種類
    [JsonPropertyName("type")]
    [ColumnEx("type", Order = 50, TypeName = "TEXT")]
    public string Type { get; set; } = string.Empty;

    /// <summary>
    /// リストごとに展開して保存
    /// </summary>
    // layout[].fields	配列	行に含まれるフィールドの一覧
    [JsonPropertyName("fields")]
    public List<LineValue> ListField { get; set; } = new();

    /// <summary>
    /// 再帰処理して保存
    /// </summary>
    // layout[].layout	配列	グループ内のフィールドのレイアウトの一覧
    [JsonPropertyName("layout")]
    public List<AppFormLayoutValue> ListLayout { get; set; } = new();

    #region NoJson
    /// <summary>
    /// AppFromLayoutResponse の番号
    /// </summary>
    [ColumnEx("row", Order = 2, TypeName = "TEXT", IsUnique =true)]
    public string MainRow { get; set; } = string.Empty;

    /// <summary>
    /// AppFromLayoutResponse の番号
    /// </summary>
    [ColumnEx("layout_row", Order = 3, TypeName = "TEXT", IsUnique = true)]
    public string SubRow { get; set; } = string.Empty;
    #endregion

}
