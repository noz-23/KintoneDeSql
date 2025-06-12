/*
 * Kintone De Sql
 * Copyright (c) 2025 noz-23
 *  https://github.com/noz-23/
 * 
 * Licensed under the MIT License 
 * 
 */
using KintoneDeSql.Data;
using KintoneDeSql.Managers;
using KintoneDeSql.Responses.Cybozu;
using System.Net.Http;
using System.Text.Json;

namespace KintoneDeSql.Requests.Cybozu;

/// <summary>
/// https://cybozu.dev/ja/common/docs/user-api/groups/
/// </summary>
internal class UserGroupsRequest : BaseSingleton<UserGroupsRequest>
{
    private const string _COMMAND = "user/groups.json";
    public async Task<ListUserGroupResponse> Get(string code_)
    {
        //var query = $"code={code_}";
        //var paramater = string.Empty;
        var query = string.Empty;
        var paramater = JsonSerializer.Serialize(new { code = code_ });
        var response = await KintoneManager.Instance.CybozuGet<ListUserGroupResponse?>(HttpMethod.Get, _COMMAND, query, paramater);
        // 値入れ
        if (response != null)
        {
            response.Code = code_;
        }
        //
        return response;

    }

    public async Task<ListUserGroupResponse> Insert(string code_)
    {
        var response = await Get(code_);
        SQLiteManager.Instance.InsertTable(ListUserGroupResponse.TableName(false), ListUserGroupResponse.ListInsertHeader(true), response.ListInsertValue(true));
        return response;
    }
}
