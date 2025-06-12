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

namespace KintoneDeSql.Responses.Cybozu;

/// <summary>
/// https://cybozu.dev/ja/common/docs/user-api/users/get-users-groups/
/// </summary>
[Table($"{SQLiteManager.CYBOZU_DATABASE}.userGroups")]
internal class ListUserGroupResponse: BaseToData, ISqlTable
{
    /// <summary>
    /// 上位のユーザーコード
    /// </summary>
    [JsonPropertyName("code")]
    [ColumnEx("code", Order = 10, TypeName = "TEXT", IsUnique = true)]
    public string Code { get; set; } = string.Empty;

    [JsonPropertyName("groups")]
    public List<UserGroupResponse> ListGroup { get; set; } = new();

    #region ISqlTable
    public static string TableName(bool withCamma_) => typeof(ListUserGroupResponse).TableName(withCamma_);

    public static List<string> ListCreateHeader(bool withCamma_)
    {
        var rtn = typeof(ListUserGroupResponse).ListColumn();
        rtn.AddRange( typeof(UserGroupResponse).ListColumn("groups_"));
        return MemberInfoExtension.ListCreateHeader(rtn, withCamma_);
    }

    public static List<string> ListInsertHeader(bool withCamma_)
    {
        var rtn = typeof(ListUserGroupResponse).ListColumn();
        rtn.AddRange(typeof(UserGroupResponse).ListColumn("groups_"));
        return MemberInfoExtension.ListInsertHeader(rtn, withCamma_);
    }

    public List<List<string>> ListInsertValue(bool withCamma_)
    {
        var rtn = new List<List<string>>();

        foreach (var group in ListGroup)
        {
            var list = this.ListValue(withCamma_);
            list.AddRange(group.ListValue(withCamma_));
            rtn.Add(list);
        }

        return rtn;
    }
    #endregion
}
