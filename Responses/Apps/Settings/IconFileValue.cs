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
using System.Security.Policy;
using System.Text.Json.Serialization;

namespace KintoneDeSql.Responses.Apps.Settings;

/// <summary>
/// https://cybozu.dev/ja/kintone/docs/rest-api/apps/settings/get-general-settings/
/// </summary>
internal class IconFileValue : BaseToData
{
    //icon.type	文字列	アイコンの種類
    [JsonPropertyName("type")]
    [ColumnEx("type", Order = 1, TypeName = "TEXT")]
    public string Type { get; set; } = string.Empty;

    //icon.key	文字列	アイコンのキー（識別子）
    [JsonPropertyName("key")]
    [ColumnEx("key", Order = 2, TypeName = "TEXT")]
    public string Key { get; set; } = string.Empty;

    //icon.file	オブジェクト	アイコンのファイル情報
    //icon.file.contentType	文字列	アイコンのMIMEタイプ
    //icon.file.fileKey	文字列	アイコンのファイルキー
    //icon.file.name	文字列	アイコンの名前
    //icon.file.size	文字列	アイコンのファイルサイズ（byte単位）
    [JsonPropertyName("file")]
    [ColumnEx("file", Order = 3, TypeName = "TEXT", IsExtract = true)]
    public Commons.FileFieldValue File { get; set; } = new();

    public override string ToString()
    {
        return Key.ToString();
    }
}
