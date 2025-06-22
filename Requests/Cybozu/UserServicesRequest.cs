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
/// https://cybozu.dev/ja/common/docs/user-api/users/get-users-services/
/// </summary>
internal class UserServicesRequest : BaseRequest<UserServicesRequest, UsersServiceResponse>
{
    protected override string _Command { get; } = "users/services.json";
    public override void Insert(UsersServiceResponse? response_)
    {
        if (response_ != null)
        {
            SQLiteManager.Instance.InsertTable(UsersServiceResponse.TableName(false), UsersServiceResponse.ListInsertHeader(true), response_.ListInsertValue(true));
        }
    }

}
