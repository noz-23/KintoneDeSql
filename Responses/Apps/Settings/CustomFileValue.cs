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

namespace KintoneDeSql.Responses.Apps.Settings;

/// <summary>
/// https://cybozu.dev/ja/kintone/docs/rest-api/apps/settings/get-customization/
/// </summary>
internal class JsFileValue : CustomFileValue
{
    public override string FileType { get; set; } = "js";
}

internal class CssFileValue : CustomFileValue
{
    public override string FileType { get; set; } = "css";
}

internal class CustomFileValue : BaseToData
{
    //desktop.js[].type	文字列	ファイルの指定方法
    //desktop.css[].type	文字列	ファイルの指定方法
    //mobile.js[].type	文字列	ファイルの指定方法
    //mobile.css[].type	文字列	ファイルの指定方法
    [JsonPropertyName("type")]
    [ColumnEx("type", Order = 100, TypeName = "TEXT")]
    public string Type { get; set; } = string.Empty;

    //desktop.js[].url	文字列	ファイルのURL
    //desktop.css[].url	文字列	ファイルのURL
    //mobile.js[].url	文字列	ファイルのURL
    //mobile.css[].url	文字列	ファイルのURL
    [JsonPropertyName("url")]
    [ColumnEx("url", Order = 101, TypeName = "TEXT", IsUnique = true)]
    public string Url { get; set; } = string.Empty;

    //desktop.js[].file	オブジェクト	添付されたファイルの情報
    //desktop.js[].file.contentType	文字列	MIMEタイプ
    //desktop.js[].file.fileKey	文字列	ファイルキー
    //desktop.js[].file.name	文字列	ファイル名
    //desktop.js[].file.size	文字列	ファイルのサイズ
    //desktop.css[].file	オブジェクト	添付されたファイルの情報
    //desktop.css[].file.contentType	文字列	MIMEタイプ
    //desktop.css[].file.fileKey	文字列	ファイルキー
    //desktop.css[].file.name	文字列	ファイル名
    //desktop.css[].file.size	文字列	ファイルのサイズ
    //mobile.js[].file	オブジェクト	添付されたファイルの情報
    //mobile.js[].file.contentType	文字列	MIMEタイプ
    //mobile.js[].file.fileKey	文字列	ファイルキー
    //mobile.js[].file.name	文字列	ファイル名
    //mobile.js[].file.size	文字列	ファイルのサイズ
    //mobile.css[].file	オブジェクト	添付されたファイルの情報
    //mobile.css[].file.contentType	文字列	MIMEタイプ
    //mobile.css[].file.fileKey	文字列	ファイルキー
    //mobile.css[].file.name	文字列	ファイル名
    //mobile.css[].file.size	文字列	ファイルのサイズ

    [JsonPropertyName("file")]
    [ColumnEx("file", Order = 200, TypeName = "TEXT", IsExtract = true)]
    public FileFieldValue File { get; set; } = new();

    #region NoJson
    /// <summary>
    /// desktop/mobile
    /// </summary>
    [ColumnEx("fileType", Order = 500, TypeName = "TEXT")]
    public virtual string FileType { get; set; } = string.Empty;
    #endregion
    public override string ToString()
    {
        return $"{File.ToString()}{Url.ToString()}";
    }
}