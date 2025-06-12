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

namespace KintoneDeSql.Responses.Plugin;

/// <summary>
/// https://cybozu.dev/ja/kintone/docs/rest-api/plugins/get-plugin-apps/
/// </summary>
internal class PluginAppResponse
{
//apps	配列（オブジェクト）	プラグインが追加されているアプリ情報の配列
//並び順は、アプリIDの昇順です。
//追加しているアプリが存在しない場合は、空配列が返ります。
//apps[].id	文字列	アプリID
//apps[].name	文字列	アプリ名
}
