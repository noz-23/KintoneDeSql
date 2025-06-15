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

namespace KintoneDeSql.Responses.Spaces;

/// <summary>
/// https://cybozu.dev/ja/kintone/docs/rest-api/spaces/get-space-members/
/// </summary>
internal class SpaceMemberValue:BaseToData
{
    //members[].entity	オブジェクト	スペースのメンバーの情報
    //members[].entity.type	文字列	スペースのメンバーの種類
    //members[].entity.code	文字列	スペースのメンバーのコード
    [JsonPropertyName("entity")]
    [ColumnEx("entity", Order = 10, TypeName = "TEXT", IsExtract = true)]
    public EntityValue Entity { get; set; } = new();

    //members[].isAdmin	真偽値	スペースのメンバーがスペース管理者かどうか
    [JsonPropertyName("isAdmin")]
    [ColumnEx("isAdmin", Order = 100, TypeName = "NUMERIC")]
    public bool IsAdmin { get; set; } = false;

    //members[].isImplicit	真偽値	ユーザーとして追加されているかどうか
    [JsonPropertyName("isImplicit")]
    [ColumnEx("isImplicit", Order = 101, TypeName = "NUMERIC")]
    public bool IsImplicit { get; set; } = false;

    //members[].includeSubs	真偽値	下位組織を含むかどうか
    [JsonPropertyName("includeSubs")]
    [ColumnEx("includeSubs", Order = 102, TypeName = "NUMERIC")]
    public bool IncludeSubs { get; set; } = false;
}
