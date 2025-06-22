
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
internal class AppStatusRequest : BaseRequest<AppStatusRequest, AppStatusResponse>
{
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
