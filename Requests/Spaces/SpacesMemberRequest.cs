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
using KintoneDeSql.Responses.Spaces;
using System.Net.Http;
using System.Text.Json;

namespace KintoneDeSql.Requests.Spaces;

/// <summary>
/// https://cybozu.dev/ja/kintone/docs/rest-api/spaces/get-space-members/
/// </summary>
internal class SpacesMemberRequest : BaseSingleton<SpacesMemberRequest>
{
    private const string _COMMAND = "space/members.json";
    public async Task<SpaceMemberResponse?> Get(string id_)
    {
        //var query = $"id={id_}";
        var query = string.Empty;
        var paramater = JsonSerializer.Serialize(new { id = id_ });
        var response = await KintoneManager.Instance.KintoneGet<SpaceMemberResponse?>(HttpMethod.Get, _COMMAND, query, paramater);
        if (response != null)
        { 
            response.Id = id_;
        }
        return response;
    }
    public async Task<SpaceMemberResponse?> Insert(string id_)
    {
        var response = await Get(id_);
        if (response != null)
        {
            SQLiteManager.Instance.InsertTable(SpaceMemberResponse.TableName(false), SpaceMemberResponse.ListInsertHeader(true), response.ListInsertValue(true));
        }
        return response;
    }
}
