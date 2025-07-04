﻿/*
 * Kintone De Sql
 * Copyright (c) 2025 noz-23
 *  https://github.com/noz-23/
 * 
 * Licensed under the MIT License 
 * 
 */
using KintoneDeSql.Attributes;
using KintoneDeSql.Data;
using KintoneDeSql.Responses.Apps.Forms;
using System.Text.Json.Serialization;

namespace KintoneDeSql.Responses.Commons;

internal class ReferenceTableValue : BaseToData
{
    // properties.フィールドコード.referenceTable.filterCond 文字列	「さらに絞り込む条件」の設定
    [JsonPropertyName("filterCond")]
    [ColumnEx("filterCond", Order = 12, TypeName = "TEXT")]
    public string FilterCond { get; set; } = string.Empty;

    // properties.フィールドコード.referenceTable.relatedApp	オブジェクト	「参照するアプリ」の設定
    [JsonPropertyName("relatedApp")]
    [ColumnEx("relatedApp", Order = 100, TypeName = "TEXT", IsExtract = true)]
    public RelatedAppValue RelatedApp { get; set; } = new();

    // properties.フィールドコード.referenceTable.condition オブジェクト	「表示するレコードの条件」の設定
    [JsonPropertyName("condition")]
    [ColumnEx("condition", Order = 200, TypeName = "TEXT", IsExtract = true)]
    public FieldMappingValue Condition { get; set; } = new();

    // properties.フィールドコード.referenceTable.displayFields    配列  「表示するフィールド」に指定されたフィールドコードの一覧
    [JsonPropertyName("displayFields")]
    [ColumnEx("displayFields", Order = 300, TypeName = "TEXT")]
    public DisplayFieldValueList ListDisplayField { get; set; } = new();
}
