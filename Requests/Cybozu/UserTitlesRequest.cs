/*
 * Kintone De Sql
 * Copyright (c) 2025 noz-23
 *  https://github.com/noz-23/
 * 
 * Licensed under the MIT License 
 * 
 */
using KintoneDeSql.Managers;
using KintoneDeSql.Responses.Cybozu.Organizations;

namespace KintoneDeSql.Requests.Cybozu;

/// <summary>
/// https://cybozu.dev/ja/common/docs/user-api/organizations/get-organizations-users/
/// </summary>
internal class UserTitlesRequest : BaseRequest<UserTitlesRequest, UserTitleResponse>
{
    protected override string _Command { get; } = "organization/users.json";
    public override void Insert(UserTitleResponse? response_)
    {
        if (response_ != null)
        {
            SQLiteManager.Instance.InsertTable(UserTitleResponse.TableName(false), UserTitleResponse.ListInsertHeader(true), response_.ListInsertValue(true));
        }
    }
}
