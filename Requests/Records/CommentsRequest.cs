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
internal class CommentsRequest : BaseSingleton<CommentsRequest>
{
    private const string _COMMAND = "record/comments.json";
    public async Task<ListCommentResponse> Get(string appId_, string record_, string appToken_ = "")
    {
        var rtn = new ListCommentResponse();

        var offset = 0;
        var count = 0;
        var limit = KintoneManager.COMMENT_LIMIT;

        do
        {
            //var query = $"app={appId_}&record={record_}&query=limit {limit} offset {offset}";
            //var paramater = string.Empty;
            var query = string.Empty;
            var paramater = JsonSerializer.Serialize(new { app = appId_, record = record_, limit = limit, offset = offset });

            var response = await KintoneManager.Instance.KintoneGet<ListCommentResponse?>(HttpMethod.Get, _COMMAND, query, paramater);
            if (response == null)
            {
                break;
            }

            response.AppId = appId_;
            response.RecordId = record_;
            LogFile.Instance.WriteLine($"{response.ListComment.Count}");
            //
            rtn.ListComment.AddRange(response.ListComment);
            //
            count = response.ListComment.Count;

            offset += count;
        } while (count == limit);
        rtn.AppId = appId_;

        return rtn;
    }
    public async Task<ListCommentResponse> Insert(string appId_, string record_, string appToken_ = "")
    {
        var response = await Get(appId_, record_, appToken_);
        SQLiteManager.Instance.InsertTable(ListCommentResponse.TableName(false), ListCommentResponse.ListInsertHeader(true), response.ListInsertValue(true));
        return response;
    }
}
