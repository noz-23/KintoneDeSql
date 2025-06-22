/*
 * Kintone De Sql
 * Copyright (c) 2025 noz-23
 *  https://github.com/noz-23/
 * 
 * Licensed under the MIT License 
 * 
 */
using KintoneDeSql.Managers;
using KintoneDeSql.Responses.Apps.Permissions;

namespace KintoneDeSql.Requests.Apps.Permissions;

/// <summary>
/// https://cybozu.dev/ja/kintone/docs/rest-api/apps/settings/get-app-permissions/
/// </summary>
internal class AppAclRequest : BaseRequest<AppAclRequest, AppAclResponse>
{
    protected override string _Command { get; } = "app/acl.json";
    public override void Insert(AppAclResponse? response_)
    {
        if (response_ != null)
        {
            SQLiteManager.Instance.InsertTable(AppAclResponse.TableName(false), AppAclResponse.ListInsertHeader(true), response_.ListInsertValue(true));
        }
    }
}
