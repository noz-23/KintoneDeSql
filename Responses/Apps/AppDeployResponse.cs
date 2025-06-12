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

namespace KintoneDeSql.Responses.Apps;

/// <summary>
/// https://cybozu.dev/ja/kintone/docs/rest-api/apps/settings/get-app-deploy-status/
/// </summary>
internal class AppDeployResponse
{
//apps	配列	処理の状況を表すオブジェクトの配列
//apps[].app	文字列	アプリID
//apps[].status	文字列	処理の進捗
//PROCESSING：処理中
//SUCCESS：処理の完了
//FAIL：処理の失敗
//CANCEL：他のアプリで処理が失敗したことによる、処理のキャンセル
}
