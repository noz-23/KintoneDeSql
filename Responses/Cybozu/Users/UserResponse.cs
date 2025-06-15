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

namespace KintoneDeSql.Responses.Cybozu.Users;

/// <summary>
/// https://cybozu.dev/ja/common/docs/user-api/users/get-users/
/// https://cybozu.dev/ja/common/docs/user-api/overview/data-structure/#user
/// </summary>
[Table($"{SQLiteManager.CYBOZU_DATABASE}.users")]
internal class UserResponse: UserResponseCustomItem,ISqlTable
{
    #region ISqlTable
    public static new string TableName(bool withCamma_) => typeof(UserResponse).TableName(withCamma_);
    public static new IEnumerable<string> ListCreateHeader(bool withCamma_) => typeof(UserValue).ListCreateHeader(withCamma_);
    public static new IEnumerable<string> ListInsertHeader(bool withCamma_) => typeof(UserValue).ListInsertHeader(withCamma_);
    public override IEnumerable<IEnumerable<string>> ListInsertValue(bool withCamma_)
    {
        var rtn = new List<IEnumerable<string>>();
        ListUser.ForEach(item_ => rtn.Add(item_.ListValue(withCamma_)));
        return rtn;
    }
    #endregion
}
