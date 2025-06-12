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
using KintoneDeSql.Responses.Commons;
using System.Text.Json.Serialization;

namespace KintoneDeSql.Responses.Records;

/// <summary>
/// https://cybozu.dev/ja/kintone/docs/rest-api/records/get-comments/
/// </summary>
internal class CommentResponse : BaseToData
{
    //comments[].id	文字列	コメントID
    [JsonPropertyName("id")]
    [ColumnEx("id", Order = 10, TypeName = "TEXT", IsUnique = true)]
    public string Id { get; set; } = string.Empty;

    //comments[].text	文字列	コメントの文字列
    [JsonPropertyName("text")]
    [ColumnEx("text", Order = 11, TypeName = "TEXT")]
    public string Text { get; set; } = string.Empty;

    //comments[].createdAt	文字列	投稿日時
    [JsonPropertyName("createdAt")]
    [ColumnEx("createdAt", Order = 12, TypeName = "TEXT")]
    public string CreatedAt { get; set; } = string.Empty;

    //comments[].creator	オブジェクト	投稿者の情報
    [JsonPropertyName("creator")]
    [ColumnEx("creator", Order = 20, IsExtract = true)]
    public ItemValue? Creator { get; set; } = new();

    //comments[].mentions	配列	宛先情報

    [JsonPropertyName("mentions")]
    public List<EntryValue> Mentions { get; set; } = new();
}