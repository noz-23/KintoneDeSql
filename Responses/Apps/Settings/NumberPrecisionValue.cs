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

namespace KintoneDeSql.Responses.Apps.Settings;

/// <summary>
/// https://cybozu.dev/ja/kintone/docs/rest-api/apps/settings/get-general-settings/
/// </summary>
internal class NumberPrecisionValue : BaseToData
{
    //numberPrecision.digits	文字列	全体の桁数
    [JsonPropertyName("digits")]
    [ColumnEx("digits", Order = 301, TypeName = "TEXT")]
    public string Digits { get; set; } = string.Empty;

    //numberPrecision.decimalPlaces	文字列	小数部の桁数
    [JsonPropertyName("decimalPlaces")]
    [ColumnEx("decimalPlaces", Order = 302, TypeName = "TEXT")]
    public string DecimalPlaces { get; set; } = string.Empty;

    //numberPrecision.roundingMode	文字列	数値の丸めかた
    [JsonPropertyName("roundingMode")]
    [ColumnEx("roundingMode", Order = 303, TypeName = "TEXT")]
    public string RoundingMode { get; set; } = string.Empty;

    public override string ToString()
    {
        return $"{Digits.ToString()},{DecimalPlaces.ToString()},{RoundingMode.ToString()}";
    }

}
