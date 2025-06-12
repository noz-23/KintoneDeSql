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
/// https://cybozu.dev/ja/kintone/docs/rest-api/plugins/get-plugins/
/// </summary>
internal class PluginResponse
{
//plugins	配列（オブジェクト）	読み込んだプラグインの一覧
//並び順は、インストールした日時の降順です。
//プラグインが存在しない場合は、空配列が返ります。
//plugins[].id	文字列	プラグインID
//plugins[].name	文字列	プラグインの名前
//plugins[].description	文字列	プラグインの説明
//plugins[].isMarketPlugin	真偽値	プラグインストアのプラグインかどうか
//true：プラグインストアのプラグインである
//false：プラグインストアのプラグインではない
//plugins[].version	文字列	プラグインのバージョン
}
