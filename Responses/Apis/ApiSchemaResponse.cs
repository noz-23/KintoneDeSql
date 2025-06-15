/*
 * Kintone De Sql
 * Copyright (c) 2025 noz-23
 *  https://github.com/noz-23/
 * 
 * Licensed under the MIT License 
 * 
 */
using KintoneDeSql.Data;
using KintoneDeSql.Extensions;
using KintoneDeSql.Managers;
using KintoneDeSql.Properties;
using System.ComponentModel.DataAnnotations.Schema;

namespace KintoneDeSql.Responses.Apis;

/// <summary>
/// https://cybozu.dev/ja/kintone/docs/rest-api/apis/get-apis/
/// </summary>
[Table($"{SQLiteManager.SUB_DATABASE}.apiSchema")]
internal class ApiSchemaResponse: ApiSchemaResponseRequired
{
    #region ISqlTable
    public static new string TableName(bool withCamma_) => typeof(ApiSchemaResponse).TableName(withCamma_);
    public static new IEnumerable<string> ListCreateHeader(bool withCamma_) => MemberInfoExtension.ListCreateHeader(_listColumn(), withCamma_);
    public static new IEnumerable<string> ListInsertHeader(bool withCamma_) => MemberInfoExtension.ListInsertHeader(_listColumn(), withCamma_);
    //
    private static IEnumerable<ColumnData> _listColumn()
    {
        const int _SORT = 100;
        var rtn = typeof(ApiSchemaResponse).ListColumn();
        rtn.AddRange(typeof(ApiSchemaValue).ListColumn(string.Empty, _SORT));
        rtn.AddRange(typeof(ApiSchemaValue).ListColumn("properties_", _SORT + _SORT));
        return rtn;
    }
    public override IEnumerable<IEnumerable<string>> ListInsertValue(bool withCamma_)
    {
        var rtn = new List<IEnumerable<string>>();
        //
        Request.Key = Resource.JSON_SCHEMA_REQUEST;
        rtn.AddRange(_listValue(Request, withCamma_));
        //
        Response.Key = Resource.JSON_SCHEMA_RESPONSE;
        rtn.AddRange(_listValue(Response, withCamma_));
        //
        Schemas.Key = Resource.JSON_SCHEMA_SCHEMAS;
        rtn.AddRange(_listValue(Schemas, withCamma_));
        //
        return rtn;
    }
    private IEnumerable<IEnumerable<string>> _listValue(ApiSchemaValue response_, bool withCamma_)
    {
        var rtn = new List<IEnumerable<string>>();
        foreach (var prop in response_.ListProperty)
        {
            prop.Value.Key = prop.Key;
            //
            var list = base.ListValue(withCamma_);
            list.AddRange(response_.ListValue(withCamma_));
            list.AddRange(prop.Value.ListValue(withCamma_));
            //
            rtn.Add(list);
        }
        return rtn;
    }
    #endregion
}
