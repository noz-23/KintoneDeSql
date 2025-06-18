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
//internal class GroupsRequest : BaseSingleton<GroupsRequest>
internal class GroupsRequest : BaseRequest<GroupsRequest, GroupResponse>
{
    //private const string _COMMAND = "groups.json";
    //public async Task<GroupResponse?> Get(int offset_, int size_ = KintoneManager.CYBOZU_LIMIT)
    //{
    //    var query = string.Empty;
    //    var paramater = JsonSerializer.Serialize(new { size = size_, offset = offset_ });
    //    return await KintoneManager.Instance.CybozuGet<GroupResponse?>(HttpMethod.Get, _COMMAND, query, paramater);
    //}
    //public async Task<GroupResponse?> Insert(int offset_, int size_ = KintoneManager.CYBOZU_LIMIT)
    //{
    //    var response = await Get(offset_, size_);
    //    if (response != null)
    //    {
    //        SQLiteManager.Instance.InsertTable(GroupResponse.TableName(false), GroupResponse.ListInsertHeader(true), response.ListInsertValue(true));
    //    }
    //    return response;
    //}
    protected override string _Command { get; } = "groups.json";
    public override void Insert(GroupResponse? response_)
    {
        if (response_ != null)
        {
            SQLiteManager.Instance.InsertTable(GroupResponse.TableName(false), GroupResponse.ListInsertHeader(true), response_.ListInsertValue(true));
        }
    }
}
