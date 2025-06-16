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
using static System.Net.Mime.MediaTypeNames;

namespace KintoneDeSql.Responses.Records;

/// <summary>
/// https://cybozu.dev/ja/kintone/docs/rest-api/records/evaluate-record-permissions/
/// </summary>
internal class FieldAlcValue : BaseToData
{
    //rights[].fields.フィールドコード.viewable	真偽値	フィールドの閲覧が可能かどうか
    [JsonPropertyName("viewable")]
    [ColumnEx("viewable", Order = 100, TypeName = "NUMERIC")]
    public bool Viewable { get; set; } = false;

    //rights[].fields.フィールドコード.editable	真偽値	フィールドの編集が可能かどうか
    [JsonPropertyName("editable")]
    [ColumnEx("editable", Order = 101, TypeName = "NUMERIC")]
    public bool Editable { get; set; } = false;

    #region NoJson
    [ColumnEx("key", Order = 2, TypeName = "TEXT")]
    public string FieldKey { get; set; } = string.Empty;
    #endregion

    public override string ToString()
    {
        return FieldKey.ToString();
    }
}
