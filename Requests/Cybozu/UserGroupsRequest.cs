/*
 * Kintone De Sql
 * Copyright (c) 2025 noz-23
 *  https://github.com/noz-23/
 * 
 * Licensed under the MIT License 
 * 
 */
using KintoneDeSql.Managers;
using KintoneDeSql.Responses.Cybozu.Users;

namespace KintoneDeSql.Requests.Cybozu;

/// <summary>
/// https://cybozu.dev/ja/common/docs/user-api/groups/get-groups-users/
/// </summary>
internal class UserGroupsRequest : BaseRequest<UserGroupsRequest, UserGroupResponse>
{
    protected override string _Command { get; } = "user/groups.json";
    public override void Insert(UserGroupResponse? response_)
    {
        if (response_ != null)
        {
            SQLiteManager.Instance.InsertTable(UserGroupResponse.TableName(false), UserGroupResponse.ListInsertHeader(true), response_.ListInsertValue(true));
        }
    }
}
