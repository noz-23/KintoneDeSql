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
/// https://cybozu.dev/ja/kintone/docs/rest-api/apps/settings/get-customization/
/// </summary>
internal class AppCustomizeRequest : BaseRequest<AppCustomizeRequest, AppCustomizeResponse>
{
    protected override string _Command { get; } = "app/customize.json";
    public override void Insert(AppCustomizeResponse? response_)
    {
        if (response_ != null)
        {
            SQLiteManager.Instance.InsertTable(AppCustomizeResponse.TableName(false), AppCustomizeResponse.ListInsertHeader(true), response_.ListInsertValue(true));
        }
    }
}
