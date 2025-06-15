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

namespace KintoneDeSql.Responses.Apis;

/// <summary>
/// https://cybozu.dev/ja/kintone/docs/rest-api/apis/get-api-schema/
/// </summary>
internal class ApiSchemaValueProperty: BaseToData
{
    [JsonPropertyName("properties")]
    public Dictionary<string, ApiSchemaValue> ListProperty { get; set; } = new();

    #region NO JSON
    [ColumnEx("key", Order = 20, TypeName = "TEXT",IsUnique =true)]
    public string Key { get; set; } = string.Empty;
    #endregion
}
