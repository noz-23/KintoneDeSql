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

internal class DestApp : BaseToData
{
}

internal class RelatedAppValue : BaseToData
{ 
}

internal class AppValue : BaseToData
{
    // properties.フィールドコード.referenceTable.relatedApp.app 文字列	「参照するアプリ」に指定されたアプリのアプリID
    [JsonPropertyName("app")]
    [ColumnEx("app", Order = 10, TypeName = "TEXT")]
    public string App { get; set; } = string.Empty;

    // properties.フィールドコード.referenceTable.relatedApp.code  文字列 「参照するアプリ」に指定されたアプリのアプリコード
    [JsonPropertyName("code")]
    [ColumnEx("code", Order = 11, TypeName = "TEXT")]
    public string Code { get; set; } = string.Empty;
}
