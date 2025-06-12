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

namespace KintoneDeSql.Responses.Cybozu;


/// <summary>
/// https://cybozu.dev/ja/common/docs/user-api/groups/
/// </summary>
[Table($"{SQLiteManager.CYBOZU_DATABASE}.groups")]
internal class ListGroupResponse:ISqlTable
{
    // groups 配列  グループ情報
    [JsonPropertyName("groups")]
    public List<GroupResponse> ListGroup { get; set; } = new();

    #region ISqlTable
    public static string TableName(bool withCamma_) => typeof(ListGroupResponse).TableName(withCamma_);

    public static List<string> ListCreateHeader(bool withCamma_) => typeof(GroupResponse).ListCreateHeader(withCamma_);

    public static List<string> ListInsertHeader(bool withCamma_) => typeof(GroupResponse).ListInsertHeader(withCamma_);
    public List<List<string>> ListInsertValue(bool withCamma_)
    {
        var rtn = new List<List<string>>();

        ListGroup.ForEach(item_ => rtn.Add(item_.ListValue(withCamma_)));

        return rtn;
    }
    #endregion
}
