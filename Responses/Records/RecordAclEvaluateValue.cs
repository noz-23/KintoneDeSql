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
internal class RecordAclEvaluateValue : BaseToData
{
    //rights[].record.viewable	真偽値	レコードの閲覧が可能かどうか
    [JsonPropertyName("viewable")]
    [ColumnEx("viewable", Order = 1, TypeName = "NUMERIC")]
    public bool Viewable { get; set; } = false;

    //rights[].record.editable	真偽値	レコードの編集が可能かどうか
    [JsonPropertyName("editable")]
    [ColumnEx("editable", Order = 2, TypeName = "NUMERIC")]
    public bool Editable { get; set; } = false;

    //rights[].record.deletable	真偽値	レコードの削除が可能かどうか
    [JsonPropertyName("deletable")]
    [ColumnEx("deletable", Order = 3, TypeName = "NUMERIC")]
    public bool Deletable { get; set; } = false;

}
