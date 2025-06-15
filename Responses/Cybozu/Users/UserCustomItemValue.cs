/*
 * Kintone De Sql
 * Copyright (c) 2025 noz-23
 *  https://github.com/noz-23/
 * 
 * Licensed under the MIT License 
 * 
 */
using KintoneDeSql.Responses.Records;
using System.Text.Json.Serialization;

namespace KintoneDeSql.Responses.Cybozu.Users;

/// <summary>
/// https://cybozu.dev/ja/common/docs/user-api/overview/data-structure/#user
/// </summary>
internal class UserCustomItemValue: CodeNameValue
{
    //customItemValues    配列（CustomItemValue型）
    [JsonPropertyName("customItemValues")]
    public CustomItemValueList ListCustomItemValue { get; set; } = new();
}
