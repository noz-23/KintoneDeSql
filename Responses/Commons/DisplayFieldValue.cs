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

internal class DisplayFieldValue : BaseToData
{
    // properties.フィールドコード.referenceTable.sort 文字列 レコードのソートの設定
    [JsonPropertyName("sort")]
    [ColumnEx("sort", Order = 10, TypeName = "TEXT")]
    public string Sort { get; set; } = string.Empty;

    // properties.フィールドコード.referenceTable.size 文字列 一度に表示する最大レコード数
    [JsonPropertyName("size")]
    [ColumnEx("size", Order = 11, TypeName = "TEXT")]
    public string Size { get; set; } = string.Empty;

    public override string ToString()
    {
        return Size.ToString();
    }
}
