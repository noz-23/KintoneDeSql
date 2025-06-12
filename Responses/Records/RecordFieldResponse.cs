/*
 * Kintone De Sql
 * Copyright (c) 2025 noz-23
 *  https://github.com/noz-23/
 * 
 * Licensed under the MIT License 
 * 
 */
using KintoneDeSql.Attributes;
using System.Text.Json.Serialization;

namespace KintoneDeSql.Responses.Records;

internal class RecordFieldResponse
{
    // record オブジェクト  取得したレコードの情報
    [JsonPropertyName("type")]
    public string Type { get; set; } = string.Empty;

    /// <summary>
    /// フィールドの値
    /// デバッグも含め一旦JSON文字列で取得
    /// </summary>
    [JsonPropertyName("value")]
    [JsonConverter(typeof(ValueConvert))]
    public string ValueJson { get; set; } = string.Empty;

    /// <summary>
    /// フィールドの値
    /// 変換後の値
    /// </summary>
    public BaseFieldValue? Value
    {
        get
        {
            _value ??= BaseFieldValue.Get(Type , ValueJson);
            return _value;
        }
    }
    private BaseFieldValue? _value = null;

    public override string ToString()
    {
        return Value?.ToString() ?? ValueJson;
    }
    public List<string> ListValue(bool withCamma_) =>Value?.ListValue(withCamma_) ??new();
    public List<KeyValuePair<ColumnExAttribute, string>> ListColumn() => Value?.ListColumn() ?? new();

}