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

internal class EntryValue : BaseToData
{
    // properties.フィールドコード.entities[].code 文字列 選択肢のユーザーのログイン名、またはグループや組織のコード
    // comments[].mentions[].code	文字列	宛先のユーザー／組織／グループコード
    [JsonPropertyName("code")]
    [ColumnEx("code", Order = 10, TypeName = "TEXT")]
    public string Code { get; set; } = string.Empty;

    // properties.フィールドコード.entities[].type 文字列 値の種類
    // comments[].mentions[].type	文字列	宛先のユーザー／組織／グループの種類
    [JsonPropertyName("type")]
    [ColumnEx("type", Order = 11, TypeName = "TEXT")]
    public string Type { get; set; } = string.Empty;
}
