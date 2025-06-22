/*
 * Kintone De Sql
 * Copyright (c) 2025 noz-23
 *  https://github.com/noz-23/
 * 
 * Licensed under the MIT License 
 * 
 */
using KintoneDeSql.Managers;
using KintoneDeSql.Responses.Plugin;

namespace KintoneDeSql.Requests.Plugins;

/// <summary>
/// https://cybozu.dev/ja/kintone/docs/rest-api/plugins/get-plugin-apps/
/// </summary>
internal class PluginAppRequest : BaseRequest<PluginAppRequest, PluginAppResponse>
{
    protected override string _Command { get; } = "plugin/apps.json";
    public override void Insert(PluginAppResponse? response_)
    {
        if (response_ != null)
        {
            SQLiteManager.Instance.InsertTable(PluginAppResponse.TableName(false), PluginAppResponse.ListInsertHeader(true), response_.ListInsertValue(true));
        }
    }
}
