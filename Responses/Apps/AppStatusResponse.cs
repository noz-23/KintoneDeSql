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
/// https://cybozu.dev/ja/kintone/docs/rest-api/apps/settings/get-process-management-settings/
/// </summary>
internal class AppStatusResponse
{
    //enable	真偽値	プロセス管理が有効かどうか
    //true：プロセス管理が有効
    //false：プロセス管理が無効
    //states	オブジェクト	ステータスの情報
    //プロセス管理を一度も設定していないアプリの場合は 「null」が返ります。
    //states.ステータス名.name	文字列	ステータス名
    //states.ステータス名.index	文字列	ステータスの順番
    //値は0から始まり、昇順で並べ替えられます。
    //states.ステータス名.assignee	オブジェクト	ステータスの作業者
    //states.ステータス名.assignee.type	文字列	ステータスの作業者のタイプ
    //ONE：次のユーザーから作業者を選択
    //ALL：次のユーザー全員
    //ANY：次のユーザーのうち一人
    //先頭（states.ステータス名.indexが最小）のステータスは、「ONE」が返ります。
    //states.ステータス名.assignee.entities	配列	ステータスの作業者の一覧
    //並び順は画面と同じです。
    //states.ステータス名.assignee.entities[].entity	オブジェクト	ステータスの作業者のユーザー情報
    //削除済みや無効なユーザー、組織、グループが指定されている場合、および削除済みのカスタマイズ項目が指定されている場合はレスポンスに含めません。
    //states.ステータス名.assignee.entities[].entity.type	文字列	ステータスの作業者の指定形式
    //USER：ユーザー
    //GROUP：グループ
    //ORGANIZATION：組織
    //FIELD_ENTITY：「フォームのフィールドを追加」で指定したフィールド
    //CREATOR：アプリ作成者
    //CUSTOM_FIELD：.com共通管理のカスタマイズ項目
    //カスタマイズ項目 (External link)
    //states.ステータス名.assignee.entities[].entity.code	文字列	ステータスの作業者のコード
    //entity.typeの値によって異なります。
    //「USER」の場合：ログイン名
    //「GROUP」の場合：グループコード
    //「ORGANIZATION」の場合：組織コード
    //「FIELD_ENTITY」の場合：「フォームのフィールドを追加」で指定したフィールドのフィールドコード
    //「CREATOR」の場合：「null」
    //「CUSTOM_FIELD」の場合：カスタマイズ項目コード
    //ゲストユーザーの場合、ログイン名の前に「guest/」が付きます。
    //states.ステータス名.assignee.entities[].includeSubs	真偽値	設定を下位組織に継承するかどうか
    //true：継承する
    //false：継承しない
    //entity.typeが「ORGANIZATION」か、「FIELD_ENTITY」で組織選択フィールドが指定されている場合のみ、「true」が返ります。
    //actions	配列	アクションの情報の一覧
    //並び順は画面と同じです。プロセス管理を一度も設定していないアプリの場合は 「null」が返ります。
    //actions[].name	文字列	アクションの名前
    //actions[].from	文字列	アクション実行前のステータス名
    //actions[].to	文字列	アクション実行後のステータス名
    //actions[].filterCond	文字列	アクションの実行条件
    //revision	文字列	アプリの設定のリビジョン番号
    [JsonPropertyName("revision")]
    [ColumnEx("revision", Order = 10, TypeName = "TEXT")]
    public string Revision { get; set; } = string.Empty;
}
