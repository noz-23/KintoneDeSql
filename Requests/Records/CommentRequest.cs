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
//internal class CommentRequest : BaseSingleton<CommentRequest>
internal class CommentRequest : BaseRequest<CommentRequest, CommentResponse>
{
    //private const string _COMMAND = "record/comments.json";
    //public async Task<CommentResponse?> Get(string appId_, string record_, int offset_, int limit_ = KintoneManager.COMMENT_LIMIT, string appToken_ = "")
    //{
    //    var query = string.Empty;
    //    var paramater = JsonSerializer.Serialize(new { app = appId_, record = record_, limit = limit_, offset = offset_ });

    //    var response = await KintoneManager.Instance.KintoneGet<CommentResponse?>(HttpMethod.Get, _COMMAND, query, paramater);
    //    if (response != null)
    //    {
    //        response.AppId = appId_;
    //        response.RecordId = record_;
    //    }
    //    return response;
    //}
    //public async Task<CommentResponse?> Insert(string appId_, string record_, int offset_, int limit_ = KintoneManager.COMMENT_LIMIT, string appToken_ = "")
    //{
    //    var response = await Get(appId_, record_, offset_, limit_, appToken_);
    //    if (response != null)
    //    {
    //        SQLiteManager.Instance.InsertTable(CommentResponse.TableName(false), CommentResponse.ListInsertHeader(true), response.ListInsertValue(true));
    //        SQLiteManager.Instance.InsertTable(CommentResponseMention.TableName(false), CommentResponseMention.ListInsertHeader(true), response.ListInsertValueMention(true));
    //    }
    //    return response;
    //}
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
