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
internal class TitleFieldValue : BaseToData
{
    //titleField.selectionMode	文字列	レコードのタイトルとして利用するフィールドの選択方法
    [JsonPropertyName("selectionMode")]
    [ColumnEx("selectionMode", Order = 1, TypeName = "TEXT")]
    public string SelectionMode { get; set; } = string.Empty;

    //titleField.code	文字列	レコードのタイトルとして利用するフィールドのフィールドコード
    [JsonPropertyName("code")]
    [ColumnEx("code", Order = 2, TypeName = "TEXT")]
    public string Code { get; set; } = string.Empty;
}
