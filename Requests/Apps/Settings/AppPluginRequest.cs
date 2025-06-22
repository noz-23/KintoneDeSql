/*
 * Kintone De Sql
 * Copyright (c) 2025 noz-23
 *  https://github.com/noz-23/
 * 
 * Licensed under the MIT License 
 * 
 */
using KintoneDeSql.Managers;
using KintoneDeSql.Responses.Apps.Settings;

namespace KintoneDeSql.Requests.Apps.Settings;

/// <summary>
/// https://cybozu.dev/ja/kintone/docs/rest-api/apps/settings/get-app-plugins/
/// </summary>
internal class AppPluginRequest : BaseRequest<AppPluginRequest, AppPluginResponse>
{
    protected override string _Command { get; } = "app/plugins.json";
    public override void Insert(AppPluginResponse? response_)
    {
        if (response_ != null)
        {
            SQLiteManager.Instance.InsertTable(AppPluginResponse.TableName(false), AppPluginResponse.ListInsertHeader(true), response_.ListInsertValue(true));
        }
    }
}
