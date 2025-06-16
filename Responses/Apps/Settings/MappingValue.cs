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
using Microsoft.VisualBasic.FileIO;
using System.Text.Json.Serialization;

namespace KintoneDeSql.Responses.Apps.Settings;

/// <summary>
/// https://cybozu.dev/ja/kintone/docs/rest-api/apps/settings/get-action-settings/
/// </summary>
internal class MappingValue : BaseToData
{
    //actions.アクション名.mappings[].srcType	文字列	コピー元の種類
    [JsonPropertyName("srcType")]
    [ColumnEx("srcType", Order = 1, TypeName = "TEXT")]
    public string SrcType { get; set; } = string.Empty;

    //actions.アクション名.mappings[].srcField	文字列	コピー元に指定されたフィールドのフィールドコード
    [JsonPropertyName("srcField")]
    [ColumnEx("srcField", Order = 2, TypeName = "TEXT")]
    public string SrcField { get; set; } = string.Empty;

    //actions.アクション名.mappings[].destField	文字列	コピー先に指定されたフィールドのフィールドコード
    [JsonPropertyName("destField")]
    [ColumnEx("destField", Order = 3, TypeName = "TEXT")]
    public string DestField { get; set; } = string.Empty;
    public override string ToString()
    {
        return $"{SrcType.ToString()} -> {DestField.ToString()}";
    }
}
