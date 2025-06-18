/*
 * Reprise Report Log Analyzer
 * Copyright (c) 2025 noz-23
 *  https://github.com/noz-23/
 * 
 * Licensed under the MIT License 
 * 
 */
using KintoneDeSql.Managers;
using KintoneDeSql.Responses.Cybozu.Users;

namespace KintoneDeSql.Requests.Cybozu;

/// <summary>
/// https://cybozu.dev/ja/common/docs/user-api/users/get-users-organizations/
/// </summary>
//internal class OrganizationTitlesRequest : BaseSingleton<OrganizationTitlesRequest>
internal class OrganizationTitlesRequest : BaseRequest<OrganizationTitlesRequest, OrganizationTitleResponse>
{
    //private const string _COMMAND = "user/organizations.json";
    //public async Task<OrganizationTitleResponse?> Get(string code_)
    //{
    //    var query = string.Empty;
    //    var paramater = JsonSerializer.Serialize(new { code = code_ });
    //    var response = await KintoneManager.Instance.CybozuGet<OrganizationTitleResponse?>(HttpMethod.Get, _COMMAND, query, paramater);
    //    if (response != null)
    //    {
    //        response.Code = code_;
    //    }
    //    return response;
    //}
    //public async Task<OrganizationTitleResponse?> Insert(string code_)
    //{
    //    var response = await Get(code_);
    //    if (response != null)
    //    {
    //        SQLiteManager.Instance.InsertTable(OrganizationTitleResponse.TableName(false), OrganizationTitleResponse.ListInsertHeader(true), response.ListInsertValue(true));
    //    }
    //    return response;
    //}
    protected override string _Command { get; } = "user/organizations.json";
    public override void Insert(OrganizationTitleResponse? response_)
    {
        if (response_ != null)
        {
            SQLiteManager.Instance.InsertTable(OrganizationTitleResponse.TableName(false), OrganizationTitleResponse.ListInsertHeader(true), response_.ListInsertValue(true));
        }
    }
}
