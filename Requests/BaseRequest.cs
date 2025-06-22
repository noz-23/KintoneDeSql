/*
 * Kintone De Sql
 * Copyright (c) 2025 noz-23
 *  https://github.com/noz-23/
 * 
 * Licensed under the MIT License 
 * 
 */
using KintoneDeSql.Data;
using KintoneDeSql.Interface;
using KintoneDeSql.Managers;
using KintoneDeSql.Properties;
using System.Net.Http;
using System.Text.Json;

namespace KintoneDeSql.Requests;

/// <summary>
/// Requestのベースクラス
/// </summary>
/// <typeparam name="REQUEST"></typeparam>
/// <typeparam name="RESPONSE"></typeparam>
internal class BaseRequest<REQUEST, RESPONSE> : BaseSingleton<REQUEST> where REQUEST : class, new() where RESPONSE : class, new()
{
    /// <summary>
    /// API　コマンド
    /// </summary>
    protected virtual string _Command { get; } = string.Empty;
    public virtual void Insert(RESPONSE? response_)
    {
    }

    #region Get
    public async Task<RESPONSE?> Get()
    {
        var query = string.Empty;
        var paramater = string.Empty;
        var response = await KintoneManager.Instance.KintoneGet<RESPONSE?>(HttpMethod.Get, _Command, query, paramater);
        //
        return response;
    }

    public async Task<RESPONSE?> Get(string id_, bool useCybozu_) => (useCybozu_ == false) ? await _getKintone(id_) : await _getCybozu(id_);

    private async Task<RESPONSE?> _getKintone(string id_)
    {
        var query = string.Empty;
        var paramater = JsonSerializer.Serialize(new { id = id_ });
        var response = await KintoneManager.Instance.KintoneGet<RESPONSE?>(HttpMethod.Get, _Command, query, paramater);
        if (response is IResponseId haveId)
        {
            haveId.Id = id_;
        }
        return response;
    }
    private async Task<RESPONSE?> _getCybozu(string code_)
    {
        var query = string.Empty;
        var paramater = JsonSerializer.Serialize(new { code = code_ });
        var response = await KintoneManager.Instance.CybozuGet<RESPONSE?>(HttpMethod.Get, _Command, query, paramater);
        if (response is IResponseCode haveCode)
        {
            haveCode.Code = code_;
        }
        return response;
    }

    public async Task<RESPONSE?> Get(string appId_, string appToken_)
    {
        var query = string.Empty;
        var paramater = JsonSerializer.Serialize(new { app = appId_ });
        var response = await KintoneManager.Instance.KintoneGet<RESPONSE?>(HttpMethod.Get, _Command, query, paramater, appToken_);
        //
        if (response is IAppTableId haveAppId)
        {
            haveAppId.AppId = appId_;
        }
        return response;
    }
    public async Task<RESPONSE?> Get(IEnumerable<string> listApp_)
    {
        var query = string.Empty;
        var paramater = JsonSerializer.Serialize(new { apps = listApp_ });
        return await KintoneManager.Instance.KintoneGet<RESPONSE?>(HttpMethod.Get, _Command, query, paramater);
    }
    public async Task<RESPONSE?> Get(string appId_, IEnumerable<string> listId_, string apiKey_)
    {
        var query = string.Empty;
        var paramater = JsonSerializer.Serialize(new { app = appId_, ids = listId_ });
        var response = await KintoneManager.Instance.KintoneGet<RESPONSE?>(HttpMethod.Get, _Command, query, paramater, apiKey_);
        //
        if (response is IAppTableId haveAppId)
        {
            haveAppId.AppId = appId_;
        }
        return response;
    }

    public async Task<RESPONSE?> Get(int offset_, int limit_, bool useCybozu_) => (useCybozu_ == false) ? await _getKintone(offset_, limit_) : await _getCybozu(offset_, limit_);

    private async Task<RESPONSE?> _getKintone(int offset_, int limit_)
    {
        var query = string.Empty;
        var paramater = JsonSerializer.Serialize(new { offset = offset_, limit = limit_ });
        return await KintoneManager.Instance.KintoneGet<RESPONSE?>(HttpMethod.Get, _Command, query, paramater);
    }
    private async Task<RESPONSE?> _getCybozu(int offset_, int size_)
    {
        var query = string.Empty;
        var paramater = JsonSerializer.Serialize(new { offset = offset_ ,size = size_});
        return await KintoneManager.Instance.CybozuGet<RESPONSE?>(HttpMethod.Get, _Command, query, paramater);
    }

    public async Task<RESPONSE?> Get(string id_, int offset_, int limit_, bool useCybozu_) => (useCybozu_ == false) ? await _getKintone(id_,offset_, limit_) : await _getCybozu(id_,offset_, limit_);
    private async Task<RESPONSE?> _getKintone(string id_, int offset_, int limit_)
    {
        var query = string.Empty;
        var paramater = JsonSerializer.Serialize(new { id = id_, offset = offset_, limit = limit_ });
        var response = await KintoneManager.Instance.KintoneGet<RESPONSE?>(HttpMethod.Get, _Command, query, paramater);
        //
        if (response is IResponseId havePluginId)
        {
            havePluginId.Id = id_;
        }
        return response;
    }

    private async Task<RESPONSE?> _getCybozu(string code_, int offset_, int size_)
    {
        var query = string.Empty;
        var paramater = JsonSerializer.Serialize(new { code = code_, offset = offset_, size = size_ });
        var response = await KintoneManager.Instance.CybozuGet<RESPONSE?>(HttpMethod.Get, _Command, query, paramater);
        if (response is IResponseCode haveCode)
        {
            haveCode.Code = code_;
        }
        return response;
    }


