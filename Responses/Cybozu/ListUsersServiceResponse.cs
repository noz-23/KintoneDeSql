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
/// https://cybozu.dev/ja/common/docs/user-api/users/get-users-services/
/// </summary>

[Table($"{SQLiteManager.CYBOZU_DATABASE}.usersServices")]

internal class ListUsersServiceResponse : ISqlTable
{
    // users	配列
    [JsonPropertyName("users")]
    public List<UsersServiceResponse> ListUser { get; set; } = new();

    #region ISqlTable
    public static string TableName(bool withCamma_) => typeof(ListUsersServiceResponse).TableName(withCamma_);
    public static List<string> ListCreateHeader(bool withCamma_) => typeof(UsersServiceResponse).ListCreateHeader(withCamma_);
    public static List<string> ListInsertHeader(bool withCamma_) => typeof(UsersServiceResponse).ListInsertHeader(withCamma_);
    public List<List<string>> ListInsertValue(bool withCamma_)
    {
        var rtn = new List<List<string>>();

        foreach (var user in ListUser)
        {
            foreach(var service in user.ListService)
            {
                var list = new List<string>() { (withCamma_ == true) ? $"'{user.Code.ToString()}'" : user.Code.ToString() };
                list.Add((withCamma_ == true) ? $"'{service.ToString()}'" : service.ToString());

                rtn.Add(list);
            }
        }

        return rtn;
    }
    #endregion
}
