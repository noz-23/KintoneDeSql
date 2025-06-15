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
internal class SpaceResponseBase: BaseToData
{
    //id	文字列	スペースID
    //apps[].id	文字列	アプリID
    [JsonPropertyName("id")]
    [ColumnEx("id", Order = 1, TypeName = "TEXT",IsUnique =true)]
    public virtual string Id { get; set; } = string.Empty;

    //defaultThread	文字列	スペースが作成されたときに初期作成されたスレッドのスレッドID
    [JsonPropertyName("defaultThread")]
    [ColumnEx("defaultThread", Order = 2, TypeName = "TEXT")]
    public string DefaultThread { get; set; } = string.Empty;


}
