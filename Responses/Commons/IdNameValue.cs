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

namespace KintoneDeSql.Responses.Commons;
//internal class SpaceValue : IdNameValue
//{
//    [JsonPropertyName("id")]
//    [ColumnEx("id", Order = 1, TypeName = "TEXT", IsUnique =true)]
//    public override string Id { get; set; } = string.Empty;
//}

/// <summary>
/// https://cybozu.dev/ja/kintone/docs/rest-api/spaces/get-space/
/// https://cybozu.dev/ja/kintone/docs/rest-api/plugins/get-plugin-apps/
/// </summary>

internal class IdNameValue : BaseToData
{
    //id	文字列	スペースID
    //apps[].id	文字列	アプリID
    [JsonPropertyName("id")]
    [ColumnEx("id", Order = 1, TypeName = "TEXT")]
    public virtual string Id { get; set; } = string.Empty;

    //name	文字列	スペース名
    //apps[].name	文字列	アプリ名
    [JsonPropertyName("name")]
    [ColumnEx("name", Order = 2, TypeName = "TEXT")]
    public string Name { get; set; } = string.Empty;
}
