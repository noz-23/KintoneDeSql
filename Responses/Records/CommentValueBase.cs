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
/// https://cybozu.dev/ja/kintone/docs/rest-api/records/get-comments/
/// </summary>
internal class CommentValueBase : BaseToData
{
    //comments[].id	文字列	コメントID
    [JsonPropertyName("id")]
    [ColumnEx("id", Order = 1, TypeName = "TEXT", IsUnique = true)]
    public string Id { get; set; } = string.Empty;
}
