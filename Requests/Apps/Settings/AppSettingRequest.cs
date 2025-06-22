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
/// https://cybozu.dev/ja/kintone/docs/rest-api/apps/settings/get-general-notification-settings/
/// </summary>
internal class AppSettingRequest : BaseRequest<AppSettingRequest, AppSettingResponse>
{
    protected override string _Command { get; } = "app/settings.json";
    public override void Insert(AppSettingResponse? response_)
    {
        if (response_ != null)
        {
            SQLiteManager.Instance.InsertTable(AppSettingResponse.TableName(false), AppSettingResponse.ListInsertHeader(true), response_.ListInsertValue(true));
        }
    }
}