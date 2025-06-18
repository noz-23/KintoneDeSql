/*
 * Kintone De Sql
 * Copyright (c) 2025 noz-23
 *  https://github.com/noz-23/
 * 
 * Licensed under the MIT License 
 * 
 */
using KintoneDeSql.Managers;
using KintoneDeSql.Responses.Records;

namespace KintoneDeSql.Requests.Records;

/// <summary>
/// https://cybozu.dev/ja/kintone/docs/rest-api/records/evaluate-record-permissions/
/// </summary>
//internal class RecordAclEvaluateRequest : BaseSingleton<RecordAclEvaluateRequest>
internal class RecordAclEvaluateRequest : BaseRequest<RecordAclEvaluateRequest, RecordAclEvaluateResponse>
{
    //private const string _COMMAND = "records/acl/evaluate.json";
    //public async Task<RecordAclEvaluateResponse?> Get(string appId_, IEnumerable<string> listId_, string apiKey_)
    //{
    //    var query = string.Empty;
    //    var paramater = JsonSerializer.Serialize(new { app = appId_, ids = listId_ });
    //    var response = await KintoneManager.Instance.KintoneGet<RecordAclEvaluateResponse?>(HttpMethod.Get, _COMMAND, query, paramater, apiKey_);
    //    if (response != null)
    //    {
    //        response.AppId = appId_;
    //    }
    //    return response;
    //}

    //public async Task<RecordAclEvaluateResponse?> Insert(string appId_, IEnumerable<string> listId_, string apiKey_)
    //{
    //    var response = await Get(appId_, listId_, apiKey_);
    //    if (response != null)
    //    {
    //        SQLiteManager.Instance.InsertTable(RecordAclEvaluateResponse.TableName(false), RecordAclEvaluateResponse.ListInsertHeader(true), response.ListInsertValue(true));
    //        SQLiteManager.Instance.InsertTable(RecordAclEvaluateResponseField.TableName(false), RecordAclEvaluateResponseField.ListInsertHeader(true), response.ListInsertValueField(true));
    //    }
    //    return response;
    //}
    protected override string _Command { get; } = "records/acl/evaluate.json";
    public override void Insert(RecordAclEvaluateResponse? response_)
    {
        if (response_ != null)
        {
            SQLiteManager.Instance.InsertTable(RecordAclEvaluateResponse.TableName(false), RecordAclEvaluateResponse.ListInsertHeader(true), response_.ListInsertValue(true));
            SQLiteManager.Instance.InsertTable(RecordAclEvaluateResponseField.TableName(false), RecordAclEvaluateResponseField.ListInsertHeader(true), response_.ListInsertValueField(true));
        }
    }
}
