/*
 * Kintone De Sql
 * Copyright (c) 2025 noz-23
 *  https://github.com/noz-23/
 * 
 * Licensed under the MIT License 
 * 
 */
using KintoneDeSql.Attributes;
using KintoneDeSql.Responses.Apps;
using KintoneDeSql.Responses.Commons;
using System.Text.Json.Serialization;

namespace KintoneDeSql.Responses.Apis;

/// <summary>
/// https://cybozu.dev/ja/kintone/docs/rest-api/apis/get-api-schema/
/// </summary>
internal class ApiSchemaValueRequired: ApiSchemaValueProperty
{
    [JsonPropertyName("required")]
    [ColumnEx("required", Order = 50, TypeName = "TEXT", IsUnique = true)]
    public RequiredValueList ListRequired { get; set; } = new();
}
