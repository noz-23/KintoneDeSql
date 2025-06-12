/*
 * Kintone De Sql
 * Copyright (c) 2025 noz-23
 *  https://github.com/noz-23/
 * 
 * Licensed under the MIT License 
 * 
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KintoneDeSql.Responses.Spaces;

/// <summary>
/// https://cybozu.dev/ja/kintone/docs/rest-api/spaces/get-space-members/
/// </summary>
internal class SpaceMemberResponse
{
//members	配列	スペースのメンバーの情報
//下記ユーザーはレスポンスに含まれません。
//ゲストユーザー
//kintoneを利用していないユーザー
//使用停止中のユーザー
//削除したユーザー
//members[].entity	オブジェクト	スペースのメンバーの情報
//members[].entity.type	文字列	スペースのメンバーの種類
//次のいずれかの値が返ります。
//USER：ユーザー
//GROUP：グループ
//ORGANIZATION：組織
//members[].entity.code	文字列	スペースのメンバーのコード
//members[].isAdmin	真偽値	スペースのメンバーがスペース管理者かどうか
//true：スペース管理者
//false：スペース管理者ではない
//members[].isImplicit	真偽値	ユーザーとして追加されているかどうか
//true：ユーザーとして追加されていない （メンバーのグループ／組織に所属するユーザー）
//false：ユーザーとして追加されている
//メンバーの組織に所属しているユーザーでも、ユーザーとして登録されていれば 「false」が返ります。
//entity.typeが「USER」以外の場合は、このプロパティが存在しません。
//members[].includeSubs	真偽値	下位組織を含むかどうか
//true：下位組織を含む
//false：下位組織を含まない
//entity.typeが「USER」以外の場合は、このプロパティが存在しません。
}
