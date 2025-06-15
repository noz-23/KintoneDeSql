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
/// https://cybozu.dev/ja/kintone/docs/rest-api/records/evaluate-record-permissions/
/// </summary>
[Table($"{SQLiteManager.SUB_DATABASE}.recordsAclEvaluate")]
internal class RecordAclEvaluateResponse : RecordAclEvaluateResponseField, ISqlTable
{
    #region ISqlTable
    public new static string TableName(bool withCamma_) => typeof(RecordAclEvaluateResponse).TableName(withCamma_);
    public new static IEnumerable<string> ListCreateHeader(bool withCamma_) => MemberInfoExtension.ListCreateHeader(_listColumn(), withCamma_);
    public new static IEnumerable<string> ListInsertHeader(bool withCamma_) => MemberInfoExtension.ListInsertHeader(_listColumn(), withCamma_);
    private static IEnumerable<ColumnData> _listColumn()
    {
        const int _SORT = 100;
        var rtn = typeof(RecordAclEvaluateResponse).ListColumn();
        rtn.AddRange(typeof(RecordAclEvaluateListValue).ListColumn(string.Empty,_SORT));
        return rtn;
    }

    public override IEnumerable<IEnumerable<string>> ListInsertValue(bool withCamma_)
    {
        var rtn = new List<IList<string>>();
        foreach (var right in ListRight)
        {
            var list = base.ListValue(withCamma_);
            list.AddRange(right.ListValue(withCamma_));
            rtn.Add(list);
        }
        return rtn;
    }
    #endregion
}
