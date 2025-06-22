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
internal class PluginRequiredRequest : BaseRequest<PluginRequiredRequest, PluginRequiredResponse>
{
    protected override string _Command { get; } = "plugins/required.json";
    public override void Insert(PluginRequiredResponse? response_)
    {
        if (response_ != null)
        {
            SQLiteManager.Instance.InsertTable(PluginRequiredResponse.TableName(false), PluginRequiredResponse.ListInsertHeader(true), response_.ListInsertValue(true));
        }
    }
}
