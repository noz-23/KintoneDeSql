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
using KintoneDeSql.Responses.Spaces;
using System.Net.Http;

namespace KintoneDeSql.Requests.Spaces;

/// <summary>
/// https://cybozu.dev/ja/kintone/docs/rest-api/spaces/get-space
/// </summary>
internal class SpacesRequest : BaseSingleton<SpacesRequest>
{
    private const string _COMMAND = "space.json";
    public async Task<SpaceResponse?> Get(string id_)
    {
        var query = $"id={id_}";
        return await KintoneManager.Instance.KintoneGet<SpaceResponse?>(HttpMethod.Get, _COMMAND, query);
    }
    public async Task<SpaceResponse?> Insert(string id_)
    {
        var response = await Get(id_);
        if (response != null)
        {
            SQLiteManager.Instance.InsertTable(SpaceResponse.TableName(false), SpaceResponse.ListInsertHeader(true), response.ListInsertValue(true));
            SQLiteManager.Instance.InsertTable(ListAttachedAppResponse.TableName(false), ListAttachedAppResponse.ListInsertHeader(true), response.ListAttachedApp.ListInsertValue(true));
        }
        return response;
    }
}
