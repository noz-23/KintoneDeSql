/*
 * Kintone De Sql
 * Copyright (c) 2025 noz-23
 *  https://github.com/noz-23/
 * 
 * Licensed under the MIT License 
 * 
 */
using KintoneDeSql.Data;
using KintoneDeSql.Files;
using KintoneDeSql.Managers;
using KintoneDeSql.Responses.Cybozu;
using System.Net.Http;
using System.Text.Json;

namespace KintoneDeSql.Requests.Cybozu;

/// <summary>
/// https://cybozu.dev/ja/common/docs/user-api/users/
/// </summary>
internal class UsersRequest : BaseSingleton<UsersRequest>
{
    private const string _COMMAND = "users.json";
    public async Task<ListUserResponse> Get()
    {
        var rtn = new ListUserResponse();

        var offset = 0;
        var count = 0;
        var size = KintoneManager.CYBOZU_LIMIT;// Cybozu APIの最大値

        do
        {
            var query =string.Empty;
            var paramater = JsonSerializer.Serialize(new { size = size, offset= offset });
            var response = await KintoneManager.Instance.CybozuGet<ListUserResponse?>(HttpMethod.Get, _COMMAND, query, paramater);
            if (response == null)
            {
                break;
            }

            // 値入れ
            response.ListUser.ForEach( user =>user.ListCustomItemValue.ForEach(item_ => item_.Id = user.Id));

            rtn.ListUser.AddRange(response.ListUser);

            count = response.ListUser.Count;
            offset += count;
        } while(count == size);

        return rtn;
    }

    public async Task<ListUserResponse> Insert()
    {
        var response = await Get();
        SQLiteManager.Instance.InsertTable(ListUserResponse.TableName(false), ListUserResponse.ListInsertHeader(true), response.ListInsertValue(true));
        foreach (var user in response.ListUser)
        {
            SQLiteManager.Instance.InsertTable(ListCustomItemValueResponse.TableName(false), ListCustomItemValueResponse.ListInsertHeader(true), user.ListCustomItemValue.ListInsertValue(true));
            LogFile.Instance.WriteLine($"Users[{user.ListCustomItemValue.Count}]");
        }

        return response;
    }
}
