/*
 * Kintone De Sql
 * Copyright (c) 2025 noz-23
 *  https://github.com/noz-23/
 * 
 * Licensed under the MIT License 
 * 
 */
using KintoneDeSql.Managers;
using KintoneDeSql.Responses.Cybozu.Users;

namespace KintoneDeSql.Requests.Cybozu;

/// <summary>
/// https://cybozu.dev/ja/common/docs/user-api/users/get-users/
/// </summary>
internal class UsersRequest : BaseRequest<UsersRequest, UserResponse>
{
    protected override string _Command { get; } = "users.json";
    public override void Insert(UserResponse? response_)
    {
        if (response_ != null)
        {
            SQLiteManager.Instance.InsertTable(UserResponse.TableName(false), UserResponse.ListInsertHeader(true), response_.ListInsertValue(true));
            SQLiteManager.Instance.InsertTable(UserResponseCustomItem.TableName(false), UserResponseCustomItem.ListInsertHeader(true), response_.ListInsertValueCustom(true));
        }
    }
}
