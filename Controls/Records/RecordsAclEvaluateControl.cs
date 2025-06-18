/*
 * Kintone De Sql
 * Copyright (c) 2025 noz-23
 *  https://github.com/noz-23/
 * 
 * Licensed under the MIT License 
 * 
 */
using KintoneDeSql.Controls.Apps;
using KintoneDeSql.Managers;
using KintoneDeSql.Properties;
using KintoneDeSql.Requests.Records;
using KintoneDeSql.Responses.Records;
using System.Data;

namespace KintoneDeSql.Controls.Records;
/// <summary>
/// https://cybozu.dev/ja/kintone/docs/rest-api/records/evaluate-record-permissions/
/// </summary>
internal class RecordAclEvaluateControl : BaseAppControl
{
    /// <summary>
    /// コンストラクタ
    /// </summary>
    public RecordAclEvaluateControl() : base()
    {
        ControlMainTableName= RecordAclEvaluateResponse.TableName(false);
        ControlSubTableName = RecordAclEvaluateResponseField.TableName(false);
    }

    public DataView? RecordDataView { get; set; } = null;

    /// <summary>
    /// Getボタン押下
    /// </summary>
    public override async Task<int> ControlInsert(string appId_, string apiKey_)
    {
        if (RecordDataView == null)
        {
            return 0;
        }

        int count = 0;
        _ProgressCount?.Invoke(0, RecordDataView.Count, ControlMainTableName);
        var listId = new List<string>();

        foreach (DataRowView row in RecordDataView)
        {
            var recordId = row[Resource.COLUMN_MAIN_TABLE_ID].ToString();
            if (recordId == null)
            {

                continue;
            }
            listId.Add(recordId);
        }

        foreach (var list in listId.Chunk(KintoneManager.API_LIMIT))
        {
            var response = await RecordAclEvaluateRequest.Instance.Insert(appId_, list,apiKey_);
            count += list.Count();

            _ProgressCount?.Invoke(count);
        }

        return count;
    }
}
