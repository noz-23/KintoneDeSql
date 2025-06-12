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
/// https://cybozu.dev/ja/common/docs/user-api/organizations/get-organizations-users/
/// </summary>
internal class UserTitlesRequest : BaseSingleton<UserTitlesRequest>
{
    private const string _COMMAND = "organization/users.json";
    public async Task<ListUserTitleRespons> Get(string code_)
    {
        var rtn = new ListUserTitleRespons();

        var offset = 0;
        var count = 0;
        var size = KintoneManager.CYBOZU_LIMIT;// Cybozu APIの最大値

        do
        {
            var query = string.Empty;
            var paramater = JsonSerializer.Serialize(new { code= code_, size = size, offset = offset });

            var response = await KintoneManager.Instance.CybozuGet<ListUserTitleRespons?>(HttpMethod.Get, _COMMAND, query, paramater);
            if (response == null)
            {
                break;
            }

            rtn.ListUserTitle.AddRange(response.ListUserTitle);

            count = response.ListUserTitle.Count;
            offset += count;
        } while (count == size);
        rtn.Code = code_;

        return rtn;
    }

    public async Task<ListUserTitleRespons> Insert(string code_)
    {
        var response = await Get(code_);
        SQLiteManager.Instance.InsertTable(ListUserTitleRespons.TableName(false), ListUserTitleRespons.ListInsertHeader(true), response.ListInsertValue(true));
        return response;
    }
}
