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
/// https://cybozu.dev/ja/kintone/docs/rest-api/apps/settings/get-action-settings/
/// </summary>
internal class AppActionValueBase: BaseToData
{
    //actions.アクション名.id	文字列	アクションID
    [JsonPropertyName("id")]
    [ColumnEx("id", Order = 2, TypeName = "TEXT",IsUnique =true)]
    public string Id { get; set; } = string.Empty;

    #region NoJson
    /// <summary>
    /// propertiesのKey
    /// </summary>
    [ColumnEx("key", Order = 1, TypeName = "TEXT", IsUnique = true)]
    public string Key { get; set; } = string.Empty;  // 実利用はなし
    #endregion
}
