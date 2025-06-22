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
/// https://cybozu.dev/ja/kintone/docs/rest-api/records/get-comments/
/// </summary>
internal class CommentRequest : BaseRequest<CommentRequest, CommentResponse>
{
    protected override string _Command { get; } = "record/comments.json";
    public override void Insert(CommentResponse? response_)
    {
        if (response_ != null)
        {
            SQLiteManager.Instance.InsertTable(CommentResponse.TableName(false), CommentResponse.ListInsertHeader(true), response_.ListInsertValue(true));
            SQLiteManager.Instance.InsertTable(CommentResponseMention.TableName(false), CommentResponseMention.ListInsertHeader(true), response_.ListInsertValueMention(true));
        }
    }
}
