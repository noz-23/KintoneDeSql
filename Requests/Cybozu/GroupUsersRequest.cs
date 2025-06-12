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
/// https://cybozu.dev/ja/common/docs/user-api/groups/get-groups-users/
/// </summary>
internal class GroupUsersRequest : BaseSingleton<GroupUsersRequest>
{
    private const string _COMMAND = "group/users.json";
    public async Task<ListGroupUserResponse> Get(string code_)
    {
        var rtn = new ListGroupUserResponse();

        var offset = 0;
        var count = 0;
        var size = KintoneManager.CYBOZU_LIMIT;// Cybozu APIの最大値

        do
        {
            //var query = $"code={code_}";
            var query = string.Empty;
            var paramater = JsonSerializer.Serialize(new { code = code_, size = size, offset = offset });

            var response = await KintoneManager.Instance.CybozuGet<ListGroupUserResponse?>(HttpMethod.Get, _COMMAND, query, paramater);
            if (response == null)
            {
                break;
            }

            rtn.ListUser.AddRange(response.ListUser);

            count = response.ListUser.Count;
            offset += count;
        } while (count == size);
        rtn.Code = code_;

        return rtn;
    }

    public async Task<ListGroupUserResponse> Insert(string code_)
    {
        var response = await Get(code_);
        SQLiteManager.Instance.InsertTable(ListGroupUserResponse.TableName(false), ListGroupUserResponse.ListInsertHeader(true), response.ListInsertValue(true));
        return response;
    }
}
