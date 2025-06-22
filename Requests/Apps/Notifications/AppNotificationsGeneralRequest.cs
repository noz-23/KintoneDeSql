/*
 * Kintone De Sql
 * Copyright (c) 2025 noz-23
 *  https://github.com/noz-23/
 * 
 * Licensed under the MIT License 
 * 
 */
using KintoneDeSql.Managers;
using KintoneDeSql.Responses.Apps.Notifications;

namespace KintoneDeSql.Requests.Apps.Notifications;

/// <summary>
/// https://cybozu.dev/ja/kintone/docs/rest-api/apps/settings/get-app-permissions/
/// </summary>
internal class AppNotificationsGeneralRequest : BaseRequest<AppNotificationsGeneralRequest, AppNotificationsGeneralResponse>
{
    protected override string _Command { get; } = "app/notifications/general.json";
    public override void Insert(AppNotificationsGeneralResponse? response_)
    {
        if (response_ != null)
        {
            SQLiteManager.Instance.InsertTable(AppNotificationsGeneralResponse.TableName(false), AppNotificationsGeneralResponse.ListInsertHeader(true), response_.ListInsertValue(true));
        }
    }

}
