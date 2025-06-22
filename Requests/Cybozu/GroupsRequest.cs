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
/// https://cybozu.dev/ja/common/docs/user-api/groups/
/// </summary>
internal class GroupsRequest : BaseRequest<GroupsRequest, GroupResponse>
{
    protected override string _Command { get; } = "groups.json";
    public override void Insert(GroupResponse? response_)
    {
        if (response_ != null)
        {
            SQLiteManager.Instance.InsertTable(GroupResponse.TableName(false), GroupResponse.ListInsertHeader(true), response_.ListInsertValue(true));
        }
    }
}
