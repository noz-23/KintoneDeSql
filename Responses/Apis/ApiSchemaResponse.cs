/*
 * Kintone De Sql
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
[Table($"{SQLiteManager.SUB_DATABASE}.apiSchema")]
internal class ApiSchemaResponse: BaseToData,ISqlTable
{
    //id	文字列	kintone REST APIのAPIのID
    [JsonPropertyName("id")]
    [ColumnEx("id", Order = 1, TypeName = "TEXT")]
    public string Id { get; set; } = string.Empty;

    //baseUrl	文字列	APIを実行するときの基本となるURL
    [JsonPropertyName("baseUrl")]
    [ColumnEx("baseUrl", Order = 2, TypeName = "TEXT")]
    public string BaseUrl { get; set; } = string.Empty;

    //path	文字列	APIのパス
    [JsonPropertyName("path")]
    [ColumnEx("path", Order = 2, TypeName = "TEXT")]
    public string Path { get; set; } = string.Empty;

    //httpMethod	文字列	APIを実行するためのHTTPメソッド
    [JsonPropertyName("httpMethod")]
    [ColumnEx("httpMethod", Order = 3, TypeName = "TEXT")]
    public string HttpMethod { get; set; } = string.Empty;

    //request	オブジェクト	APIリクエストのスキーマ情報
    [JsonPropertyName("request")]
    public ApiJsonResponse Request { get; set; } = new ();

    //response	オブジェクト	APIレスポンスのスキーマ情報
    [JsonPropertyName("response")]
    public ApiJsonResponse Response { get; set; } =new ();

    //schemas	オブジェクト	APIで共通で使用するスキーマ情報一覧
    [JsonPropertyName("schemas")]
    public ApiJsonResponse Schemas { get; set; } = new ();

    /// <summary>
    /// request/response/schemas
    /// </summary>
    [ColumnEx("jsonSchema", Order = 100, TypeName = "TEXT")]
    public string JsonSchema { get; set; } = string.Empty;

    #region ISqlTable
    public static string TableName(bool withCamma_) => typeof(ApiSchemaResponse).TableName(withCamma_);
    public static List<string> ListCreateHeader(bool withCamma_)
    {
        var list = typeof(ApiSchemaResponse).ListColumn();
        list.AddRange(typeof(ApiJsonResponse).ListColumn());
        list.AddRange(typeof(ApiJsonResponse).ListColumn("properties_"));
        return MemberInfoExtension.ListCreateHeader(list, withCamma_);
    }
    public static List<string> ListInsertHeader(bool withCamma_)
    {
        var list = typeof(ApiSchemaResponse).ListColumn();
        list.AddRange(typeof(ApiJsonResponse).ListColumn());
        list.AddRange(typeof(ApiJsonResponse).ListColumn("properties_"));
        return MemberInfoExtension.ListInsertHeader(list, withCamma_);
    }
    public List<List<string>> ListInsertValue(bool withCamma_)
    {
        var rtn = new List<List<string>>();
        //
        JsonSchema = "request";
        rtn.AddRange(_listValue(Request,withCamma_));
        //
        JsonSchema = "response";
        rtn.AddRange(_listValue(Response, withCamma_));
        //
        JsonSchema = "schemas";
        rtn.AddRange(_listValue(Schemas, withCamma_));
        //
        return rtn;
    }

    private List<List<string>> _listValue(ApiJsonResponse response_, bool withCamma_)
    {
        var rtn = new List<List<string>>();
        //
        var listBase = base.ListValue(withCamma_);
        listBase.AddRange(response_.ListValue(withCamma_));
        //
        foreach (var prop in response_.ListProperty)
        {
            var listProp = new List<string>(listBase);
            prop.Value.PropertiesKey = prop.Key;
            listProp.AddRange(prop.Value.ListValue(withCamma_));

            rtn.Add(listProp);
        }

        return rtn;
    }
    #endregion
}
