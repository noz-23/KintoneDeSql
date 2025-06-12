/*
 * Reprise Report Log Analyzer
 * Copyright (c) 2025 noz-23
 *  https://github.com/noz-23/
 * 
 * Licensed under the MIT License 
 * 
 */
using KintoneDeSql.Attributes;
using KintoneDeSql.Data;
using System.Text.Json.Serialization;

namespace KintoneDeSql.Responses.Apis;

/// <summary>
/// https://cybozu.dev/ja/kintone/docs/rest-api/apis/get-apis/
/// </summary>
internal class ApiResponse : BaseToData
{
    // apis.APIのID.link	文字列	APIのスキーマ情報を取得するためのAPIのURL
    [JsonPropertyName("link")]
    [ColumnEx("link", Order = 11, TypeName = "TEXT")]
    public string Link { get; set; } = string.Empty;
}
