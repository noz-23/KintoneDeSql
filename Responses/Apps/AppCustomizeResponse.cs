/*
 * Kintone De Sql
 * Copyright (c) 2025 noz-23
 *  https://github.com/noz-23/
 * 
 * Licensed under the MIT License 
 * 
 */
using KintoneDeSql.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace KintoneDeSql.Responses.Apps;
/// <summary>
/// https://cybozu.dev/ja/kintone/docs/rest-api/apps/settings/get-customization/
/// </summary>
internal class AppCustomizeResponse
{
    //scope	文字列	カスタマイズの適用範囲
    //ALL：すべてのユーザー
    //ADMIN：アプリの管理者だけ
    //NONE：適用しない
    //desktop	オブジェクト	PCで読み込まれるファイルの情報
    //desktop.js	配列	JavaScriptファイルの一覧
    //desktop.js[].type	文字列	ファイルの指定方法
    //URL：URLで指定されている場合
    //FILE：アップロードして指定されている場合
    //desktop.js[].url	文字列	ファイルのURL
    //desktop.js[].file	オブジェクト	添付されたファイルの情報
    //desktop.js[].file.contentType	文字列	MIMEタイプ
    //desktop.js[].file.fileKey	文字列	ファイルキー
    //desktop.js[].file.name	文字列	ファイル名
    //desktop.js[].file.size	文字列	ファイルのサイズ
    //単位はbyteです。
    //desktop.css	配列	CSSファイルの一覧
    //desktop.css[].type	文字列	ファイルの指定方法
    //URL：URLで指定されている場合
    //FILE：アップロードして指定されている場合
    //desktop.css[].url	文字列	ファイルのURL
    //desktop.css[].file	オブジェクト	添付されたファイルの情報
    //desktop.css[].file.contentType	文字列	MIMEタイプ
    //desktop.css[].file.fileKey	文字列	ファイルキー
    //desktop.css[].file.name	文字列	ファイル名
    //desktop.css[].file.size	文字列	ファイルのサイズ
    //単位はbyteです。
    //mobile	オブジェクト	モバイルで読み込まれるファイルの情報
    //mobile.js	配列	JavaScriptファイルの一覧
    //mobile.js[].type	文字列	ファイルの指定方法
    //URL：URLで指定されている場合
    //FILE：アップロードして指定されている場合
    //mobile.js[].url	文字列	ファイルのURL
    //mobile.js[].file	オブジェクト	添付されたファイルの情報
    //mobile.js[].file.contentType	文字列	MIMEタイプ
    //mobile.js[].file.fileKey	文字列	ファイルキー
    //mobile.js[].file.name	文字列	ファイル名
    //mobile.js[].file.size	文字列	ファイルのサイズ
    //単位はbyteです。
    //mobile.css	配列	CSSファイルの一覧
    //mobile.css[].type	文字列	ファイルの指定方法

    //mobile.css[].url	文字列	ファイルのURL
    //mobile.css[].file	オブジェクト	添付されたファイルの情報
    //mobile.css[].file.contentType	文字列	MIMEタイプ
    //mobile.css[].file.fileKey	文字列	ファイルキー
    //mobile.css[].file.name	文字列	ファイル名
    //mobile.css[].file.size	文字列	ファイルのサイズ

    //revision	文字列	アプリの設定のリビジョン番号
    [JsonPropertyName("revision")]
    [ColumnEx("revision", Order = 10, TypeName = "TEXT")]
    public string Revision { get; set; } = string.Empty;

}
