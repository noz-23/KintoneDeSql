/*
 * Kintone De Sql
 * Copyright (c) 2025 noz-23
 *  https://github.com/noz-23/
 * 
 * Licensed under the MIT License 
 * 
 */
using KintoneDeSql.Data;
using KintoneDeSql.Extensions;
using KintoneDeSql.Interface;
using KintoneDeSql.Managers;
using System.ComponentModel.DataAnnotations.Schema;

namespace KintoneDeSql.Responses.Records;

/// <summary>
/// https://cybozu.dev/ja/kintone/docs/rest-api/records/get-comments/
/// </summary>
[Table($"{SQLiteManager.SUB_DATABASE}.comments")]
internal class CommentResponse : CommentResponseMention,ISqlTable
{
    #region ISqlTable
    public static new string TableName(bool withCamma_) => typeof(CommentResponse).TableName(withCamma_);
    public static new IEnumerable<string> ListCreateHeader(bool withCamma_) => MemberInfoExtension.ListCreateHeader(_listColumn(), withCamma_);
    public static new IEnumerable<string> ListInsertHeader(bool withCamma_) => MemberInfoExtension.ListInsertHeader(_listColumn(), withCamma_);
    private static IEnumerable<ColumnData> _listColumn()
    {
        const int _SORT = 100;
        var rtn = typeof(CommentResponse).ListColumn();
        rtn.AddRange(typeof(CommentValue).ListColumn(string.Empty, _SORT));
        return rtn;
    }
    public override IEnumerable<IEnumerable<string>> ListInsertValue(bool withCamma_)
    {
        var rtn = new List<IEnumerable<string>>();
        foreach (var comment in ListComment)
        {
            var list = this.ListValue(withCamma_);
            list.AddRange(comment.ListValue(withCamma_));
            rtn.Add(list);
        }
        return rtn;
    }
    #endregion
}
