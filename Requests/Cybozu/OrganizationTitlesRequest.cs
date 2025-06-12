/*
 * Reprise Report Log Analyzer
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
/// https://cybozu.dev/ja/common/docs/user-api/users/get-users-organizations/
/// </summary>
internal class OrganizationTitlesRequest : BaseSingleton<OrganizationTitlesRequest>
{
    private const string _COMMAND = "user/organizations.json";
    public async Task<ListOrganizationTitleResponse?> Get(string code_)
    {
        //var query = $"code={code_}";
        //var paramater = string.Empty;
        var query = string.Empty;
        var paramater = JsonSerializer.Serialize(new { code = code_ });
        var response = await KintoneManager.Instance.CybozuGet<ListOrganizationTitleResponse?>(HttpMethod.Get, _COMMAND, query, paramater);
        // 値入れ
        if (response != null)
        {
            response.Code = code_;
        }
        //
        return response;
    }

    public async Task<ListOrganizationTitleResponse?> Insert(string code_)
    {
        var response = await Get(code_);
        if (response != null)
        {
            SQLiteManager.Instance.InsertTable(ListOrganizationTitleResponse.TableName(false), ListOrganizationTitleResponse.ListInsertHeader(true), response.ListInsertValue(true));
        }
        return response;
    }
}