    public async Task<RESPONSE?> Get(string appId_, string appToken_, string lastId_, int limit_, bool useQuery_)
    {
        var query = string.Empty;
        // 方法1：レコードIDを利用する方法
        // https://cybozu.dev/ja/id/7b342fa8f1aa22a1bb9cca4a/#use-id
        // https://cybozu.dev/ja/kintone/tips/best-practices/data-acquisition-operation/kintone-get-records-with-offset-limitation/#use-id
        var paramater =(useQuery_ ==false)
            ? JsonSerializer.Serialize(new { app = appId_, offset = lastId_, limit = limit_})
            : JsonSerializer.Serialize(new { app = appId_, query = $"{Resource.COLUMN_MAIN_TABLE_ID} > {lastId_} order by {Resource.COLUMN_MAIN_TABLE_ID} asc limit {limit_}", totalCount = true });
        //
        var response = await KintoneManager.Instance.KintoneGet<RESPONSE?>(HttpMethod.Get, _Command, query, paramater, appToken_);
        if (response is IAppTableId haveAppId)
        {
            haveAppId.AppId = appId_;
        }
        return response;
    }

    public async Task<RESPONSE?> Get(string appId_, string appToken_, string record_, int offset_, int limit_ )
    {
        var query = string.Empty;
        var paramater = JsonSerializer.Serialize(new { app = appId_, record = record_, offset = offset_, limit = limit_ });

        var response = await KintoneManager.Instance.KintoneGet<RESPONSE?>(HttpMethod.Get, _Command, query, paramater, appToken_);
        if (response is IAppTableId haveAppId)
        {
            haveAppId.AppId = appId_;
        }
        if (response is IAppRecordId haveRecordId)
        {
            haveRecordId.RecordId = record_;
        }

        return response;
    }
    #endregion

    #region Insert
    public async Task<RESPONSE?> Insert()
    {
        var response = await Get();
        Insert(response);   // override
        return response;
    }
    public async Task<RESPONSE?> Insert(string id_, bool useCybozu_)
    {
        var response = await Get(id_, useCybozu_);
        Insert(response);   // override
        return response;
    }

    public async Task<RESPONSE?> Insert(string appId_, string appToken_)
    {
        var response = await Get(appId_, appToken_);
        Insert(response);   // override
        return response;
    }
    public async Task<RESPONSE?> Insert(IEnumerable<string> listApp_)
    {
        var response = await Get(listApp_);
        Insert(response);   // override
        return response;
    }

    public async Task<RESPONSE?> Insert(string appId_, IEnumerable<string> listId_, string apiKey_)
    {
        var response = await Get(appId_, listId_, apiKey_);
        Insert(response);   // override
        return response;
    }

    private async Task<RESPONSE?> _insert(int offset_, int limit_, bool useCybozu_)
    {
        var response = await Get(offset_, limit_, useCybozu_);
        Insert(response);   // override
        return response;
    }

    private async Task<RESPONSE?> _insert(string id_, int offset_, int limit_, bool useCybozu_)
    {
        var response = await Get(id_, offset_, limit_, useCybozu_);
        Insert(response);   // override
        return response;
    }

    public async Task<RESPONSE?> Insert(string appId_, string appToken_, string lastId_, int limit_, bool useQuery_)
    {
        var response = await Get(appId_, appToken_, lastId_, limit_, useQuery_);
        Insert(response);   // override
        return response;
    }

    private async Task<RESPONSE?> _insert(string appId_, string appToken_, string record_, int offset_, int limit_)
    {
        var response = await Get(appId_, appToken_, record_, offset_, limit_);
        Insert(response);   // override
        return response;
    }
    #endregion

    /// <summary>
    /// Insert で繰り返し取得をする場合
    /// </summary>
    /// <param name="limit_"></param>
    /// <param name="useCybozu_"></param>
    /// <returns></returns>
    public async Task<int> InsertAll(int limit_, bool useCybozu_)
    {
        var count = 0;
        var offset = 0;
        //
        do
        {
            var response = await _insert(offset, limit_, useCybozu_);

            if (response is IResponseCount haveCount)
            {
                count = haveCount.Count;
                if (count == 0)
                {
                    break;
                }
                offset += count;
            }
            else
            {
                break;
            }

        } while (count == limit_);
        //
        return offset;
    }

    public async Task<int> InsertAll(string id_, int limit_, bool useCybozu_)
    {
        var count = 0;
        var offset = 0;
        //
        do
        {
            var response = await _insert(id_,offset, limit_, useCybozu_);

            if (response is IResponseCount haveCount)
            {
                count = haveCount.Count;
                if (count == 0)
                {
                    break;
                }
                offset += count;
            }
            else
            {
                break;
            }

        } while (count == limit_);
        //
        return offset;
    }

    public async Task<int> InsertAll(string appId_, string appToken_, string record_, int limit_)
    {
        var count = 0;
        var offset = 0;
        //
        do
        {
            var response = await _insert(appId_, appToken_, record_, offset, limit_);

            if (response is IResponseCount haveCount)
            {
                count = haveCount.Count;
                if (count == 0)
                {
                    break;
                }
                offset += count;
            }
            else
            {
                break;
            }

        } while (count == limit_);
        //
        return offset;
    }
}
