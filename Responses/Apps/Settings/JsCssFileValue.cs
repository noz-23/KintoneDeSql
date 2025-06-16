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
using System.Security.Policy;
using System.Text.Json.Serialization;

namespace KintoneDeSql.Responses.Apps.Settings;

/// <summary>
/// https://cybozu.dev/ja/kintone/docs/rest-api/apps/settings/get-customization/
/// </summary>

internal class DeskTopFileValue : JsCssFileValue
{
    public override string FileType { get; set; } = "desktop";
}
internal class MobileFileValue : JsCssFileValue
{
    public override string FileType { get; set; } = "mobile";
}
internal class JsCssFileValue : BaseToData
{
    //desktop.js	配列	JavaScriptファイルの一覧
    //mobile.js	配列	JavaScriptファイルの一覧
    [JsonPropertyName("js")]
    public List<JsFileValue> ListJs { get; set; } = new();

    //desktop.css	配列	CSSファイルの一覧
    //mobile.css	配列	CSSファイルの一覧
    [JsonPropertyName("css")]
    public List<CssFileValue> ListCss { get; set; } = new();

    #region NoJson
    /// <summary>
    /// js/css
    /// </summary>
    [ColumnEx("fileType", Order = 50, TypeName = "TEXT")]
    public virtual string FileType { get; set; } = string.Empty;
    #endregion
    public override string ToString()
    {
        return FileType.ToString();
    }
}