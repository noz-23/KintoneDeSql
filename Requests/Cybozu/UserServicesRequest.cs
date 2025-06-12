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
/// https://cybozu.dev/ja/common/docs/user-api/users/get-users-services/
/// </summary>
internal class UserServicesRequest : BaseSingleton<UserServicesRequest>
{

    private const string _COMMAND = "users/services.json";
    public async Task<ListUsersServiceResponse> Get()
    {
        var rtn = new ListUsersServiceResponse();

        var offset = 0;
        var count = 0;
        var size = KintoneManager.CYBOZU_LIMIT;// Cybozu APIの最大値

        do
        {
            var query = string.Empty;
            var paramater = JsonSerializer.Serialize(new { size = size, offset = offset });
            var response = await KintoneManager.Instance.CybozuGet<ListUsersServiceResponse?>(HttpMethod.Get, _COMMAND, query, paramater);
            if (response == null)
            {
                break;
            }

            // 値入れ
            rtn.ListUser.AddRange(response.ListUser);

            count = response.ListUser.Count;
            offset += count;
        } while (count == size);

        return rtn;
    }

    public async Task<ListUsersServiceResponse> Insert()
    {
        var response = await Get();
        SQLiteManager.Instance.InsertTable(ListUsersServiceResponse.TableName(false), ListUsersServiceResponse.ListInsertHeader(true), response.ListInsertValue(true));
        return response;
    }
}
