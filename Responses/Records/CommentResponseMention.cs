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
using KintoneDeSql.Responses.Commons;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace KintoneDeSql.Responses.Records;

/// <summary>
/// https://cybozu.dev/ja/kintone/docs/rest-api/records/get-comments/
/// </summary>
[Table($"{SQLiteManager.SUB_DATABASE}.comments_Mention")]
internal class CommentResponseMention : CommentResponseBase, ISqlTable
{
    // older	真偽値	取得したコメントIDより前のコメントがあるかどうか
    [JsonPropertyName("older")]
    [ColumnEx("older", Order = 110, TypeName = "NUMERIC")]
    public bool Older { get; set; } = false;

    // newer 真偽値 取得したコメントIDより後のコメントがあるかどうか
    [JsonPropertyName("newer")]
    [ColumnEx("newer", Order = 111, TypeName = "NUMERIC")]
    public bool Newer { get; set; } = false;

    #region ISqlTable
    public static string TableName(bool withCamma_) => typeof(CommentResponseMention).TableName(withCamma_);
    public static IEnumerable<string> ListCreateHeader(bool withCamma_) => MemberInfoExtension.ListCreateHeader(_listColumn(), withCamma_);
    public static IEnumerable<string> ListInsertHeader(bool withCamma_) => MemberInfoExtension.ListInsertHeader(_listColumn(), withCamma_);
    private static IEnumerable<ColumnData> _listColumn()
    {
        const int _SORT = 100;
        var rtn = typeof(CommentResponseBase).ListColumn();
        rtn.AddRange(typeof(CommentValueBase).ListColumn(string.Empty, _SORT));
        rtn.AddRange(typeof(MentionValue).ListColumn(string.Empty, _SORT+ _SORT));
        return rtn;
    }
    public virtual IEnumerable<IEnumerable<string>> ListInsertValue(bool withCamma_) => ListInsertValueMention(withCamma_);
    public IEnumerable<IEnumerable<string>> ListInsertValueMention(bool withCamma_)
    {
        var rtn = new List<IEnumerable<string>>();

        foreach (var comment in ListComment)
        {
            foreach (var mention in comment.ListMention)
            {
                var list = this.ListValue(withCamma_, typeof(CommentResponseBase));
                list.AddRange(comment.ListValue(withCamma_,typeof(CommentValueBase)));
                list.AddRange(mention.ListValue(withCamma_));
                rtn.Add(list);
            }
        }
        return rtn;
    }
    #endregion
}
