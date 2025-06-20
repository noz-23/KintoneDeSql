﻿/*
 * Kintone De Sql
 * Copyright (c) 2025 noz-23
 *  https://github.com/noz-23/
 * 
 * Licensed under the MIT License 
 * 
 */
using KintoneDeSql.Managers;
using KintoneDeSql.Responses.Apps;
using KintoneDeSql.Responses.Apps.Views;

namespace KintoneDeSql.Requests.Apps.Views;

/// <summary>
/// https://cybozu.dev/ja/kintone/docs/rest-api/apps/view/get-views/
/// </summary>
//internal class AppViewsRequest : BaseSingleton<AppViewsRequest>
internal class AppViewsRequest : BaseRequest<AppViewsRequest, AppViewResponse>
{
    //private const string _COMMAND = "app/views.json";
    //public async Task<AppViewResponse?> Get(string appId_)
    //{
    //    var query = string.Empty;
    //    var paramater = JsonSerializer.Serialize(new { app = appId_ });
    //    var response = await KintoneManager.Instance.KintoneGet<AppViewResponse?>(HttpMethod.Get, _COMMAND, query, paramater);
    //    //
    //    if (response != null)
    //    {
    //        response.AppId = appId_;
    //    }
    //    return response;
    //}

    //public async Task<AppViewResponse?> Insert(string appId_)
    //{
    //    var response = await Get(appId_);
    //    if (response != null)
    //    {
    //        SQLiteManager.Instance.InsertTable(AppViewResponse.TableName(false), AppViewResponse.ListInsertHeader(true), response.ListInsertValue(true));
    //        SQLiteManager.Instance.InsertTable(AppViewResponseField.TableName(false), AppViewResponseField.ListInsertHeader(true), response.ListInsertValueField(true));
    //    }
    //    return response;
    //}

    protected override string _Command { get; } = "app/views.json";
    public override void Insert(AppViewResponse? response_)
    {
        if (response_ != null)
        {
            SQLiteManager.Instance.InsertTable(AppViewResponse.TableName(false), AppViewResponse.ListInsertHeader(true), response_.ListInsertValue(true));
            SQLiteManager.Instance.InsertTable(AppViewResponseField.TableName(false), AppViewResponseField.ListInsertHeader(true), response_.ListInsertValueField(true));
        }
    }
}
