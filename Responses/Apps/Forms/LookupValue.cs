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
using KintoneDeSql.Responses.Commons;
using System.Text.Json.Serialization;

namespace KintoneDeSql.Responses.Apps.Forms;

/// <summary>
/// https://cybozu.dev/ja/kintone/docs/overview/field-types/
/// </summary>
internal class LookupValue : BaseToData
{
    // properties.フィールドコード.lookup.relatedApp	オブジェクト	「関連付けるアプリ」の設定
    [JsonPropertyName("relatedApp")]
    [ColumnEx("relatedApp", Order = 100, TypeName = "TEXT", IsExtract = true)]
    public RelatedAppValue? RelatedApp { get; set; } = new();

    // properties.フィールドコード.lookup.relatedKeyField 文字列	「コピー元のフィールド」に指定されたフィールドのフィールドコード
    [JsonPropertyName("size")]
    [ColumnEx("size", Order = 10, TypeName = "TEXT")]
    public string Size { get; set; } = string.Empty;

    // properties.フィールドコード.lookup.filterCond	文字列	絞り込みの初期設定
    [JsonPropertyName("filterCond")]
    [ColumnEx("filterCond", Order = 11, TypeName = "TEXT")]
    public string FilterCond { get; set; } = string.Empty;

    // properties.フィールドコード.lookup.sort	文字列	ソートの初期設定
    [JsonPropertyName("sort")]
    [ColumnEx("sort", Order = 12, TypeName = "TEXT")]
    public string Sort { get; set; } = string.Empty;

    // properties.フィールドコード.lookup.lookupPickerFields 配列	「コピー元のレコードの選択時に表示するフィールド」の設定を表すフィールドコードの一覧
    [JsonPropertyName("lookupPickerFields")]
    [ColumnEx("lookupPickerFields", Order = 20, TypeName = "TEXT")]
    public StringValueList ListLookupPickerField { get; set; } = new();

    // properties.フィールドコード.lookup.fieldMappings    配列  「ほかのフィールドのコピー」の設定の一覧
    [JsonPropertyName("fieldMappings")]
    [ColumnEx("fieldMappings", Order = 100, TypeName = "TEXT")]
    public FieldMappingValueList ListFieldMapping { get; set; } = new();

}
