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

internal class LineSizeValue : BaseToData
{
    // layout[].fields[].size.width	文字列	ピクセル単位でのフィールドの横幅
    [JsonPropertyName("width")]
    [ColumnEx("width", Order = 10, TypeName = "TEXT")]
    public string Width { get; set; } = string.Empty;


    // layout[].fields[].size.height	文字列	フィールド名を含めた、ピクセル単位でのフィールドの縦幅
    [JsonPropertyName("height")]
    [ColumnEx("height", Order = 11, TypeName = "TEXT")]
    public string Height { get; set; } = string.Empty;

    // layout[].fields[].size.innerHeight	文字列	フィールド名を除いた、ピクセル単位でのフィールドの縦幅
    [JsonPropertyName("innerHeight")]
    [ColumnEx("innerHeight", Order = 12, TypeName = "TEXT")]
    public string InnerHeight { get; set; } = string.Empty;
}
