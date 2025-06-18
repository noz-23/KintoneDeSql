
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
/// https://cybozu.dev/ja/kintone/docs/rest-api/apps/settings/get-process-management-settings/
/// </summary>
//internal class AppStatusRequest : BaseSingleton<AppStatusRequest>
internal class AppStatusRequest : BaseRequest<AppStatusRequest, AppStatusResponse>
{
    //private const string _COMMAND = "app/status.json";
    //public async Task<AppStatusResponse?> Get(string appId_)
    //{
    //    var query = string.Empty;
    //    var paramater = JsonSerializer.Serialize(new { app = appId_ });
    //    var response = await KintoneManager.Instance.KintoneGet<AppStatusResponse?>(HttpMethod.Get, _COMMAND, query, paramater);
    //    //
    //    if (response != null)
    //    {
    //        response.AppId = appId_;
    //    }
    //    return response;
    //}

    //public async Task<AppStatusResponse?> Insert(string appId_)
    //{
    //    var response = await Get(appId_);
    //    if (response != null)
    //    {
    //        SQLiteManager.Instance.InsertTable(AppStatusResponse.TableName(false), AppStatusResponse.ListInsertHeader(true), response.ListInsertValue(true));
    //        SQLiteManager.Instance.InsertTable(AppStatusResponseAction.TableName(false), AppStatusResponseAction.ListInsertHeader(true), response.ListInsertValueAction(true));
    //        SQLiteManager.Instance.InsertTable(AppStatusResponseStatus.TableName(false), AppStatusResponseStatus.ListInsertHeader(true), response.ListInsertValueState(true));
    //        SQLiteManager.Instance.InsertTable(AppStatusResponseEntity.TableName(false), AppStatusResponseEntity.ListInsertHeader(true), response.ListInsertValueEntity(true));
    //    }
    //    return response;
    //}
    protected override string _Command { get; } = "app/status.json";
    public override void Insert(AppStatusResponse? response_)
    {
        if (response_ != null)
        {
            SQLiteManager.Instance.InsertTable(AppStatusResponse.TableName(false), AppStatusResponse.ListInsertHeader(true), response_.ListInsertValue(true));
            SQLiteManager.Instance.InsertTable(AppStatusResponseAction.TableName(false), AppStatusResponseAction.ListInsertHeader(true), response_.ListInsertValueAction(true));
            SQLiteManager.Instance.InsertTable(AppStatusResponseEntity.TableName(false), AppStatusResponseEntity.ListInsertHeader(true), response_.ListInsertValueEntity(true));
            SQLiteManager.Instance.InsertTable(AppStatusResponseStatus.TableName(false), AppStatusResponseStatus.ListInsertHeader(true), response_.ListInsertValueState(true));
        }
    }
}
