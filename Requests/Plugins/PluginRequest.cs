/*
 * Kintone De Sql
 * Copyright (c) 2025 noz-23
 *  https://github.com/noz-23/
 * 
 * Licensed under the MIT License 
 * 
 */
using KintoneDeSql.Managers;
using KintoneDeSql.Responses.Plugins;

namespace KintoneDeSql.Requests.Plugins;

/// <summary>
/// https://cybozu.dev/ja/kintone/docs/rest-api/plugins/get-plugins/
/// </summary>
internal class PluginRequest : BaseRequest<PluginRequest, PluginResponsee>
{
    protected override string _Command { get; } = "plugins.json";
    public override void Insert(PluginResponsee? response_)
    {
        if (response_ != null)
        {
            SQLiteManager.Instance.InsertTable(PluginResponsee.TableName(false), PluginResponsee.ListInsertHeader(true), response_.ListInsertValue(true));
        }
    }
}
