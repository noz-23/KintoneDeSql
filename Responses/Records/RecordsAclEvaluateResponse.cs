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

namespace KintoneDeSql.Responses.Records;

/// <summary>
/// https://cybozu.dev/ja/kintone/docs/rest-api/records/evaluate-record-permissions/
/// </summary>
internal class RecordsAclEvaluateResponse
{
//rights	配列	アクセス権の設定の一覧
//rights[].id	文字列	レコードID
//rights[].record	オブジェクト	指定したレコードのアクセス権の設定
//rights[].record.viewable	真偽値	レコードの閲覧が可能かどうか
//true：閲覧できる
//false：閲覧できない
//メンテナンスモードの場合は、「false」が設定されます。
//rights[].record.editable	真偽値	レコードの編集が可能かどうか
//true：編集できる
//false：編集できない
//メンテナンスモードの場合は、「false」が設定されます。
//rights[].record.deletable	真偽値	レコードの削除が可能かどうか
//true：削除できる
//false：削除できない
//メンテナンスモードの場合は、「false」が設定されます。
//rights[].fields	オブジェクト	レコードに存在するフィールドのアクセス権
//レコードを更新するAPIで操作できるフィールドのアクセス権が返ります。
//APIを実行するユーザーに閲覧権限のないフィールドや、アクセス権を設定していないフィールドも含まれます。
//テーブル内のフィールドでも通常に配置したフィールドと同じ階層で返ります。
//rights[].fields.フィールドコード.viewable	真偽値	フィールドの閲覧が可能かどうか
//true：閲覧できる
//false：閲覧できない
//メンテナンスモードの場合は、「false」が設定されます。
//rights[].fields.フィールドコード.editable	真偽値	フィールドの編集が可能かどうか
//true：編集できる
//false：編集できない
//メンテナンスモードの場合は、「false」が設定されます。

}
