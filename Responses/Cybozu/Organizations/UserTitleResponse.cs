﻿/*
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

namespace KintoneDeSql.Responses.Cybozu.Organizations;

/// <summary>
/// https://cybozu.dev/ja/common/docs/user-api/organizations/get-organizations-users/
/// https://cybozu.dev/ja/common/docs/user-api/overview/data-structure/#user-title
/// </summary>
[Table($"{SQLiteManager.CYBOZU_DATABASE}.userTitles")]
internal class UserTitleResponse : BaseToData,ISqlTable, IResponseCode
{
    /// <summary>
    /// organization 保存
    /// </summary>
    // userTitles	配列	組織の所属ユーザーとその役職の一覧
    [JsonPropertyName("userTitles")]
    public List<UserTitleValue> ListUserTitle { get; set; } = new();

    #region NoJson
    [ColumnEx("organizationsCode", Order = 1, TypeName = "TEXT", IsUnique = true)]
    public string Code { get; set; } = string.Empty;
    #endregion

    #region ISqlTable
    public static string TableName(bool withCamma_) => typeof(UserTitleResponse).TableName(withCamma_);
    public static IEnumerable<string> ListCreateHeader(bool withCamma_) => MemberInfoExtension.ListCreateHeader(_listColumn(), withCamma_);
    public static IEnumerable<string> ListInsertHeader(bool withCamma_) => MemberInfoExtension.ListInsertHeader(_listColumn(), withCamma_);
    private static IEnumerable<ColumnData> _listColumn()
    {
        const int _SORT = 100;
        var rtn = typeof(UserTitleResponse).ListColumn();
        rtn.AddRange(typeof(UserTitleValue).ListColumn(string.Empty, _SORT));
        return rtn;
    }
    public IEnumerable<IEnumerable<string>> ListInsertValue(bool withCamma_)
    {
        var rtn = new List<IEnumerable<string>>();
        foreach (var user in ListUserTitle)
        {
            var list = this.ListValue(withCamma_);
            list.AddRange(user.ListValue(withCamma_));
            rtn.Add(list);
        }
        return rtn;
    }
    #endregion
}
