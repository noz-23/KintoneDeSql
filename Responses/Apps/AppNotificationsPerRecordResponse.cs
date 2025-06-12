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
/// https://cybozu.dev/ja/kintone/docs/rest-api/apps/settings/get-per-record-notification-settings/
/// </summary>
internal class AppNotificationsPerRecordResponse
{
    //notifications	配列	条件通知の設定の一覧
    //notifications[].filterCond	文字列	レコードの条件
    //クエリ形式で表されます。クエリ形式の詳細は次のページを参照してください。
    //クエリの書き方
    //notifications[].title	文字列	通知内容
    //notifications[].targets	配列	通知先の対象の一覧
    //notifications[].targets[].entity	オブジェクト	通知先の対象
    //notifications[].targets[].entity.type	文字列	通知先の対象の種類
    //USER：ユーザー
    //GROUP：グループ
    //ORGANIZATION：組織
    //FIELD_ENTITY：「フォームのフィールドを追加」で指定したフィールド
    //作成者
    //更新者
    //ユーザー選択
    //組織選択
    //グループ選択
    //notifications[].targets[].entity.code	文字列	通知先の対象のコード
    //entity.typeの値によって異なります。
    //「USER」の場合：ユーザーのログイン名
    //「GROUP」の場合：グループコード
    //「ORGANIZATION」の場合：組織コード
    //「FIELD_ENTITY」の場合：「フォームのフィールドを追加」で指定したフィールドのフィールドコード
    //ゲストユーザーの場合、ログイン名の前に「guest/」が付きます。
    //notifications[].targets[].includeSubs	真偽値	設定を下位組織に継承するかどうか
    //true：継承する
    //false：継承しない
    //entity.typeの値が「ORGANIZATION」か、「FIELD_ENTITY」で組織選択フィールドが指定されている場合、「true」が返ります。
    //revision	文字列	アプリの設定のリビジョン番号
    [JsonPropertyName("revision")]
    [ColumnEx("revision", Order = 10, TypeName = "TEXT")]
    public string Revision { get; set; } = string.Empty;
}
