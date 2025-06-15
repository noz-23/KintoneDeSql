/*
 * Kintone De Sql
 * Copyright (c) 2025 noz-23
 *  https://github.com/noz-23/
 * 
 * Licensed under the MIT License 
 * 
 */
using KintoneDeSql.Data;
using System.Text.Json.Serialization;

namespace KintoneDeSql.Responses.Records;

/// <summary>
/// https://cybozu.dev/ja/kintone/docs/rest-api/records/get-records/
/// </summary>
internal class RecordFieldValue
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
    public BaseFieldValue Value
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
    public IList<string> ListValue(bool withCamma_) =>Value?.ListValue(withCamma_) ?? new List<string>();
    public IList<ColumnData> ListColumn() => Value?.ListColumn() ?? new List<ColumnData>();

}