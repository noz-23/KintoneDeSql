/*
 * Kintone De Sql
 * Copyright (c) 2025 noz-23
 *  https://github.com/noz-23/
 * 
 * Licensed under the MIT License 
 * 
 */
using KintoneDeSql.Managers;
using KintoneDeSql.Responses.Cybozu.Organizations;

namespace KintoneDeSql.Requests.Cybozu;

/// <summary>
/// https://cybozu.dev/ja/common/docs/user-api/organizations/
/// </summary>
internal class OrganizationsRequest : BaseRequest<OrganizationsRequest, OrganizationResponse>
{
    protected override string _Command { get; } = "organizations.json";
    public override void Insert(OrganizationResponse? response_)
    {
        if (response_ != null)
        {
            SQLiteManager.Instance.InsertTable(OrganizationResponse.TableName(false), OrganizationResponse.ListInsertHeader(true), response_.ListInsertValue(true));
        }
    }
}
