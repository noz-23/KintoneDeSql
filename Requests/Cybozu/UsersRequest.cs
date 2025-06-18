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
//internal class UsersRequest : BaseSingleton<UsersRequest>
internal class UsersRequest : BaseRequest<UsersRequest, UserResponse>
{
    //private const string _COMMAND = "users.json";
    //public async Task<UserResponse?> Get(int offset_, int size_ = KintoneManager.CYBOZU_LIMIT)
    //{
    //    var query = string.Empty;
    //    var paramater = JsonSerializer.Serialize(new { size = size_, offset = offset_ });
    //    return await KintoneManager.Instance.CybozuGet<UserResponse?>(HttpMethod.Get, _COMMAND, query, paramater);
    //}
    //public async Task<UserResponse?> Insert(int offset_, int size_ = KintoneManager.CYBOZU_LIMIT)
    //{
    //    var response = await Get(offset_, size_);
    //    if (response != null)
    //    {
    //        SQLiteManager.Instance.InsertTable(UserResponse.TableName(false), UserResponse.ListInsertHeader(true), response.ListInsertValue(true));
    //        SQLiteManager.Instance.InsertTable(UserResponseCustomItem.TableName(false), UserResponseCustomItem.ListInsertHeader(true), response.ListInsertValueCustom(true));
    //    }
    //    return response;
    //}
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
