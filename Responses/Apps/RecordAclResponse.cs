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
/// https://cybozu.dev/ja/kintone/docs/rest-api/apps/settings/get-record-permissions/
/// </summary>
internal class RecordAclResponse
{
    //rights	配列	アクセス権の設定の一覧
    //設定の優先度が高いものから順に並びます。
    //rights[].filterCond	文字列	レコードの条件
    //クエリ形式で表されます。クエリ形式の詳細は次のページを参照してください。
    //クエリの書き方
    //rights[].entities	配列	アクセス権の設定対象の一覧
    //設定の優先度が高いものから順に並びます。
    //rights[].entities[].entity	オブジェクト	アクセス権の設定の対象
    //rights[].entities[].entity.type	文字列	アクセス権の設定対象の種類
    //USER：ユーザー
    //GROUP：グループ
    //ORGANIZATION：組織
    //FIELD_ENTITY：「フォームのフィールドを追加」で指定したフィールド
    //rights[].entities[].entity.code	文字列	アクセス権の設定対象のコード
    //entity.typeの値によって異なります。
    //「USER」の場合：ユーザーのログイン名
    //「GROUP」の場合：グループコード
    //「ORGANIZATION」の場合：組織コード
    //「FIELD_ENTITY」の場合：「フォームのフィールドを追加」で指定したフィールドのフィールドコード
    //rights[].entities[].viewable	真偽値	レコードの閲覧が可能かどうか
    //true：閲覧できる
    //false：閲覧できない
    //rights[].entities[].editable	真偽値	レコードの編集が可能かどうか
    //true：編集できる
    //false：編集できない
    //rights[].entities[].deletable	真偽値	レコードの削除が可能かどうか
    //true：削除できる
    //false：削除できない
    //rights[].entities[].includeSubs	真偽値	設定を下位組織に継承するか
    //true：継承する
    //false：継承しない
    //revision	文字列	アプリの設定のリビジョン番号
    [JsonPropertyName("revision")]
    [ColumnEx("revision", Order = 10, TypeName = "TEXT")]
    public string Revision { get; set; } = string.Empty;
}
