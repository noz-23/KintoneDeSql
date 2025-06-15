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

namespace KintoneDeSql.Responses.Apps.Forms;

internal class FieldMappingValue : BaseToData
{
    // properties.フィールドコード.lookup.fieldMappings[].field 文字列	「ほかのフィールドのコピー」のコピー先に指定されたフィールドのフィールドコード
    // properties.フィールドコード.referenceTable.condition.field 文字列	「表示するレコードの条件」で指定された、関連レコード一覧フィールドと同じアプリのフィールドのフィールドコード
    [JsonPropertyName("field")]
    [ColumnEx("field", Order = 10, TypeName = "TEXT", IsUnique =true)]
    public string Field { get; set; } = string.Empty;

    // properties.フィールドコード.lookup.fieldMappings[].relatedField 文字列	「ほかのフィールドのコピー」のコピー元に指定されたフィールドのフィールドコード
    // properties.フィールドコード.referenceTable.condition.relatedField   文字列 「表示するレコードの条件」で指定された、関連レコード一覧フィールドが参照するアプリのフィールドのフィールドコード
    [JsonPropertyName("relatedField")]
    [ColumnEx("relatedField", Order = 11, TypeName = "TEXT", IsUnique = true)]
    public string RelatedField { get; set; } = string.Empty;

    public override string ToString()
    {
        return $"{Field} -> {RelatedField}";
    }
}
