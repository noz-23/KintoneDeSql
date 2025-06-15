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
/// https://cybozu.dev/ja/kintone/docs/rest-api/records/evaluate-record-permissions/
/// </summary>
[Table($"{SQLiteManager.SUB_DATABASE}.recordsAclEvaluate_Field")]
internal class RecordAclEvaluateResponseField : BaseToData, ISqlTable
{
    //rights	配列	アクセス権の設定の一覧
    [JsonPropertyName("rights")]
    public List<RecordAclEvaluateListValue> ListRight { get; set; } = new();

    #region NoJson
    /// <summary>
    /// 上位のアプリ ID
    /// </summary>
    [ColumnEx("appId", Order = 1, TypeName = "TEXT", IsUnique = true)]
    public string AppId { get; set; } = string.Empty;
    #endregion

    #region ISqlTable
    public static string TableName(bool withCamma_) => typeof(RecordAclEvaluateResponseField).TableName(withCamma_);
    public static IEnumerable<string> ListCreateHeader(bool withCamma_) => MemberInfoExtension.ListCreateHeader(_listColumn(), withCamma_);
    public static IEnumerable<string> ListInsertHeader(bool withCamma_) => MemberInfoExtension.ListInsertHeader(_listColumn(), withCamma_);
    private static IEnumerable<ColumnData> _listColumn()
    {
        const int _SORT = 100;
        var rtn = typeof(RecordAclEvaluateResponseField).ListColumn();
        rtn.AddRange(typeof(RecordAclEvaluateListValueBase).ListColumn(string.Empty, _SORT));
        rtn.AddRange(typeof(FieldAlcValue).ListColumn(string.Empty, _SORT + _SORT));
        return rtn;
    }

    public virtual IEnumerable<IEnumerable<string>> ListInsertValue(bool withCamma_) => ListInsertValueField(withCamma_);
    public IEnumerable<IEnumerable<string>> ListInsertValueField(bool withCamma_)
    {
        var rtn = new List<IList<string>>();

        foreach (var right in ListRight)
        {
            foreach (var field in right.ListField)
            {
                field.Value.FieldKey = field.Key;
                //
                var list = base.ListValue(withCamma_,typeof(RecordAclEvaluateResponseField));
                list.AddRange(right.ListValue(withCamma_, typeof(RecordAclEvaluateListValueBase)));
                list.AddRange(field.Value.ListValue(withCamma_));
                rtn.Add(list);

            }
        }
        return rtn;
    }
    #endregion
}
