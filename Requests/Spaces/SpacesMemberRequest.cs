/*
 * Kintone De Sql
 * Copyright (c) 2025 noz-23
 *  https://github.com/noz-23/
 * 
 * Licensed under the MIT License 
 * 
 */
using KintoneDeSql.Managers;
using KintoneDeSql.Responses.Spaces;

namespace KintoneDeSql.Requests.Spaces;

/// <summary>
/// https://cybozu.dev/ja/kintone/docs/rest-api/spaces/get-space-members/
/// </summary>
internal class SpacesMemberRequest : BaseRequest<SpacesMemberRequest, SpaceMemberResponse>
{
    protected override string _Command { get; } = "space/members.json";
    public override void Insert(SpaceMemberResponse? response_)
    {
        if (response_ != null)
        {
            SQLiteManager.Instance.InsertTable(SpaceMemberResponse.TableName(false), SpaceMemberResponse.ListInsertHeader(true), response_.ListInsertValue(true));
        }
    }
}
