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

namespace KintoneDeSql.Responses.Cybozu.Users;

/// <summary>
/// https://cybozu.dev/ja/common/docs/user-api/users/get-users-services/
/// </summary>
internal class UsersServiceValue:BaseToData
{
    //users[].code 文字列 ユーザーのログイン名
    [JsonPropertyName("code")]
    [ColumnEx("code", Order = 1, TypeName = "TEXT")]
    public string Code { get; set; } = string.Empty;

    //users[].services 配列（文字列）	ユーザーの利用サービスの一覧

    [JsonPropertyName("services")]
    [ColumnEx("services", Order = 2, TypeName = "TEXT")]
    public StringValueList ListService { get; set; } = new ();
}
