/*
 * Kintone De Sql
 * Copyright (c) 2025 noz-23
 *  https://github.com/noz-23/
 * 
 * Licensed under the MIT License 
 * 
 */
using KintoneDeSql.Managers;
using KintoneDeSql.Responses.Apps.Forms;

namespace KintoneDeSql.Requests.Apps.Forms;

/// <summary>
/// https://cybozu.dev/ja/kintone/docs/rest-api/apps/form/get-form-layout/
/// </summary>
//internal class AppFormLayoutsRequest: BaseSingleton<AppFormLayoutsRequest>
internal class AppFormLayoutsRequest : BaseRequest<AppFormLayoutsRequest, AppFormLayoutResponse>
{
    //private const string _COMMAND = "app/form/layout.json";
    //public async Task<AppFormLayoutResponse?> Get(string appId_, string appToken_ = "")
    //{
    //    var query =string.Empty;
    //    var paramater = JsonSerializer.Serialize(new { app = appId_ });
    //    var response = await KintoneManager.Instance.KintoneGet<AppFormLayoutResponse?>(HttpMethod.Get, _COMMAND, query, paramater, appToken_);
    //    //
    //    if (response != null)
    //    {
    //        response.AppId = appId_;
    //    }
    //    return response;
    //}
    //public async Task<AppFormLayoutResponse?> Insert(string appId_, string appToken_ = "")
    //{
    //    var response = await Get(appId_, appToken_);
    //    if (response != null)
    //    {
    //        SQLiteManager.Instance.InsertTable(AppFormLayoutResponse.TableName(false), AppFormLayoutResponse.ListInsertHeader(true), response.ListInsertValue(true));
    //    }

    //    return response;
    //}
    //public async Task<AppFormLayoutResponse?> Get(string appId_, string appToken_ = "") => await base._Get(_COMMAND, appId_, appToken_);
    //public async Task<AppFormLayoutResponse?> Insert(string appId_, string appToken_ = "") => await base._Insert(_COMMAND, appId_, appToken_);
    protected override string _Command { get; } = "app/form/layout.json";
    public override void Insert(AppFormLayoutResponse? response_)
    {
        if (response_ != null)
        {
            SQLiteManager.Instance.InsertTable(AppFormLayoutResponse.TableName(false), AppFormLayoutResponse.ListInsertHeader(true), response_.ListInsertValue(true));
        }
    }
}
