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
//internal class RecordAclRequest : BaseSingleton<RecordAclRequest>
internal class RecordAclRequest : BaseRequest<RecordAclRequest, RecordAclResponse>
{
    //private const string _COMMAND = "record/acl.json";
    //public async Task<RecordAclResponse?> Get(string appId_)
    //{
    //    var query = string.Empty;
    //    var paramater = JsonSerializer.Serialize(new { app = appId_ });
    //    var response = await KintoneManager.Instance.KintoneGet<RecordAclResponse?>(HttpMethod.Get, _COMMAND, query, paramater);
    //    //
    //    if (response != null)
    //    {
    //        response.AppId = appId_;
    //    }
    //    return response;
    //}

    //public async Task<RecordAclResponse?> Insert(string appId_)
    //{
    //    var response = await Get(appId_);
    //    if (response != null)
    //    {
    //        SQLiteManager.Instance.InsertTable(RecordAclResponse.TableName(false), RecordAclResponse.ListInsertHeader(true), response.ListInsertValue(true));
    //    }
    //    return response;
    //}
    protected override string _Command { get; } = "record/acl.json";
    public override void Insert(RecordAclResponse? response_)
    {
        if (response_ != null)
        {
            SQLiteManager.Instance.InsertTable(RecordAclResponse.TableName(false), RecordAclResponse.ListInsertHeader(true), response_.ListInsertValue(true));
        }
    }
}
