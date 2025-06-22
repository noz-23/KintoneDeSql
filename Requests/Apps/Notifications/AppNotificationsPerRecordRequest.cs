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

namespace KintoneDeSql.Requests.Apps;

/// <summary>
/// https://cybozu.dev/ja/kintone/docs/rest-api/apps/settings/get-per-record-notification-settings/
/// </summary>
internal class AppNotificationsPerRecordRequest : BaseRequest<AppNotificationsPerRecordRequest, AppNotificationsPerRecordResponse>
{
    protected override string _Command { get; } = "app/notifications/perRecord.json";
    public override void Insert(AppNotificationsPerRecordResponse? response_)
    {
        if (response_ != null)
        {
            SQLiteManager.Instance.InsertTable(AppNotificationsPerRecordResponse.TableName(false), AppNotificationsPerRecordResponse.ListInsertHeader(true), response_.ListInsertValue(true));
        }
    }
}
