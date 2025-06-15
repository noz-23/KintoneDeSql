/*
 * Reprise Report Log Analyzer
 * Copyright (c) 2025 noz-23
 *  https://github.com/noz-23/
 * 
 * Licensed under the MIT License 
 * 
 */
using KintoneDeSql.Attributes;
using KintoneDeSql.Data;
using KintoneDeSql.Extensions;
using KintoneDeSql.Interface;
using KintoneDeSql.Managers;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace KintoneDeSql.Responses.Apis;

/// <summary>
/// https://cybozu.dev/ja/kintone/docs/rest-api/apis/get-apis/
/// </summary>
/// 
[Table($"{SQLiteManager.SUB_DATABASE}.apis")]
internal class ApiGetResponse : BaseToData,ISqlTable
{
    [JsonPropertyName("baseUrl")]
    [ColumnEx("baseUrl", Order = 10, TypeName = "TEXT")]
    public string BaseUrl { get; set; } = string.Empty;  // 実利用はなし
    
    // apis	オブジェクト	各APIの情報一覧
    // apis.APIのID	オブジェクト	APIの情報
    [JsonPropertyName("apis")]
    public Dictionary<string, ApiGetValue> ListApi { get; set; } = new ();

    #region ISqlTable
    public static string TableName(bool withCamma_) => typeof(ApiGetResponse).TableName(withCamma_);
    public static IEnumerable<string> ListCreateHeader(bool withCamma_) => MemberInfoExtension.ListCreateHeader(_listColumn(), withCamma_);
    public static IEnumerable<string> ListInsertHeader(bool withCamma_) => MemberInfoExtension.ListInsertHeader(_listColumn(), withCamma_);
    private static IEnumerable<ColumnData> _listColumn()
    {
        const int _SORT = 100;
        var rtn = typeof(ApiGetResponse).ListColumn();
        rtn.AddRange(typeof(ApiGetValue).ListColumn(string.Empty, _SORT));
        return rtn;
    }
    public IEnumerable<IEnumerable<string>> ListInsertValue(bool withCamma_)
    {
        var rtn = new List<IEnumerable<string>>();

        foreach (var api in ListApi)
        {
            api.Value.ApiKey = api.Key;
            //
            var list =this.ListValue(withCamma_);
            list.AddRange(api.Value.ListValue(withCamma_));

            rtn.Add(list);
        }

        return rtn;
    }
    #endregion

}
