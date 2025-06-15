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
using KintoneDeSql.Responses.Apps;
using KintoneDeSql.Responses.Cybozu.Groups;
using System.Net.Http;
using System.Text.Json;

namespace KintoneDeSql.Requests.Cybozu;

/// <summary>
/// https://cybozu.dev/ja/common/docs/user-api/groups/
/// </summary>
internal class GroupsRequest : BaseSingleton<GroupsRequest>
{
    private const string _COMMAND = "groups.json";
    public async Task<GroupResponse?> Get(int offset_, int size_ = KintoneManager.CYBOZU_LIMIT)
    {
        var query = string.Empty;
        var paramater = JsonSerializer.Serialize(new { size = size_, offset = offset_ });
        return await KintoneManager.Instance.CybozuGet<GroupResponse?>(HttpMethod.Get, _COMMAND, query, paramater);
    }

    public async Task<GroupResponse?> Insert(int offset_, int size_ = KintoneManager.CYBOZU_LIMIT)
    {
        var response = await Get(offset_, size_);
        if (response != null)
        {
            SQLiteManager.Instance.InsertTable(GroupResponse.TableName(false), GroupResponse.ListInsertHeader(true), response.ListInsertValue(true));
        }
        return response;
    }
}
