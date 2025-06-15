/*
 * Kintone De Sql
 * Copyright (c) 2025 noz-23
 *  https://github.com/noz-23/
 * 
 * Licensed under the MIT License 
 * 
 */
using KintoneDeSql.Attributes;
using System.Text.Json.Serialization;

namespace KintoneDeSql.Responses.Records;

/// <summary>
/// https://cybozu.dev/ja/kintone/docs/rest-api/records/get-comments/
/// </summary>
internal class CommentValue : CommentValueBase
{
    //comments[].text	文字列	コメントの文字列
    [JsonPropertyName("text")]
    [ColumnEx("text", Order = 11, TypeName = "TEXT")]
    public string Text { get; set; } = string.Empty;

    //comments[].createdAt	文字列	投稿日時
    [JsonPropertyName("createdAt")]
    [ColumnEx("createdAt", Order = 100, TypeName = "TEXT")]
    public string CreatedAt { get; set; } = string.Empty;

    //comments[].creator	オブジェクト	投稿者の情報
    [JsonPropertyName("creator")]
    [ColumnEx("creator", Order = 200, TypeName = "TEXT", IsExtract = true)]
    public CreatorValue Creator { get; set; } = new();

    //comments[].mentions	配列	宛先情報
    [JsonPropertyName("mentions")]
    [ColumnEx("mentions", Order = 500, TypeName = "TEXT")]
    public MentionValueList ListMention { get; set; } = new();
}