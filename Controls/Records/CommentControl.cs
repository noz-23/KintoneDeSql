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
/// https://cybozu.dev/ja/kintone/docs/rest-api/records/get-comments/
/// </summary>
internal class CommentControl : BaseAppControl
{
    /// <summary>
    /// コンストラクタ
    /// </summary>
    public CommentControl() : base()
    {
        ControlMainTableName = CommentResponse.TableName(false);
        // 下の表示
        ControlSubTableName = CommentResponseMention.TableName(false);
    }

    /// <summary>
    /// 上位のレコード表示
    /// </summary>
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

        var count = 0;
        _ProgressCount?.Invoke(count, RecordDataView.Count, ControlMainTableName);
        foreach (DataRowView row in RecordDataView)
        {
            var recordId = row[Resource.COLUMN_MAIN_TABLE_ID].ToString();
            if (recordId == null)
            {
                continue;
            }

            await CommentRequest.Instance.InsertAll(appId_, apiKey_, recordId, KintoneManager.COMMENT_LIMIT);
            _ProgressCount?.Invoke(++count);
        }
        return RecordDataView.Count;
    }
}
