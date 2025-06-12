using KintoneDeSql.Attributes;
using KintoneDeSql.Data;
using KintoneDeSql.Extensions;
using KintoneDeSql.Interface;
/*
 * Reprise Report Log Analyzer
 * Copyright (c) 2025 noz-23
 *  https://github.com/noz-23/
 * 
 * Licensed under the MIT License 
 * 
 */
using KintoneDeSql.Managers;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace KintoneDeSql.Responses.Apis;

/// <summary>
/// https://cybozu.dev/ja/kintone/docs/rest-api/apis/get-apis/
/// </summary>
/// 
[Table($"{SQLiteManager.SUB_DATABASE}.apis")]
internal class ListApiResponse : BaseToData,ISqlTable
{
    [JsonPropertyName("baseUrl")]
    [ColumnEx("baseUrl", Order = 1, TypeName = "TEXT")]
    public string BaseUrl { get; set; } = string.Empty;  // 実利用はなし
    

    // apis	オブジェクト	各APIの情報一覧
    // apis.APIのID	オブジェクト	APIの情報
    [JsonPropertyName("apis")]
    public Dictionary<string, ApiResponse> ListApi { get; set; } = new ();

    /// <summary>
    /// apis のKey
    /// </summary>
    [ColumnEx("api_key", Order = 10, TypeName = "TEXT", IsPrimary = true)]
    public string ApiKey { get; set; } = string.Empty;  // 実利用はなし

    #region ISqlTable
    public static string TableName(bool withCamma_) => typeof(ListApiResponse).TableName(withCamma_);
    public static List<string> ListCreateHeader(bool withCamma_)
    {
        var list = typeof(ListApiResponse).ListColumn();
        list.AddRange(typeof(ApiResponse).ListColumn());
        return MemberInfoExtension.ListCreateHeader(list, withCamma_);
    }
    public static List<string> ListInsertHeader(bool withCamma_)
    {
        var list = typeof(ListApiResponse).ListColumn();
        list.AddRange(typeof(ApiResponse).ListColumn());
        return MemberInfoExtension.ListInsertHeader(list, withCamma_);
    }
    public List<List<string>> ListInsertValue(bool withCamma_)
    {
        var rtn = new List<List<string>>();

        foreach (var api in ListApi)
        {
            ApiKey = api.Key;
            var list =this.ListValue(withCamma_);
            //var list = new List<string>();

            //var baseUrl = (withCamma_ == true) ? $"'{BaseUrl}'" : BaseUrl;
            //list.Add(baseUrl); 
            ////
            //var apiKey = (withCamma_ ==true) ?$"'{api.Key}'":api.Key;
            //list.Add(apiKey);
            //
            list.AddRange(api.Value.ListValue(withCamma_));

            rtn.Add(list);
        }

        return rtn;
    }
    #endregion

}
