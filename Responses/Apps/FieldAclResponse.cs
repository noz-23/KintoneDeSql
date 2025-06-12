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
/// https://cybozu.dev/ja/kintone/docs/rest-api/apps/settings/get-field-permissions/
/// </summary>
internal class FieldAclResponse
{
    //rights	配列	アクセス権の設定の一覧
    //rights[].code	文字列	アクセス権を設定するフィールドコード
    //rights[].entities	配列	アクセス権の設定対象の一覧
    //設定の優先度が高いものから順に並びます。
    //rights[].entities[].accessibility	文字列	フィールドに対して可能な操作
    //READ：閲覧だけ
    //WRITE：閲覧と編集
    //NONE：閲覧と編集は不可
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
    //ゲストユーザーを指定する場合、ログイン名の前に「guest/」が付きます。
    //rights[].entities[].includeSubs	真偽値	設定を下位組織に継承するかどうか
    //true：継承する
    //false：継承しない
    //entity.typeが「ORGANIZATION」か、「FIELD_ENTITY」で組織選択フィールドが指定されている場合のみ、「true」が返ります。
    //revision	文字列	アプリの設定のリビジョン番号
    [JsonPropertyName("revision")]
    [ColumnEx("revision", Order = 10, TypeName = "TEXT")]
    public string Revision { get; set; } = string.Empty;
}
