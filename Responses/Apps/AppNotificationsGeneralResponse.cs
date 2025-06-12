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
/// https://cybozu.dev/ja/kintone/docs/rest-api/apps/settings/get-general-notification-settings/
/// </summary>
internal class AppNotificationsGeneralResponse
{
    //notifications	配列（オブジェクト）	条件通知の設定を表すオブジェクトの配列
    //notifications[].entity	オブジェクト	条件通知の設定の対象を表すオブジェクト
    //notifications[].entity.type	文字列	条件通知の設定対象の種類
    //USER：ユーザー
    //GROUP：グループ
    //ORGANIZATION：組織
    //FIELD_ENTITY：「フォームのフィールドを追加」で指定したフィールド
    //作成者
    //更新者
    //ユーザー選択
    //組織選択
    //グループ選択
    //notifications[].entity.code	文字列	条件通知の設定対象のコード
    //entity.typeの値によって異なります。
    //「USER」の場合：ユーザーのログイン名
    //「GROUP」の場合：グループコード
    //「ORGANIZATION」の場合：組織コード
    //「FIELD_ENTITY」の場合：「フォームのフィールドを追加」で指定したフィールドのフィールドコード
    //ゲストユーザーの場合、ログイン名の前に「guest/」が付きます。
    //notifications[].includeSubs	真偽値	設定を下位組織に継承するかどうか
    //true：継承する
    //false：継承しない
    //entity.typeの値が「ORGANIZATION」か、「FIELD_ENTITY」で組織選択フィールドが指定されている場合のみ、「true」が返ります。
    //notifications[].recordAdded	真偽値	レコード追加で通知するかどうか
    //true：レコード追加で通知する
    //false：レコード追加で通知しない
    //notifications[].recordEdited	真偽値	レコード編集で通知するかどうか
    //true：レコード編集で通知する
    //false：レコード編集で通知しない
    //notifications[].commentAdded	真偽値	コメントの書き込みで通知するかどうか
    //true：コメントの書き込みで通知する
    //false：コメントの書き込みで通知しない
    //notifications[].statusChanged	真偽値	ステータスの更新で通知するかどうか
    //true：ステータスの更新で通知する
    //false：ステータスの更新で通知しない
    //notifications[].fileImported	真偽値	ファイル読み込みで通知するかどうか
    //true：ファイル読み込みで通知する
    //false：ファイル読み込みで通知しない
    //notifyToCommenter	真偽値	コメントを書き込んだユーザーが、そのレコードにコメントが書き込まれたときに通知を受信するかどうか
    //true：コメントが書き込まれたときに通知を受信する
    //false：コメントが書き込まれたときに通知を受信しない
    //revision	文字列	アプリの設定のリビジョン番号
    [JsonPropertyName("revision")]
    [ColumnEx("revision", Order = 10, TypeName = "TEXT")]
    public string Revision { get; set; } = string.Empty;
}
