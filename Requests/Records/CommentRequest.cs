/*
 * Kintone De Sql
 * Copyright (c) 2025 noz-23
 *  https://github.com/noz-23/
 * 
 * Licensed under the MIT License 
 * 
 */
using KintoneDeSql.Data;
using KintoneDeSql.Files;
using KintoneDeSql.Managers;
using KintoneDeSql.Responses.Records;
using System.Net.Http;
using System.Text.Json;

namespace KintoneDeSql.Requests.Records;

/// <summary>
/// https://cybozu.dev/ja/kintone/docs/rest-api/records/get-comments/
/// </summary>
internal class CommentRequest : BaseSingleton<CommentRequest>
{
    private const string _COMMAND = "record/comments.json";
    public async Task<CommentResponse?> Get(string appId_, string record_, int offset_, int limit_ = KintoneManager.COMMENT_LIMIT, string appToken_ = "")
    {
        //var rtn = new CommentResponse();

        //var offset = 0;
        //var count = 0;
        //var limit = KintoneManager.COMMENT_LIMIT;

        //do
        //{
        //    var query = string.Empty;
        //    var paramater = JsonSerializer.Serialize(new { app = appId_, record = record_, limit = limit, offset = offset });

        //    var response = await KintoneManager.Instance.KintoneGet<CommentResponse?>(HttpMethod.Get, _COMMAND, query, paramater);
        //    if (response == null)
        //    {
        //        break;
        //    }

        //    response.AppId = appId_;
        //    response.RecordId = record_;
        //    LogFile.Instance.WriteLine($"{response.ListComment.Count}");
        //    //
        //    rtn.ListComment.AddRange(response.ListComment);
        //    //
        //    count = response.ListComment.Count;

        //    offset += count;
        //} while (count == limit);
        //rtn.AppId = appId_;

        //return rtn;
        var query = string.Empty;
        var paramater = JsonSerializer.Serialize(new { app = appId_, record = record_, limit = limit_, offset = offset_ });

        var response = await KintoneManager.Instance.KintoneGet<CommentResponse?>(HttpMethod.Get, _COMMAND, query, paramater);
        if (response != null)
        {
            response.AppId = appId_;
            response.RecordId = record_;
        }
        return response;

    }
    public async Task<CommentResponse?> Insert(string appId_, string record_, int offset_, int limit_ = KintoneManager.COMMENT_LIMIT, string appToken_ = "")
    {
        var response = await Get(appId_, record_, offset_, limit_, appToken_);
        if (response != null)
        {
            SQLiteManager.Instance.InsertTable(CommentResponse.TableName(false), CommentResponse.ListInsertHeader(true), response.ListInsertValue(true));
            SQLiteManager.Instance.InsertTable(CommentResponseMention.TableName(false), CommentResponseMention.ListInsertHeader(true), response.ListInsertValueMention(true));
        }
        return response;
    }
}
