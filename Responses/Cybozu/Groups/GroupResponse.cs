/*
 * Kintone De Sql
 * Copyright (c) 2025 noz-23
 *  https://github.com/noz-23/
 * 
 * Licensed under the MIT License 
 * 
 */
using KintoneDeSql.Extensions;
using KintoneDeSql.Interface;
using KintoneDeSql.Managers;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace KintoneDeSql.Responses.Cybozu.Groups;


/// <summary>
/// https://cybozu.dev/ja/common/docs/user-api/groups/
/// </summary>
[Table($"{SQLiteManager.CYBOZU_DATABASE}.groups")]
internal class GroupResponse:ISqlTable , IResponseCount
{
    // groups 配列  グループ情報
    [JsonPropertyName("groups")]
    public List<GroupValue> ListGroup { get; set; } = new();

    #region ISqlTable
    public static string TableName(bool withCamma_) => typeof(GroupResponse).TableName(withCamma_);
    public static IEnumerable<string> ListCreateHeader(bool withCamma_) => typeof(GroupValue).ListCreateHeader(withCamma_);
    public static IEnumerable<string> ListInsertHeader(bool withCamma_) => typeof(GroupValue).ListInsertHeader(withCamma_);
    public IEnumerable<IEnumerable<string>> ListInsertValue(bool withCamma_)
    {
        var rtn = new List<IList<string>>();
        ListGroup.ForEach(group_ => rtn.Add(group_.ListValue(withCamma_)));
        return rtn;
    }
    #endregion

    #region
    public int Count { get => ListGroup.Count; }
    #endregion
}
