/*
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
/// https://cybozu.dev/ja/kintone/docs/rest-api/apps/settings/get-record-permissions/
/// </summary>
internal class RecordAclRequest : BaseRequest<RecordAclRequest, RecordAclResponse>
{
    protected override string _Command { get; } = "record/acl.json";
    public override void Insert(RecordAclResponse? response_)
    {
        if (response_ != null)
        {
            SQLiteManager.Instance.InsertTable(RecordAclResponse.TableName(false), RecordAclResponse.ListInsertHeader(true), response_.ListInsertValue(true));
        }
    }
}
