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
//internal class UserGroupsRequest : BaseSingleton<UserGroupsRequest>
internal class UserGroupsRequest : BaseRequest<UserGroupsRequest, UserGroupResponse>
{
    //private const string _COMMAND = "user/groups.json";
    //public async Task<UserGroupResponse?> Get(string code_,int offset_, int size_ = KintoneManager.CYBOZU_LIMIT)
    //{
    //    var query = string.Empty;
    //    var paramater = JsonSerializer.Serialize(new { code = code_, offset = offset_, size = size_ });
    //    var response = await KintoneManager.Instance.CybozuGet<UserGroupResponse?>(HttpMethod.Get, _COMMAND, query, paramater);
    //    if (response != null)
    //    {
    //        response.Code = code_;
    //    }
    //    return response;
    //}

    //public async Task<UserGroupResponse?> Insert(string code_, int offset_, int size_ = KintoneManager.CYBOZU_LIMIT)
    //{
    //    var response = await Get(code_, offset_, size_);
    //    if (response != null)
    //    {
    //        SQLiteManager.Instance.InsertTable(UserGroupResponse.TableName(false), UserGroupResponse.ListInsertHeader(true), response.ListInsertValue(true));
    //    }
    //    return response;
    //}
    protected override string _Command { get; } = "user/groups.json";
    public override void Insert(UserGroupResponse? response_)
    {
        if (response_ != null)
        {
            SQLiteManager.Instance.InsertTable(UserGroupResponse.TableName(false), UserGroupResponse.ListInsertHeader(true), response_.ListInsertValue(true));
        }
    }
}
