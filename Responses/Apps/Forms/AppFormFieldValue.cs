/*
 * Kintone De Sql
 * Copyright (c) 2025 noz-23
 *  https://github.com/noz-23/
 * 
 * Licensed under the MIT License 
 * 
 */
using KintoneDeSql.Attributes;
using KintoneDeSql.Responses.Commons;
using System.Text.Json.Serialization;

namespace KintoneDeSql.Responses.Apps.Forms;
/// <summary>
/// https://cybozu.dev/ja/kintone/docs/rest-api/apps/form/get-form-fields/
/// </summary>
internal class AppFormFieldValue : AppFormFieldListValue
{
    // properties.フィールドコード.label   文字列 フィールド名
    [JsonPropertyName("label")]
    [ColumnEx("label", Order = 11, TypeName = "TEXT")]
    public string Label { get; set; } = string.Empty;

    // properties.フィールドコード.code 文字列 フィールドコード
    [JsonPropertyName("code")]
    [ColumnEx("code", Order = 12, TypeName = "TEXT")]
    public string Code { get; set; } = string.Empty;

    // properties.フィールドコード.type 文字列 フィールドの種類
    [JsonPropertyName("type")]
    [ColumnEx("type", Order = 13, TypeName = "TEXT")]
    public string Type { get; set; } = string.Empty;

    // properties.フィールドコード.noLabel 真偽値 フィールド名を非表示にするかどうか
    [JsonPropertyName("nolabel")]
    [ColumnEx("nolabel", Order = 14, TypeName = "NUMERIC")]
    public bool NoLabel { get; set; } = false;

    // properties.フィールドコード.required 真偽値	入力が必須かどうか
    [JsonPropertyName("required")]
    [ColumnEx("required", Order = 15, TypeName = "NUMERIC")]
    public bool Required { get; set; } = false;

    // properties.フィールドコード.unique	真偽値	重複を禁止するかどうか
    [JsonPropertyName("unique")]
    [ColumnEx("unique", Order = 16, TypeName = "NUMERIC")]
    public bool Unique { get; set; } = false;

    // properties.フィールドコード.maxValue 文字列 最大値
    [JsonPropertyName("maxValue")]
    [ColumnEx("maxValue", Order = 17, TypeName = "TEXT")]
    public string MaxValue { get; set; } = string.Empty;

    // properties.フィールドコード.minValue 文字列 最小値
    [JsonPropertyName("minValue")]
    [ColumnEx("minValue", Order = 18, TypeName = "TEXT")]
    public string MinValue { get; set; } = string.Empty;
    // properties.フィールドコード.maxLength 文字列 最大文字数
    [JsonPropertyName("maxLength")]
    [ColumnEx("maxLength", Order = 19, TypeName = "TEXT")]
    public string MaxLength { get; set; } = string.Empty;

    // properties.フィールドコード.minLength 文字列 最小文字数
    [JsonPropertyName("minLength")]
    [ColumnEx("minLength", Order = 20, TypeName = "TEXT")]
    public string MinLength { get; set; } = string.Empty;

    // properties.フィールドコード.defaultValue 文字列または配列    初期値
    [JsonConverter(typeof(ValueConvert))]
    [JsonPropertyName("defaultValue")]
    [ColumnEx("defaultValue", Order = 21, TypeName = "TEXT")]
    public string DefaultValue { get; set; } = string.Empty;

    // properties.フィールドコード.defaultNowValue 真偽値 レコード登録時の日時を初期値にするかどうか
    [JsonPropertyName("defaultNowValue")]
    [ColumnEx("defaultNowValue", Order = 22, TypeName = "NUMERIC")]
    public bool DefaultNowValue { get; set; } = false;
    // properties.フィールドコード.align 文字列 選択肢の並び
    [JsonPropertyName("align")]
    [ColumnEx("align", Order = 23, TypeName = "TEXT")]
    public string Align { get; set; } = string.Empty;

    // properties.フィールドコード.expression	文字列	自動計算式
    [JsonPropertyName("expression")]
    [ColumnEx("expression", Order = 24, TypeName = "TEXT")]
    public string Expression { get; set; } = string.Empty;

    // properties.フィールドコード.hideExpression 真偽値 計算フィールドの計算式を非表示にするかどうか
    [JsonPropertyName("hideExpression")]
    [ColumnEx("hideExpression", Order = 25, TypeName = "NUMERIC")]
    public bool HideExpression { get; set; } = false;

    // properties.フィールドコード.digit 真偽値 数値の桁区切りを表示するかどうか
    [JsonPropertyName("digit")]
    [ColumnEx("digit", Order = 26, TypeName = "NUMERIC")]
    public bool Digit { get; set; } = false;

    // properties.フィールドコード.thumbnailSize	文字列	画像のサムネイルの大きさ（ピクセル単位）
    [JsonPropertyName("thumbnailSize")]
    [ColumnEx("thumbnailSize", Order = 27, TypeName = "TEXT")]
    public string ThumbnailSize { get; set; } = string.Empty;

    // properties.フィールドコード.protocol 文字列 リンクの種類
    [JsonPropertyName("protocol")]
    [ColumnEx("protocol", Order = 28, TypeName = "TEXT")]
    public string Protocol { get; set; } = string.Empty;

    // properties.フィールドコード.format	文字列	計算フィールドの表示形式
    [JsonPropertyName("format")]
    [ColumnEx("format", Order = 29, TypeName = "TEXT")]
    public string Format { get; set; } = string.Empty;

    //properties.フィールドコード.displayScale 文字列 小数点以下の表示桁数
    [JsonPropertyName("displayScale")]
    [ColumnEx("displayScale", Order = 30, TypeName = "TEXT")]
    public string DisplayScale { get; set; } = string.Empty;

    //properties.フィールドコード.unit 文字列 単位記号
    [JsonPropertyName("unit")]
    [ColumnEx("unit", Order = 31, TypeName = "TEXT")]
    public string Unit { get; set; } = string.Empty;

    //properties.フィールドコード.unitPosition 文字列 単位記号の表示位置
    [JsonPropertyName("unitPosition")]
    [ColumnEx("unitPosition", Order = 32, TypeName = "TEXT")]
    public string UnitPosition { get; set; } = string.Empty;

    // properties.フィールドコード.openGroup 真偽値 グループ内のフィールドを表示するかどうか
    [JsonPropertyName("openGroup")]
    [ColumnEx("openGroup", Order = 33, TypeName = "NUMERIC")]
    public bool OpenGroup { get; set; } = false;

    //properties.フィールドコード.enabled 真偽値 機能が有効かどうか
    [JsonPropertyName("enabled")]
    [ColumnEx("enabled", Order = 34, TypeName = "NUMERIC")]
    public bool Enabled { get; set; } = false;

    //properties.フィールドコード.referenceTable.relatedApp オブジェクト	「参照するアプリ」の設定
    [JsonPropertyName("relatedApp")]
    [ColumnEx("relatedApp", Order = 41, TypeName = "TEXT", IsExtract = true)]
    public ReferenceTableValue RelatedApp { get; set; } = new();

    // properties.フィールドコード.lookup	オブジェクト	ルックアップフィールドの設定
    [JsonPropertyName("lookup")]
    [ColumnEx("lookupt", Order = 42, TypeName = "TEXT", IsExtract = true)]
    public LookupValue Lookup { get; set; } = new();
}
