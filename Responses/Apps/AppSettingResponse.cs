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
/// https://cybozu.dev/ja/kintone/docs/rest-api/apps/settings/get-general-settings/
/// </summary>
internal class AppSettingResponse
{
    //name	文字列	アプリ名
    [JsonPropertyName("name")]
    [ColumnEx("name", Order = 13, TypeName = "TEXT")]
    public string Name { get; set; } = string.Empty;

    //description	文字列	アプリの説明
    [JsonPropertyName("description")]
    [ColumnEx("description", Order = 13, TypeName = "TEXT")]
    public string Description { get; set; } = string.Empty;

    //icon	オブジェクト	アプリのアイコンの情報
    //icon.type	文字列	アイコンの種類
    //icon.key	文字列	アイコンのキー（識別子）
    //icon.file	オブジェクト	アイコンのファイル情報
    //icon.file.contentType	文字列	アイコンのMIMEタイプ
    //icon.file.fileKey	文字列	アイコンのファイルキー
    //icon.file.name	文字列	アイコンの名前
    //icon.file.size	文字列	アイコンのファイルサイズ（byte単位）

    //titleField	オブジェクト	レコードのタイトルの情報
    //titleField.selectionMode	文字列	レコードのタイトルとして利用するフィールドの選択方法
    //titleField.code	文字列	レコードのタイトルとして利用するフィールドのフィールドコード
    //titleField.selectionModeの値によって異なります。

    //enableThumbnails	真偽値	サムネイルを表示するかどうか

    //enableBulkDeletion	真偽値	レコード一括削除を有効にするかどうか

    //enableComments	真偽値	レコードのコメント機能を有効にするかどうか

    //enableDuplicateRecord	真偽値	「レコードを再利用する」機能を有効にするかどうか

    //enableInlineRecordEditing	真偽値	レコード一覧でのインライン編集を有効にするかどうか

    //numberPrecision	オブジェクト	数値と計算の精度
    //numberPrecision.digits	文字列	全体の桁数
    //numberPrecision.decimalPlaces	文字列	小数部の桁数
    //numberPrecision.roundingMode	文字列	数値の丸めかた

    //firstMonthOfFiscalYear	文字列	第一四半期の開始月

    //revision	文字列	アプリの設定のリビジョン番号
    [JsonPropertyName("revision")]
    [ColumnEx("revision", Order = 2, TypeName = "TEXT")]
    public string Revision { get; set; } = string.Empty;
}
