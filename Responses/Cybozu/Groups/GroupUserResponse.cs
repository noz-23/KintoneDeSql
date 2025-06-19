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
using KintoneDeSql.Responses.Cybozu.Users;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace KintoneDeSql.Responses.Cybozu.Groups;

/// <summary>
/// https://cybozu.dev/ja/common/docs/user-api/groups/get-groups-users/
/// </summary>
[Table($"{SQLiteManager.CYBOZU_DATABASE}.groupUsers")]
internal class GroupUserResponse : BaseToData, ISqlTable, IResponseCode
{
    /// <summary>
    /// groupUsers で保存
    /// </summary>
    // users	配列	ユーザ情報の一覧
    [JsonPropertyName("users")]
    public List<UserValue> ListUser { get; set; } = new();

    #region NoJson
    [ColumnEx("groupCode", Order = 1, TypeName = "TEXT",IsUnique =true)]
    public string Code { get; set; } = string.Empty;
    #endregion

    #region ISqlTable
    public static string TableName(bool withCamma_) => typeof(GroupUserResponse).TableName(withCamma_);
    public static IEnumerable<string> ListCreateHeader(bool withCamma_) => MemberInfoExtension.ListCreateHeader(_listColumn(), withCamma_);
    public static IEnumerable<string> ListInsertHeader(bool withCamma_) => MemberInfoExtension.ListInsertHeader(_listColumn(), withCamma_);
    private static IEnumerable<ColumnData> _listColumn()
    {
        const int _SORT = 100;
        var rtn = typeof(GroupUserResponse).ListColumn();
        rtn.AddRange(typeof(UserValue).ListColumn(string.Empty, _SORT));
        return rtn;
    }
    public IEnumerable<IEnumerable<string>> ListInsertValue(bool withCamma_)
    {
        var rtn = new List<IEnumerable<string>>();
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
