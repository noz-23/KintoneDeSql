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
/// https://cybozu.dev/ja/kintone/docs/rest-api/apps/settings/get-action-settings/
/// </summary>
internal class AppActionRequest : BaseRequest<AppActionRequest, AppActionResponse>
{
    protected override string _Command { get; } = "app/actions.json";
    public override void Insert(AppActionResponse? response_)
    {
        if (response_ != null)
        {
            SQLiteManager.Instance.InsertTable(AppActionResponse.TableName(false), AppActionResponse.ListInsertHeader(true), response_.ListInsertValue(true));
            SQLiteManager.Instance.InsertTable(AppActionResponseEntity.TableName(false), AppActionResponseEntity.ListInsertHeader(true), response_.ListInsertValueEntity(true));
            SQLiteManager.Instance.InsertTable(AppActionResponseMapping.TableName(false), AppActionResponseMapping.ListInsertHeader(true), response_.ListInsertValueMapping(true));
        }
    }
}
