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

namespace KintoneDeSql.Responses.Apps.Views;

/// <summary>
/// https://cybozu.dev/ja/kintone/docs/rest-api/apps/view/get-views/
/// </summary>
internal class AppViewValueBase : BaseToData
{
    // views.一覧名.id 文字列 一覧のID
    [JsonPropertyName("id")]
    [ColumnEx("id", Order = 3, TypeName = "TEXT", IsUnique = true)]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// 再帰処理で保存
    /// </summary>
    // views.一覧名.fields    配列 表示するフィールドのフィールドコードの一覧
    [JsonPropertyName("fields")]
    public FieldValueList ListField { get; set; } = new();

    #region NO JSON
    [ColumnEx("key", Order = 2, TypeName = "TEXT",IsUnique =true)]
    public string Key { get; set; } = string.Empty;
    #endregion
}
