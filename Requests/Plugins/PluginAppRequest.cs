/*
 * Kintone De Sql
 * Copyright (c) 2025 noz-23
 *  https://github.com/noz-23/
 * 
 * Licensed under the MIT License 
 * 
 */
using KintoneDeSql.Data;
using KintoneDeSql.Managers;
using KintoneDeSql.Responses.Plugin;
using KintoneDeSql.Responses.Plugins;
using System.Net.Http;
using System.Text.Json;

namespace KintoneDeSql.Requests.Plugins;

/// <summary>
/// https://cybozu.dev/ja/kintone/docs/rest-api/plugins/get-plugin-apps/
/// </summary>
//internal class PluginAppRequest : BaseSingleton<PluginAppRequest>
internal class PluginAppRequest : BaseRequest<PluginAppRequest, PluginAppResponse>
{
    //private const string _COMMAND = "plugin/apps.json";
    //public async Task<PluginAppResponse?> Get(string id_, int offset_, int limit_ = KintoneManager.RECORD_LIMIT)
    //{
    //    var query = string.Empty;
    //    var paramater = JsonSerializer.Serialize(new { id = id_ , offset = offset_, limit = limit_ });
    //    var response = await KintoneManager.Instance.KintoneGet<PluginAppResponse?>(HttpMethod.Get, _COMMAND, query, paramater);
    //    if (response != null)
    //    {
    //        response.PluginId = id_;
    //    }

    //    return response;
    //}

    //public async Task<PluginAppResponse?> Insert(string id_, int offset_, int limit_ = KintoneManager.RECORD_LIMIT)
    //{
    //    var response = await Get(id_, offset_, limit_);
    //    if (response != null)
    //    {
    //        SQLiteManager.Instance.InsertTable(PluginAppResponse.TableName(false), PluginAppResponse.ListInsertHeader(true), response.ListInsertValue(true));
    //    }

    //    return response;
    //}
    protected override string _Command { get; } = "plugin/apps.json";
    public override void Insert(PluginAppResponse? response_)
    {
        if (response_ != null)
        {
            SQLiteManager.Instance.InsertTable(PluginAppResponse.TableName(false), PluginAppResponse.ListInsertHeader(true), response_.ListInsertValue(true));
        }
    }
}
