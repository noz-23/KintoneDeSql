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
using KintoneDeSql.Responses.Cybozu;
using KintoneDeSql.Responses.Plugins;
using System.Net.Http;
using System.Text.Json;

namespace KintoneDeSql.Requests.Plugins;

/// <summary>
/// https://cybozu.dev/ja/kintone/docs/rest-api/plugins/get-plugins/
/// </summary>
internal class PluginsRequest : BaseSingleton<PluginsRequest>
{
    private const string _COMMAND = "plugins.json";
    public async Task<PluginResponsee?> Get(int offset_,int size_ = KintoneManager.CYBOZU_LIMIT)
    {
        var query = string.Empty;
        var paramater = JsonSerializer.Serialize(new { size = size_, offset = offset_ });
        return await KintoneManager.Instance.KintoneGet<PluginResponsee?>(HttpMethod.Get, _COMMAND, query, paramater);
    }

    public async Task<PluginResponsee?> Insert(int offset_, int size_ = KintoneManager.CYBOZU_LIMIT)
    {
        var response = await Get(offset_, size_);
        if (response != null)
        {
            SQLiteManager.Instance.InsertTable(PluginResponsee.TableName(false), PluginResponsee.ListInsertHeader(true), response.ListInsertValue(true));
        }
        return response;
    }

}
