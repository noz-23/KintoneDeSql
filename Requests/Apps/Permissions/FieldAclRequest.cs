﻿/*
 * Kintone De Sql
 * Copyright (c) 2025 noz-23
 *  https://github.com/noz-23/
 * 
 * Licensed under the MIT License 
 * 
 */
using KintoneDeSql.Managers;
using KintoneDeSql.Responses.Apps.Permissions;

namespace KintoneDeSql.Requests.Apps.Permissions;

/// <summary>
/// https://cybozu.dev/ja/kintone/docs/rest-api/apps/settings/get-field-permissions/
/// </summary>
internal class FieldAclRequest : BaseRequest<FieldAclRequest, FieldAclResponse>
{
    protected override string _Command { get; } = "field/acl.json";
    public override void Insert(FieldAclResponse? response_)
    {
        if (response_ != null)
        {
            SQLiteManager.Instance.InsertTable(FieldAclResponse.TableName(false), FieldAclResponse.ListInsertHeader(true), response_.ListInsertValue(true));
        }
    }
}
