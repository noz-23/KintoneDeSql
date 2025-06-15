/*
 * Kintone De Sql
 * Copyright (c) 2025 noz-23
 *  https://github.com/noz-23/
 * 
 * Licensed under the MIT License 
 * 
 */
using KintoneDeSql.Attributes;
using KintoneDeSql.Extensions;
using KintoneDeSql.Responses.Records;
using System.Text.Json.Serialization;

namespace KintoneDeSql.Responses.Commons;

internal class SubTableFieldValue: BaseFieldValue
{
    // "id": "48290",
    [JsonPropertyName("id")]
    [ColumnEx("id", Order = 10, TypeName = "TEXT")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("value")]
    public Dictionary<string, RecordFieldValue> ListRecord { get; set; } = new();

    #region SubTable
    /// <summary>
    /// レコードからテーブル作成
    /// 基本は　FormField で作成するので未使用
    /// Api Key 利用時
    /// </summary>
    public override IList<string> ListSubCreateHeader(bool withCamma_)
    {
        var rtn = new List<string>();
        foreach (var pair in ListRecord)
        {
            if (pair.Value.Value == null)
            {
                continue;
            }

            if (ListToCreateHeader.TryGetValue(pair.Value.Value.FieldType, out var run) == true)
            {
                var list = run(pair.Value, pair.Key, withCamma_);
                rtn.AddRange(list);
            }
        }
        return rtn;
    }

    public override IList<string> ListSubInsertHeader(bool withCamma_)
    {
        var rtn = new List<string>();
        foreach (var pair in ListRecord)
        {
            if (pair.Value.Value == null)
            {
                continue;
            }

            if (ListToInsertHeader.TryGetValue(pair.Value.Value.FieldType, out var run) == true)
            {
                var list = run(pair.Value, pair.Key, withCamma_);
                rtn.AddRange(list);
            }
        }
        return rtn;
    }

    public override IList<string> ListValue(bool withCamma_)
    {
        var rtn = base.ListValue(withCamma_);

        foreach (var pair in ListRecord)
        {
            if (pair.Value.Value == null)
            {
                continue;
            }

            if (ListToValue.TryGetValue(pair.Value.Value.FieldType, out var run) == true)
            {
                rtn.AddRange(run(pair.Value, pair.Key, withCamma_));
            }
        }
        return rtn;
    }
    #endregion
}

