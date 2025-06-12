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
/// https://cybozu.dev/ja/common/docs/user-api/organizations/
/// </summary>
internal class OrganizationsRequest : BaseSingleton<OrganizationsRequest>
{
    private const string _COMMAND = "organizations.json";
    public async Task<ListOrganizationResponse> Get()
    {
        var rtn = new ListOrganizationResponse();

        var offset = 0;
        var count = 0;
        var size = 100;// Cybozu APIの最大値

        do
        {
            var query = string.Empty;
            var paramater = JsonSerializer.Serialize(new { size = size, offset = offset });
            var response = await KintoneManager.Instance.CybozuGet<ListOrganizationResponse?>(HttpMethod.Get, _COMMAND, query, paramater);
            if (response == null)
            {
                break;
            }

            rtn.ListOrganization.AddRange(response.ListOrganization);

            count = response.ListOrganization.Count;
            offset += count;
        } while (count == size);

        return rtn;

    }

    public async Task<ListOrganizationResponse> Insert()
    {
        var response = await Get();
        SQLiteManager.Instance.InsertTable(ListOrganizationResponse.TableName(false), ListOrganizationResponse.ListInsertHeader(true), response.ListInsertValue(true));
        return response;
    }
}
