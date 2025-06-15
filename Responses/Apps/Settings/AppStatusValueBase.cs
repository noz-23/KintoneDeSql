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
/// https://cybozu.dev/ja/kintone/docs/rest-api/apps/settings/get-process-management-settings/
/// </summary>
internal class AppStatusValueBase : BaseToData
{
    //states.ステータス名.assignee	オブジェクト	ステータスの作業者
    [JsonPropertyName("assignee")]
    [ColumnEx("assignee", Order = 100, IsExtract =true)]
    public AssigneeValue Assignee { get; set; } = new();

    #region NoJson
    /// <summary>
    /// propertiesのKey
    /// </summary>
    [ColumnEx("key", Order = 3, TypeName = "TEXT", IsUnique = true)]
    public string Key { get; set; } = string.Empty;  // 実利用はなし
    #endregion

}
