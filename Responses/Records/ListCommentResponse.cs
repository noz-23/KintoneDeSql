/*
 * Kintone De Sql
 * Copyright (c) 2025 noz-23
 *  https://github.com/noz-23/
 * 
 * Licensed under the MIT License 
 * 
 */
using KintoneDeSql.Attributes;
using KintoneDeSql.Data;
using KintoneDeSql.Extensions;
using KintoneDeSql.Interface;
using KintoneDeSql.Managers;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace KintoneDeSql.Responses.Records;


/// <summary>
/// https://cybozu.dev/ja/kintone/docs/rest-api/records/get-comments/
/// </summary>
[Table($"{SQLiteManager.SUB_DATABASE}.comments")]
internal class ListCommentResponse : BaseToData, ISqlTable
{
    // records 配列（オブジェクト）	レコードの一覧
    [JsonPropertyName("comments")]
    public List<CommentResponse> ListComment { get; set; } = new();

    // older	真偽値	取得したコメントIDより前のコメントがあるかどうか
    [JsonPropertyName("older")]
    [ColumnEx("older", Order = 120, TypeName = "NUMERIC")]
    public bool Older { get; set; } = false;

    // newer 真偽値 取得したコメントIDより後のコメントがあるかどうか
    [JsonPropertyName("newer")]
    [ColumnEx("newer", Order = 121, TypeName = "NUMERIC")]
    public bool Newer { get; set; } = false;

    /// <summary>
    /// 上位のアプリ ID
    /// </summary>
    [ColumnEx("appId", Order = 1, TypeName = "TEXT",IsUnique =true)]
    public string AppId { get; set; } = string.Empty;


    /// <summary>
    /// 上位のレコード ID
    /// </summary>
    [ColumnEx("recordId", Order = 2, TypeName = "TEXT", IsUnique = true)]
    public string RecordId { get; set; } = string.Empty;

    #region ISqlTable
    public static string TableName(bool withCamma_) => typeof(ListCommentResponse).TableName(withCamma_);
    public static List<string> ListCreateHeader(bool withCamma_)
    {
        var list = typeof(ListCommentResponse).ListColumn();
        list.AddRange(typeof(CommentResponse).ListColumn());
        return MemberInfoExtension.ListCreateHeader(list,withCamma_);
    }
    public static List<string> ListInsertHeader(bool withCamma_)
    {
        var list = typeof(ListCommentResponse).ListColumn();
        list.AddRange(typeof(CommentResponse).ListColumn());
        return MemberInfoExtension.ListInsertHeader(list, withCamma_);
    }

    public List<List<string>> ListInsertValue(bool withCamma_)
    {
        var rtn = new List<List<string>>();

        if (ListComment.Any() == false)
        {
            ListComment.Add(new ());
        }

        foreach (var comment_ in ListComment)
        {
            var list = this.ListValue(withCamma_);
            list.AddRange(comment_.ListValue(withCamma_));
            rtn.Add(list);
        }
        return rtn;
    }
    #endregion
}
