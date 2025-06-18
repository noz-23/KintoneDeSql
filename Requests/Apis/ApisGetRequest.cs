/*
 * Kintone De Sql
 * Copyright (c) 2025 noz-23
 *  https://github.com/noz-23/
 * 
 * Licensed under the MIT License 
 * 
 */
using KintoneDeSql.Managers;
using KintoneDeSql.Responses.Apis;

namespace KintoneDeSql.Requests.Apis;

/// <summary>
/// https://cybozu.dev/ja/kintone/docs/rest-api/apis/get-apis/
/// </summary>
//internal class ApisGetRequest:BaseSingleton<ApisGetRequest>
internal class ApisGetRequest : BaseRequest<ApisGetRequest, ApiGetResponse>
{
    //private const string _COMMAND = "apis.json";
    //public async Task<ApiGetResponse?> Get()
    //{
    //    return await KintoneManager.Instance.KintoneGet<ApiGetResponse?>(HttpMethod.Get, _COMMAND);
    //}
    //public async Task<ApiGetResponse?> Insert()
    //{
    //    var response = await Get();

    //    if (response != null)
    //    {
    //        SQLiteManager.Instance.InsertTable(ApiGetResponse.TableName(false), ApiGetResponse.ListInsertHeader(true), response.ListInsertValue(true));
    //    }

    //    return response;
    //}
    protected override string _Command { get; } = "apis.json";
    public override void Insert(ApiGetResponse? response_)
    {
        if (response_ != null)
        {
            SQLiteManager.Instance.InsertTable(ApiGetResponse.TableName(false), ApiGetResponse.ListInsertHeader(true), response_.ListInsertValue(true));
        }
    }
}
