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
using KintoneDeSql.Properties;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace KintoneDeSql.Responses.Apis;

/// <summary>
/// https://cybozu.dev/ja/kintone/docs/rest-api/apis/get-apis/
/// </summary>
[Table($"{SQLiteManager.SUB_DATABASE}.apiSchema_Properties")]
internal class ApiSchemaResponseProperty : ApiSchemaResponseBase, ISqlTable
{
    //baseUrl	文字列	APIを実行するときの基本となるURL
    [JsonPropertyName("baseUrl")]
    [ColumnEx("baseUrl", Order = 10, TypeName = "TEXT")]
    public string BaseUrl { get; set; } = string.Empty;

    //path	文字列	APIのパス
    [JsonPropertyName("path")]
    [ColumnEx("path", Order = 11, TypeName = "TEXT")]
    public string Path { get; set; } = string.Empty;

    //request	オブジェクト	APIリクエストのスキーマ情報
    [JsonPropertyName("request")]
    public ApiSchemaValue Request { get; set; } = new();

    //response	オブジェクト	APIレスポンスのスキーマ情報
    [JsonPropertyName("response")]
    public ApiSchemaValue Response { get; set; } = new();

    //schemas	オブジェクト	APIで共通で使用するスキーマ情報一覧
    [JsonPropertyName("schemas")]
    public ApiSchemaValue Schemas { get; set; } = new();

    #region ISqlTable
    public static string TableName(bool withCamma_) => typeof(ApiSchemaResponseProperty).TableName(withCamma_);
    public static IEnumerable<string> ListCreateHeader(bool withCamma_) => MemberInfoExtension.ListCreateHeader(_listColumn(), withCamma_);
    public static IEnumerable<string> ListInsertHeader(bool withCamma_) => MemberInfoExtension.ListInsertHeader(_listColumn(), withCamma_);
    private static IEnumerable<ColumnData> _listColumn()
    {
        const int _SORT = 100;
        var rtn = typeof(ApiSchemaResponseBase).ListColumn();
        rtn.AddRange(typeof(ApiSchemaValueProperty).ListColumn(string.Empty, _SORT));
        rtn.AddRange(typeof(ApiSchemaValue).ListColumn("properties_", _SORT+ _SORT));
        return rtn;
    }
    public virtual IEnumerable<IEnumerable<string>> ListInsertValue(bool withCamma_) => ListInsertValueProperty(withCamma_);
    public IEnumerable<IEnumerable<string>> ListInsertValueProperty(bool withCamma_)
    {
        var rtn = new List<IEnumerable<string>>();
        //
        Request.Key = Resource.JSON_SCHEMA_REQUEST;
        rtn.AddRange(_listInsertValue(Request, withCamma_));
        //
        Response.Key = Resource.JSON_SCHEMA_RESPONSE;
        rtn.AddRange(_listInsertValue(Response, withCamma_));
        //
        Schemas.Key = Resource.JSON_SCHEMA_SCHEMAS;
        rtn.AddRange(_listInsertValue(Schemas, withCamma_));
        //
        return rtn;
    }
    private IEnumerable<IEnumerable<string>> _listInsertValue(ApiSchemaValue response_, bool withCamma_)
    {
        var rtn = new List<IEnumerable<string>>();
        foreach (var prop in response_.ListProperty)
        {
            prop.Value.Key = prop.Key;
            //
            var list = base.ListValue(withCamma_,typeof(ApiSchemaResponseBase));
            list.AddRange(response_.ListValue(withCamma_, typeof(ApiSchemaValueProperty)));
            list.AddRange(prop.Value.ListValue(withCamma_, typeof(ApiSchemaValue)));
            //
            rtn.Add(list);
        }
        return rtn;
    }
    #endregion
}
