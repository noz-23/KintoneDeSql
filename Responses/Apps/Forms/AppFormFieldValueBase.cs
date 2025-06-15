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

namespace KintoneDeSql.Responses.Apps.Forms;
/// <summary>
/// https://cybozu.dev/ja/kintone/docs/rest-api/apps/form/get-form-fields/
/// </summary>

internal class AppFormFieldValueBase: BaseToData
{
    // properties.フィールドコード.fields オブジェクト  テーブル内のフィールド
    [JsonPropertyName("fields")]
    public Dictionary<string, AppFormFieldValue> ListField { get; set; } = new();

    /// <summary>
    /// このフィールド値
    /// </summary>
    /// <param name="withCamma_"></param>
    /// <returns></returns>
    public IList<string> ListValueBase(bool withCamma_)=>this.ListValue(withCamma_, typeof(AppFormFieldValueBase));

    #region NoJson
    /// <summary>
    /// 上位の
    /// </summary>
    [ColumnEx("appId", Order = 1, TypeName = "TEXT",IsUnique =true)]
    public string AppId { get; set; } = string.Empty;

    /// <summary>
    /// propertiesのKey
    /// </summary>
    [ColumnEx("key", Order = 2, TypeName = "TEXT", IsUnique = true)]
    public string MainKey { get; set; } = string.Empty;  // 実利用はなし

    /// <summary>
    /// propertiesのKey
    /// </summary>
    [ColumnEx("field_key", Order = 3, TypeName = "TEXT",IsUnique =true)]
    public string SubKey { get; set; } = string.Empty;  // 実利用はなし

    #endregion

}
