/*
 * Kintone De Sql
 * Copyright (c) 2025 noz-23
 *  https://github.com/noz-23/
 * 
 * Licensed under the MIT License 
 * 
 */
using KintoneDeSql.Data;
using KintoneDeSql.Managers;
using KintoneDeSql.Responses.Apis;
using System.Net.Http;

namespace KintoneDeSql.Requests.Apis;

/// <summary>
/// https://cybozu.dev/ja/kintone/docs/rest-api/apis/get-apis/
/// </summary>
internal class ApisRequest:BaseSingleton<ApisRequest>
{
    private const string _COMMAND = "apis.json";

    public async Task<ListApiResponse?> Get()
    {
        return await KintoneManager.Instance.KintoneGet<ListApiResponse?>(HttpMethod.Get, _COMMAND);
    }
    public async Task<ListApiResponse?> Insert()
    {
        var response = await Get();

        if (response != null)
        {
            SQLiteManager.Instance.InsertTable(ListApiResponse.TableName(false), ListApiResponse.ListInsertHeader(true), response.ListInsertValue(true));
        }

        return response;
    }
}
