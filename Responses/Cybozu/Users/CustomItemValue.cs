﻿/*
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

namespace KintoneDeSql.Responses.Cybozu.Users;

/// <summary>
/// https://cybozu.dev/ja/common/docs/user-api/users/get-users/
/// https://cybozu.dev/ja/common/docs/user-api/overview/data-structure/#custom-item-value
/// </summary>
internal class CustomItemValue : BaseToData
{
    // code	文字列	コード
    [JsonPropertyName("code")]
    [ColumnEx("code", Order = 100, TypeName = "TEXT")]
    public string Code { get; set; } = string.Empty;

    // value 文字列 値
    [JsonPropertyName("value")]
    [ColumnEx("value", Order = 101, TypeName = "TEXT")]
    public string Value { get; set; } = string.Empty;

    #region NoJson
    /// <summary>
    /// 上位のUser ID
    /// </summary>
    [ColumnEx("id", Order = 10, TypeName = "TEXT")]
    public string Id { get; set; } = string.Empty;
    #endregion
}
