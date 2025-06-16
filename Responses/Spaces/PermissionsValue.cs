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

namespace KintoneDeSql.Responses.Spaces;

/// <summary>
/// https://cybozu.dev/ja/kintone/docs/rest-api/spaces/get-space/
/// </summary>
internal class PermissionsValue : BaseToData
{
    //permissions.createApp	文字列	アプリを作成できるユーザー
    [JsonPropertyName("createApp")]
    [ColumnEx("createApp", Order = 1, TypeName = "TEXT")]
    public string CreateApp { get; set; } = string.Empty;

    public override string ToString()
    {
        return CreateApp.ToString();
    }
}