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

namespace KintoneDeSql.Responses.Records;

/// <summary>
/// https://cybozu.dev/ja/kintone/docs/rest-api/records/evaluate-record-permissions/
/// </summary>
internal class RecordAclEvaluateListValueBase : BaseToData
{
    //rights[].id	文字列	レコードID
    [JsonPropertyName("id")]
    [ColumnEx("id", Order = 1, TypeName = "TEXT")]
    public string Id { get; set; } = string.Empty;

}
