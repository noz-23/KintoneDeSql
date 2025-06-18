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
//internal class PluginRequest : BaseSingleton<PluginRequest>
internal class PluginRequest : BaseRequest<PluginRequest, PluginResponsee>
{
    //private const string _COMMAND = "plugins.json";
    //public async Task<PluginResponsee?> Get(int offset_,int limit_ = KintoneManager.CYBOZU_LIMIT)
    //{
    //    var query = string.Empty;
    //    var paramater = JsonSerializer.Serialize(new { offset = offset_ , limit = limit_});
    //    return await KintoneManager.Instance.KintoneGet<PluginResponsee?>(HttpMethod.Get, _COMMAND, query, paramater);
    //}

    //public async Task<PluginResponsee?> Insert(int offset_, int limit_ = KintoneManager.CYBOZU_LIMIT)
    //{
    //    var response = await Get(offset_, limit_);
    //    if (response != null)
    //    {
    //        SQLiteManager.Instance.InsertTable(PluginResponsee.TableName(false), PluginResponsee.ListInsertHeader(true), response.ListInsertValue(true));
    //    }
    //    return response;
    //}
    protected override string _Command { get; } = "plugins.json";
    public override void Insert(PluginResponsee? response_)
    {
        if (response_ != null)
        {
            SQLiteManager.Instance.InsertTable(PluginResponsee.TableName(false), PluginResponsee.ListInsertHeader(true), response_.ListInsertValue(true));
        }
    }
}
