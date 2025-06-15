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
internal class RecordAclEvaluateListValue: RecordAclEvaluateListValueBase
{
    //rights[].record	オブジェクト	指定したレコードのアクセス権の設定
    [JsonPropertyName("record")]
    [ColumnEx("record", Order = 10, TypeName = "TEXT", IsExtract =true)]
    public RecordAclEvaluateValue Record { get; set; } = new();

    //rights[].fields	オブジェクト	レコードに存在するフィールドのアクセス権
    [JsonPropertyName("fields")]
    public Dictionary<string, FieldAlcValue> ListField { get; set; } = new();
}
