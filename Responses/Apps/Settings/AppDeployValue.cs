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

namespace KintoneDeSql.Responses.Apps.Settings;

/// <summary>
/// https://cybozu.dev/ja/kintone/docs/rest-api/apps/settings/get-app-deploy-status/
/// </summary>
internal class AppDeployValue:BaseToData
{
    //apps[].app	文字列	アプリID
    [JsonPropertyName("app")]
    [ColumnEx("app", Order = 10, TypeName = "TEXT", IsUnique = true)]
    public string App { get; set; } = string.Empty;

    //apps[].status	文字列	処理の進捗
    [JsonPropertyName("status")]
    [ColumnEx("status", Order = 11, TypeName = "TEXT")]
    public string Status { get; set; } = string.Empty;
}

