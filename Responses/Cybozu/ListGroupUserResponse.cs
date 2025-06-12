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
/// https://cybozu.dev/ja/common/docs/user-api/groups/get-groups-users/
/// </summary>
[Table($"{SQLiteManager.CYBOZU_DATABASE}.groupUsers")]
internal class ListGroupUserResponse : BaseToData, ISqlTable
{
    /// <summary>
    /// 上位のグループコード
    /// </summary>
    [JsonPropertyName("code")]
    [ColumnEx("code", Order = 10, TypeName = "TEXT", IsUnique = true)]
    public string Code { get; set; } = string.Empty;

    // users	配列	ユーザ情報の一覧
    [JsonPropertyName("users")]
    public List<UserResponse> ListUser { get; set; } = new();

    #region ISqlTable
    public static string TableName(bool withCamma_) => typeof(ListGroupUserResponse).TableName(withCamma_);

    public static List<string> ListCreateHeader(bool withCamma_)
    {
        var rtn = typeof(ListGroupUserResponse).ListColumn();
        rtn.AddRange(typeof(UserResponse).ListColumn("users_"));

        return MemberInfoExtension.ListCreateHeader(rtn, withCamma_);
    }
    public static List<string> ListInsertHeader(bool withCamma_)
    {
        var rtn = typeof(ListGroupUserResponse).ListColumn();
        rtn.AddRange(typeof(UserResponse).ListColumn("users_"));

        return MemberInfoExtension.ListInsertHeader(rtn, withCamma_);
    }

    public List<List<string>> ListInsertValue(bool withCamma_)
    {
        var rtn = new List<List<string>>();

        foreach (var user in ListUser)
        {
            var list = this.ListValue(withCamma_);
            list.AddRange(user.ListValue(withCamma_));
            rtn.Add(list);
        }
        return rtn;
    }
    #endregion

}
