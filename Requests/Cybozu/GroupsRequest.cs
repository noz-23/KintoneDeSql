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
using KintoneDeSql.Requests.Apps;
using KintoneDeSql.Responses.Cybozu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace KintoneDeSql.Requests.Cybozu;

/// <summary>
/// https://cybozu.dev/ja/common/docs/user-api/groups/
/// </summary>
internal class GroupsRequest : BaseSingleton<GroupsRequest>
{
    private const string _COMMAND = "groups.json";
    public async Task<ListGroupResponse> Get()
    {
        var rtn = new ListGroupResponse();

        var offset = 0;
        var count = 0;
        var size = KintoneManager.CYBOZU_LIMIT;// Cybozu APIの最大値

        do
        {
            var query = string.Empty;
            var paramater = JsonSerializer.Serialize(new { size = size, offset = offset });
            var response = await KintoneManager.Instance.CybozuGet<ListGroupResponse?>(HttpMethod.Get, _COMMAND, query, paramater);
            if (response == null)
            {
                break;
            }

            rtn.ListGroup.AddRange(response.ListGroup);

            count = response.ListGroup.Count;
            offset += count;
        } while (count == size);

        return rtn;
    }

    public async Task<ListGroupResponse> Insert()
    {
        var response = await Get();
        SQLiteManager.Instance.InsertTable(ListGroupResponse.TableName(false), ListGroupResponse.ListInsertHeader(true), response.ListInsertValue(true));
        return response;
    }
}
