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

namespace KintoneDeSql.Responses.Cybozu.Users;

/// <summary>
/// https://cybozu.dev/ja/common/docs/user-api/users/get-users-services/
/// </summary>

[Table($"{SQLiteManager.CYBOZU_DATABASE}.usersServices")]
internal class UsersServiceResponse : ISqlTable
{
    // users	配列
    [JsonPropertyName("users")]
    public List<UsersServiceValue> ListUser { get; set; } = new();

    #region ISqlTable
    public static string TableName(bool withCamma_) => typeof(UsersServiceResponse).TableName(withCamma_);
    public static IEnumerable<string> ListCreateHeader(bool withCamma_) => typeof(UsersServiceValue).ListCreateHeader(withCamma_);
    public static IEnumerable<string> ListInsertHeader(bool withCamma_) => typeof(UsersServiceValue).ListInsertHeader(withCamma_);
    public IEnumerable<IEnumerable<string>> ListInsertValue(bool withCamma_)
    {
        var rtn = new List<IEnumerable<string>>();

        foreach (var user in ListUser)
        {
            foreach(var service in user.ListService)
            {
                var list = new List<string>() { withCamma_ == true ? $"'{user.Code.ToString()}'" : user.Code.ToString() };
                list.Add(withCamma_ == true ? $"'{service.ToString()}'" : service.ToString());

                rtn.Add(list);
            }
        }

        return rtn;
    }
    #endregion
}
