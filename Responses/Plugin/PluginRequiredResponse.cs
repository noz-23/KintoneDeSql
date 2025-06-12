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
/// https://cybozu.dev/ja/kintone/docs/rest-api/plugins/get-required-plugins/
/// </summary>
internal class PluginRequiredResponse
{
//plugins	配列（オブジェクト）	インストールが必要なプラグインの一覧

//並び順は、プラグインIDの昇順です。
//プラグインが存在しない場合は、空配列が返ります。
//plugins[].id	文字列	プラグインID
//plugins[].name	文字列	プラグインの名前
//取得できない場合はnullが返ります。
//plugins[].isMarketPlugin	真偽値	プラグインストアのプラグインかどうか
//true：プラグインストアのプラグインである
//false：プラグインストアのプラグインではない
}
