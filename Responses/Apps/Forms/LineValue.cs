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
using Microsoft.SqlServer.Server;
using System.Text.Json.Serialization;

namespace KintoneDeSql.Responses.Apps.Forms;

/// <summary>
/// https://cybozu.dev/ja/kintone/docs/rest-api/apps/form/get-form-layout/
/// </summary>
internal class LineValue : BaseToData
{
    // layout[].fields[].type	文字列	フィールドの種類
    [JsonPropertyName("type")]
    [ColumnEx("type", Order = 10, TypeName = "TEXT")]
    public string Type { get; set; } = string.Empty;

    // layout[].fields[].code	文字列	フィールドコード
    [JsonPropertyName("code")]
    [ColumnEx("code", Order = 11, TypeName = "TEXT")]
    public string Code { get; set; } = string.Empty;

    // layout[].fields[].label	文字列	ラベル名
    [JsonPropertyName("label")]
    [ColumnEx("label", Order = 12, TypeName = "TEXT")]
    public string Lable { get; set; } = string.Empty;

    // layout[].fields[].elementId	文字列	要素ID
    [JsonPropertyName("elementId")]
    [ColumnEx("elementId", Order = 13, TypeName = "TEXT")]
    public string ElementId { get; set; } = string.Empty;

    // layout[].fields[].size	オブジェクト	フィールドのサイズ
    [JsonPropertyName("size")]
    [ColumnEx("size", Order = 20, TypeName = "TEXT", IsExtract = true)]
    public LineSizeValue Size { get; set; } = new();

    #region NoJson
    [ColumnEx("column", Order = 4, TypeName = "TEXT")]
    public string Column { get; set; } = string.Empty;
    #endregion

    public override string ToString()
    {
        return $"{Code.ToString()}:{Size.ToString()}";
    }
}
