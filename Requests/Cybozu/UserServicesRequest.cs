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
//internal class UserServicesRequest : BaseSingleton<UserServicesRequest>
internal class UserServicesRequest : BaseRequest<UserServicesRequest, UsersServiceResponse>
{

    //private const string _COMMAND = "users/services.json";
    //public async Task<UsersServiceResponse?> Get(int offset_, int size_ = KintoneManager.CYBOZU_LIMIT)
    //{
    //    var query = string.Empty;
    //    var paramater = JsonSerializer.Serialize(new { size = size_, offset = offset_ });
    //    return await KintoneManager.Instance.CybozuGet<UsersServiceResponse?>(HttpMethod.Get, _COMMAND, query, paramater);
    //}
    //public async Task<UsersServiceResponse?> Insert(int offset_, int size_ = KintoneManager.CYBOZU_LIMIT)
    //{
    //    var response = await Get(offset_, size_);
    //    if (response != null)
    //    {
    //        SQLiteManager.Instance.InsertTable(UsersServiceResponse.TableName(false), UsersServiceResponse.ListInsertHeader(true), response.ListInsertValue(true));
    //    }
    //    return response;
    //}
    protected override string _Command { get; } = "users/services.json";
    public override void Insert(UsersServiceResponse? response_)
    {
        if (response_ != null)
        {
            SQLiteManager.Instance.InsertTable(UsersServiceResponse.TableName(false), UsersServiceResponse.ListInsertHeader(true), response_.ListInsertValue(true));
        }
    }

}
