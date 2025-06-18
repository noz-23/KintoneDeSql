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
//internal class AppPluginRequest : BaseSingleton<AppPluginRequest>
internal class AppPluginRequest : BaseRequest<AppPluginRequest, AppPluginResponse>
{
    //private const string _COMMAND = "app/plugins.json";
    //public async Task<AppPluginResponse?> Get(string appId_)
    //{
    //    var query = string.Empty;
    //    var paramater = JsonSerializer.Serialize(new { app = appId_ });
    //    var response = await KintoneManager.Instance.KintoneGet<AppPluginResponse?>(HttpMethod.Get, _COMMAND, query, paramater);
    //    //
    //    if (response != null)
    //    {
    //        response.AppId = appId_;
    //    }
    //    return response;
    //}

    //public async Task<AppPluginResponse?> Insert(string appId_)
    //{
    //    var response = await Get(appId_);
    //    if (response != null)
    //    {
    //        SQLiteManager.Instance.InsertTable(AppPluginResponse.TableName(false), AppPluginResponse.ListInsertHeader(true), response.ListInsertValue(true));
    //    }

    //    return response;
    //}

    protected override string _Command { get; } = "app/plugins.json";
    public override void Insert(AppPluginResponse? response_)
    {
        if (response_ != null)
        {
            SQLiteManager.Instance.InsertTable(AppPluginResponse.TableName(false), AppPluginResponse.ListInsertHeader(true), response_.ListInsertValue(true));
        }
    }
}
