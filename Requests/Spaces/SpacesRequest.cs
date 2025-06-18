/*
 * Kintone De Sql
 * Copyright (c) 2025 noz-23
 *  https://github.com/noz-23/
 * 
 * Licensed under the MIT License 
 * 
 */
using KintoneDeSql.Managers;
using KintoneDeSql.Responses.Spaces;

namespace KintoneDeSql.Requests.Spaces;

/// <summary>
/// https://cybozu.dev/ja/kintone/docs/rest-api/spaces/get-space
/// </summary>
//internal class SpacesRequest : BaseSingleton<SpacesRequest>
internal class SpacesRequest : BaseRequest<SpacesRequest, SpaceResponse>
{
    //private const string _COMMAND = "space.json";
    //public async Task<SpaceResponse?> Get(string id_)
    //{
    //    var query = string.Empty;
    //    var paramater = JsonSerializer.Serialize(new { id = id_ });
    //    return await KintoneManager.Instance.KintoneGet<SpaceResponse?>(HttpMethod.Get, _COMMAND, query, paramater);
    //}
    //public async Task<SpaceResponse?> Insert(string id_)
    //{
    //    var response = await Get(id_);
    //    if (response != null)
    //    {
    //        SQLiteManager.Instance.InsertTable(SpaceResponse.TableName(false), SpaceResponse.ListInsertHeader(true), response.ListInsertValue(true));
    //        SQLiteManager.Instance.InsertTable(SpaceResponseAttachedApps.TableName(false), SpaceResponseAttachedApps.ListInsertHeader(true), response.ListInsertValueAttachedApp(true));
    //    }
    //    return response;
    //}
    protected override string _Command { get; } = "space.json";
    public override void Insert(SpaceResponse? response_)
    {
        if (response_ != null)
        {
            SQLiteManager.Instance.InsertTable(SpaceResponse.TableName(false), SpaceResponse.ListInsertHeader(true), response_.ListInsertValue(true));
            SQLiteManager.Instance.InsertTable(SpaceResponseAttachedApps.TableName(false), SpaceResponseAttachedApps.ListInsertHeader(true), response_.ListInsertValueAttachedApp(true));
        }
    }
}
