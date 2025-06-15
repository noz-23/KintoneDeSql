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
using KintoneDeSql.Requests.Apps;
using KintoneDeSql.Requests.Cybozu;
using KintoneDeSql.Requests.Records;
using KintoneDeSql.Responses.Apps;
using KintoneDeSql.Responses.Records;
using System.Data;
using System.Windows;

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
    /// Getボタン押下
    /// </summary>
    public override async Task<int> ControlInsert(string appId_)
    {
        if (RecordDataView == null)
        {
            return 0;
        }

        _ProgressCount?.Invoke(0, RecordDataView.Count, ControlMainTableName);
        foreach (DataRowView row in RecordDataView)
        {
            var recordId = row[Resource.COLUMN_MAIN_TABLE_ID].ToString();
            if (recordId == null)
            {
                continue;
            }

            var count = 0;
            var offset = 0;
            const int _LIMIT = KintoneManager.COMMENT_LIMIT;
            do
            {
                var response = await CommentRequest.Instance.Insert(appId_, recordId,offset, _LIMIT);
                if (response == null)
                {
                    break;
                }
                if (response.ListComment.Count == 0)
                {
                    break;
                }
                //
                count = response.ListComment.Count;
                offset += count;
            } while (count == _LIMIT);
        }
        return RecordDataView.Count;
    }

    public DataView? RecordDataView { get; set; } = null;
}
