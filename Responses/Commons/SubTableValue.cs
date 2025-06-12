/*
 * Kintone De Sql
 * Copyright (c) 2025 noz-23
 *  https://github.com/noz-23/
 * 
 * Licensed under the MIT License 
 * 
 */
using KintoneDeSql.Attributes;
using KintoneDeSql.Responses.Records;
using System.Text.Json.Serialization;

namespace KintoneDeSql.Responses.Commons;

internal class SubTableValue: BaseFieldValue
{
    // "id": "48290",
    [JsonPropertyName("id")]
    [ColumnEx("id", Order = 10, TypeName = "TEXT")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("value")]
    public Dictionary<string, RecordFieldResponse> ListRecord { get; set; } = new();


    public override List<string> ListInsertHeaderBase(bool withCamma_)
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

    public override List<string> ListValue(bool withCamma_)
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

}

