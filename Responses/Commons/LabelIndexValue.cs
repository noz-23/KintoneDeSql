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

namespace KintoneDeSql.Responses.Commons;

internal class OptionValue : LabelIndexValue
{
    #region NoJson
    /// <summary>
    /// 上位の
    /// </summary>
    [ColumnEx("key", Order = 1, TypeName = "TEXT")]
    public string Key { get; set; } = string.Empty;
    #endregion
}
internal class LabelIndexValue : BaseToData
{
    // properties.フィールドコード.options.（選択肢名）.label 文字列 選択肢名
    [JsonPropertyName("label")]
    [ColumnEx("label", Order = 10, TypeName = "TEXT")]
    public string Label { get; set; } = string.Empty;

    // properties.フィールドコード.options.（選択肢名）.index 文字列 選択肢の順番（昇順）
    [JsonPropertyName("index")]
    [ColumnEx("index", Order = 11, TypeName = "TEXT")]
    public string Index { get; set; } = string.Empty;
}
