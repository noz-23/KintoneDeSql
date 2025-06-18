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
/// https://cybozu.dev/ja/kintone/docs/rest-api/plugins/get-required-plugins/
/// </summary>
//internal class PluginRequiredRequest : BaseSingleton<PluginRequiredRequest>
internal class PluginRequiredRequest : BaseRequest<PluginRequiredRequest, PluginRequiredResponse>
{
    //private const string _COMMAND = "plugins/required.json";
    //public async Task<PluginRequiredResponse?> Get(int offset_, int limit_ = KintoneManager.CYBOZU_LIMIT)
    //{
    //    var query = string.Empty;
    //    var paramater = JsonSerializer.Serialize(new { offset = offset_, limit = limit_ });
    //    return await KintoneManager.Instance.KintoneGet<PluginRequiredResponse?>(HttpMethod.Get, _COMMAND, query, paramater);
    //}

    //public async Task<PluginRequiredResponse?> Insert(int offset_, int limit_ = KintoneManager.CYBOZU_LIMIT)
    //{
    //    var response = await Get(offset_, limit_);
    //    if (response != null)
    //    {
    //        SQLiteManager.Instance.InsertTable(PluginRequiredResponse.TableName(false), PluginRequiredResponse.ListInsertHeader(true), response.ListInsertValue(true));
    //    }

    //    return response;
    //}
    protected override string _Command { get; } = "plugins/required.json";
    public override void Insert(PluginRequiredResponse? response_)
    {
        if (response_ != null)
        {
            SQLiteManager.Instance.InsertTable(PluginRequiredResponse.TableName(false), PluginRequiredResponse.ListInsertHeader(true), response_.ListInsertValue(true));
        }
    }
}
