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
/// https://cybozu.dev/ja/kintone/docs/rest-api/apps/form/get-form-fields/
/// </summary>
internal class LookupValue : BaseToData
{
    // properties.フィールドコード.lookup.relatedApp	オブジェクト	「関連付けるアプリ」の設定
    [JsonPropertyName("relatedApp")]
    [ColumnEx("relatedApp", Order = 100, TypeName = "TEXT", IsExtract = true)]
    public RelatedAppValue RelatedApp { get; set; } = new();

    // properties.フィールドコード.lookup.relatedKeyField 文字列	「コピー元のフィールド」に指定されたフィールドのフィールドコード
    [JsonPropertyName("relatedKeyField")]
    [ColumnEx("relatedKeyField", Order = 110, TypeName = "TEXT")]
    public string RelatedKeyField { get; set; } = string.Empty;

    // properties.フィールドコード.lookup.filterCond	文字列	絞り込みの初期設定
    [JsonPropertyName("filterCond")]
    [ColumnEx("filterCond", Order = 111, TypeName = "TEXT")]
    public string FilterCond { get; set; } = string.Empty;

    // properties.フィールドコード.lookup.sort	文字列	ソートの初期設定
    [JsonPropertyName("sort")]
    [ColumnEx("sort", Order = 112, TypeName = "TEXT")]
    public string Sort { get; set; } = string.Empty;

    // properties.フィールドコード.lookup.lookupPickerFields 配列	「コピー元のレコードの選択時に表示するフィールド」の設定を表すフィールドコードの一覧
    [JsonPropertyName("lookupPickerFields")]
    [ColumnEx("lookupPickerFields", Order = 120, TypeName = "TEXT")]
    public StringValueList ListLookupPickerField { get; set; } = new();

    // properties.フィールドコード.lookup.fieldMappings    配列  「ほかのフィールドのコピー」の設定の一覧
    [JsonPropertyName("fieldMappings")]
    [ColumnEx("fieldMappings", Order = 200, TypeName = "TEXT")]
    public FieldMappingValueList ListFieldMapping { get; set; } = new();

    public override string ToString()
    {
        return $"{RelatedApp.ToString()}:{RelatedKeyField.ToString()}";
    }
}
