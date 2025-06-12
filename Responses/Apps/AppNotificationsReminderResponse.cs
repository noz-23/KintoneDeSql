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
/// https://cybozu.dev/ja/kintone/docs/rest-api/apps/settings/get-reminder-notification-settings/
/// </summary>
internal class AppNotificationsReminderResponse
{

    //notifications	配列	リマインダーの条件通知の設定
    //notifications[].timing	オブジェクト	通知のタイミング
    //notifications[].timing.code	文字列	通知のタイミングの基準日時となるフィールドのフィールドコード
    //notifications[].timing.daysLater	文字列	基準日時から何日前または何日後に通知するか
    //基準日時より前に通知する場合は、負の整数が返ります。
    //notifications[].timing.hoursLater	文字列	基準日時にtiming.daysLaterを足した日時から、何時間後または何時間前に通知するか
    //timing.codeに日時を表すフィールドを指定し「相対時刻を指定」が設定されている場合に値が返ります。
    //基準日時より前の場合は、負の整数が返ります。
    //notifications[].timing.time	文字列	基準日時にtiming.daysLaterを足した日付から、いつ通知するか
    //次のいずれかの場合に値が返ります。
    //timing.codeに日付フィールドを設定している場合
    //timing.codeに日時を表すフィールドを指定し「絶対時刻を指定」が設定されている場合
    //notifications[].filterCond	文字列	リマインダーの条件通知を行う条件
    //クエリ形式で表されます。クエリ形式の詳細は次のページを参照してください。
    //クエリの書き方
    //削除済みのユーザー／組織／グループが指定されている場合はエラーが返ります。
    //notifications[].title	文字列	リマインダーの条件通知で通知される内容
    //notifications[].targets	配列	通知先の対象の一覧
    //notifications[].targets[].entity	オブジェクト	通知先の対象を表すオブジェクト
    //notifications[].targets[].entity.type	文字列	通知先の対象の種類
    //USER：ユーザー
    //GROUP：グループ
    //ORGANIZATION：組織
    //FIELD_ENTITY：通知先のフィールドで指定したフィールド
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
    //entity.typeが「ORGANIZATION」か、「FIELD_ENTITY」で組織選択フィールドが指定されている場合のみ 「false」が返ります。
    //timezone	文字列	リマインドする時刻のタイムゾーン
    //リマインダーの条件通知を一度も設定していないアプリでは 「null」が返ります。
    //revision	文字列	アプリ設定のリビジョン番号
    [JsonPropertyName("revision")]
    [ColumnEx("revision", Order = 10, TypeName = "TEXT")]
    public string Revision { get; set; } = string.Empty;
}
