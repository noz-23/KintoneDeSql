/*
 * Kintone De Sql
 * Copyright (c) 2025 noz-23
 *  https://github.com/noz-23/
 * 
 * Licensed under the MIT License 
 * 
 */
using KintoneDeSql.Managers;
using KintoneDeSql.Responses.Cybozu.Groups;

namespace KintoneDeSql.Requests.Cybozu;

/// <summary>
/// https://cybozu.dev/ja/common/docs/user-api/groups/get-groups-users/
/// </summary>
internal class GroupUsersRequest : BaseRequest<GroupUsersRequest, GroupUserResponse>
{
    protected override string _Command { get; } = "group/users.json";
    public override void Insert(GroupUserResponse? response_)
    {
        if (response_ != null)
        {
            SQLiteManager.Instance.InsertTable(GroupUserResponse.TableName(false), GroupUserResponse.ListInsertHeader(true), response_.ListInsertValue(true));
        }
    }
}
