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
internal class OrganizationTitlesRequest : BaseRequest<OrganizationTitlesRequest, OrganizationTitleResponse>
{
    protected override string _Command { get; } = "user/organizations.json";
    public override void Insert(OrganizationTitleResponse? response_)
    {
        if (response_ != null)
        {
            SQLiteManager.Instance.InsertTable(OrganizationTitleResponse.TableName(false), OrganizationTitleResponse.ListInsertHeader(true), response_.ListInsertValue(true));
        }
    }
}
