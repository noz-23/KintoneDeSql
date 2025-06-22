/*
 * Kintone De Sql
 * Copyright (c) 2025 noz-23
 *  https://github.com/noz-23/
 * 
 * Licensed under the MIT License 
 * 
 */
using KintoneDeSql.Managers;
using KintoneDeSql.Responses.Apps;

namespace KintoneDeSql.Requests.Apps;

/// <summary>
/// https://cybozu.dev/ja/kintone/docs/rest-api/apps/get-apps/
/// </summary>
internal class AppsGetRequest : BaseRequest<AppsGetRequest, AppGetResponse>
{
    protected override string _Command { get; } = "apps.json";
    public override void Insert(AppGetResponse? response_)
    {
        if (response_ != null)
        {
            SQLiteManager.Instance.InsertTable(AppGetResponse.TableName(false), AppGetResponse.ListInsertHeader(true), response_.ListInsertValue(true));
        }
    }
}
