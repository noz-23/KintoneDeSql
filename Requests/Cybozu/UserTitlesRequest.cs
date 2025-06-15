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
using KintoneDeSql.Responses.Cybozu.Organizations;
using System.Net.Http;
using System.Text.Json;

namespace KintoneDeSql.Requests.Cybozu;

/// <summary>
/// https://cybozu.dev/ja/common/docs/user-api/organizations/get-organizations-users/
/// </summary>
internal class UserTitlesRequest : BaseSingleton<UserTitlesRequest>
{
    private const string _COMMAND = "organization/users.json";
    public async Task<UserTitleResponse?> Get(string code_, int offset_, int size_ = KintoneManager.CYBOZU_LIMIT)
    {
        var query = string.Empty;
        var paramater = JsonSerializer.Serialize(new { code = code_, offset = offset_, size = size_ });
        var response = await KintoneManager.Instance.CybozuGet<UserTitleResponse?>(HttpMethod.Get, _COMMAND, query, paramater);
        if (response != null) 
        {
            response.Code = code_;
        }
        return response;
    }
    public async Task<UserTitleResponse?> Insert(string code_, int offset_, int size_ = KintoneManager.CYBOZU_LIMIT)
    {
        var response = await Get(code_, offset_, size_);
        if (response != null)
        {
            SQLiteManager.Instance.InsertTable(UserTitleResponse.TableName(false), UserTitleResponse.ListInsertHeader(true), response.ListInsertValue(true));
        }
        return response;
    }
}
