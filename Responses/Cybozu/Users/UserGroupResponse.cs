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
using KintoneDeSql.Responses.Cybozu.Groups;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace KintoneDeSql.Responses.Cybozu.Users;

/// <summary>
/// https://cybozu.dev/ja/common/docs/user-api/users/get-users-groups/
/// </summary>
[Table($"{SQLiteManager.CYBOZU_DATABASE}.userGroups")]
internal class UserGroupResponse: BaseToData, ISqlTable, IAppCode
{
    /// <summary>
    /// userGroups 保存
    /// </summary>
    // groups	配列	ユーザーが所属するグループ（ロール）の一覧
    [JsonPropertyName("groups")]
    public List<UserGroupValue> ListGroup { get; set; } = new();

    #region NoJson
    [ColumnEx("userCode", Order = 1, TypeName = "TEXT")]
    public string Code { get; set; } = string.Empty;
    #endregion

    #region ISqlTable
    public static string TableName(bool withCamma_) => typeof(UserGroupResponse).TableName(withCamma_);
    public static IEnumerable<string> ListCreateHeader(bool withCamma_) => MemberInfoExtension.ListCreateHeader(_listColumn(), withCamma_);
    public static IEnumerable<string> ListInsertHeader(bool withCamma_) => MemberInfoExtension.ListInsertHeader(_listColumn(), withCamma_);
    private static IEnumerable<ColumnData> _listColumn()
    {
        const int _SORT = 100;
        var rtn = typeof(UserGroupResponse).ListColumn();
        rtn.AddRange(typeof(UserGroupValue).ListColumn(string.Empty, _SORT));
        return rtn;
    }
    public IEnumerable<IEnumerable<string>> ListInsertValue(bool withCamma_)
    {
        var rtn = new List<IEnumerable<string>>();
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
