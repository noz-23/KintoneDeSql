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

namespace KintoneDeSql.Responses.Spaces;

/// <summary>
/// https://cybozu.dev/ja/kintone/docs/rest-api/spaces/get-space-members/
/// </summary>
[Table($"{SQLiteManager.SPACE_DATABASE}.spaceMember")]
internal class SpaceMemberResponse : BaseToData, ISqlTable, IResponseId
{
    //members	配列	スペースのメンバーの情報
    [JsonPropertyName("members")]
    public List<SpaceMemberValue> ListSpaceMember { get; set; } = new();

    #region NoJson
    /// <summary>
    /// 上位ののスペース ID
    /// </summary>
    [ColumnEx("id", Order = 1, TypeName = "TEXT")]
    public string Id { get; set; } = string.Empty;
    //
    #endregion

    #region ISqlTable
    public static string TableName(bool withCamma_) => typeof(SpaceMemberResponse).TableName(withCamma_);
    public static IEnumerable<string> ListCreateHeader(bool withCamma_) => MemberInfoExtension.ListCreateHeader(_listColumn(), withCamma_);
    public static IEnumerable<string> ListInsertHeader(bool withCamma_) => MemberInfoExtension.ListInsertHeader(_listColumn(), withCamma_);
    private static IEnumerable<ColumnData> _listColumn()
    {
        var rtn = typeof(SpaceMemberResponse).ListColumn();
        rtn.AddRange(typeof(SpaceMemberValue).ListColumn());
        return rtn;
    }
    public IEnumerable<IEnumerable<string>> ListInsertValue(bool withCamma_)
    {
        var rtn = new List<IList<string>>();

        foreach (var member in ListSpaceMember)
        {
            var list =base.ListValue(withCamma_);
            list.AddRange(member.ListValue(withCamma_));
            rtn.Add(list);
        }

        return rtn;
    }
    #endregion
}
