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
internal class RecordAclEvaluateRequest : BaseRequest<RecordAclEvaluateRequest, RecordAclEvaluateResponse>
{
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
