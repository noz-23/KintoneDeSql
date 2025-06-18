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
/// https://cybozu.dev/ja/kintone/docs/rest-api/apps/settings/get-field-permissions/
/// </summary>
//internal class FieldAclRequest : BaseSingleton<FieldAclRequest>
internal class FieldAclRequest : BaseRequest<FieldAclRequest, FieldAclResponse>
{
    //private const string _COMMAND = "field/acl.json";
    //public async Task<FieldAclResponse?> Get(string appId_)
    //{
    //    var query = string.Empty;
    //    var paramater = JsonSerializer.Serialize(new { app = appId_ });
    //    var response = await KintoneManager.Instance.KintoneGet<FieldAclResponse?>(HttpMethod.Get, _COMMAND, query, paramater);
    //    //
    //    if (response != null)
    //    {
    //        response.AppId = appId_;
    //    }
    //    return response;
    //}

    //public async Task<FieldAclResponse?> Insert(string appId_)
    //{
    //    var response = await Get(appId_);
    //    if (response != null)
    //    {
    //        SQLiteManager.Instance.InsertTable(FieldAclResponse.TableName(false), FieldAclResponse.ListInsertHeader(true), response.ListInsertValue(true));
    //    }
    //    return response;
    //}
    protected override string _Command { get; } = "field/acl.json";
    public override void Insert(FieldAclResponse? response_)
    {
        if (response_ != null)
        {
            SQLiteManager.Instance.InsertTable(FieldAclResponse.TableName(false), FieldAclResponse.ListInsertHeader(true), response_.ListInsertValue(true));
        }
    }
}
