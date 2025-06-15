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
using KintoneDeSql.Responses.Records;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using System.Windows.Documents;

namespace KintoneDeSql.Responses.Cybozu.Users;

/// <summary>
/// https://cybozu.dev/ja/common/docs/user-api/overview/data-structure/#user
/// </summary>
[Table($"{SQLiteManager.CYBOZU_DATABASE}.users_Custom")]
internal class UserResponseCustomItem: ISqlTable
{
    /// <summary>
    /// users 保存
    /// </summary>
    // users	配列	ユーザ情報の一覧
    [JsonPropertyName("users")]
    public List<UserValue> ListUser { get; set; } = new();

    #region ISqlTable
    public static string TableName(bool withCamma_) => typeof(UserResponseCustomItem).TableName(withCamma_);
    public static IEnumerable<string> ListCreateHeader(bool withCamma_) => MemberInfoExtension.ListCreateHeader(_listColumn(), withCamma_);
    public static IEnumerable<string> ListInsertHeader(bool withCamma_) => MemberInfoExtension.ListInsertHeader(_listColumn(), withCamma_);
    private static IEnumerable<ColumnData> _listColumn()
    {
        const int _SORT = 100;
        var rtn = typeof(CodeNameValue).ListColumn();
        rtn.AddRange(typeof(CustomItemValue).ListColumn("customItem_", _SORT));
        return rtn;
    }


    public virtual IEnumerable<IEnumerable<string>> ListInsertValue(bool withCamma_) => ListInsertValueCustom(withCamma_);
    public IEnumerable<IEnumerable<string>> ListInsertValueCustom(bool withCamma_)
    {
        var rtn = new List<IEnumerable<string>>();

        foreach (var user in ListUser)
        {
            foreach (var custom in user.ListCustomItemValue)
            {
                var list =user.ListValue(withCamma_, typeof(CodeNameValue));
                list.AddRange(custom.ListValue(withCamma_,typeof(CustomItemValue)));
                rtn.Add(list); 
            }
        }
        return rtn;
    }


    #endregion

}
