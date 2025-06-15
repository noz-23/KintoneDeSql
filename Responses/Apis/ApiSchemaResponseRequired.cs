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
using KintoneDeSql.Responses.Commons;
using System.ComponentModel.DataAnnotations.Schema;

namespace KintoneDeSql.Responses.Apis;

/// <summary>
/// https://cybozu.dev/ja/kintone/docs/rest-api/apis/get-apis/
/// </summary>
[Table($"{SQLiteManager.SUB_DATABASE}.apiSchema_Required")]
internal class ApiSchemaResponseRequired : ApiSchemaResponseProperty
{
    #region ISqlTable
    public static new string TableName(bool withCamma_) => typeof(ApiSchemaResponseRequired).TableName(withCamma_);
    public static new IEnumerable<string> ListCreateHeader(bool withCamma_) => MemberInfoExtension.ListCreateHeader(_listColumn(), withCamma_);
    public static new IEnumerable<string> ListInsertHeader(bool withCamma_) => MemberInfoExtension.ListInsertHeader(_listColumn(), withCamma_);
    //
    private static IEnumerable<ColumnData> _listColumn()
    {
        const int _SORT = 100;
        var rtn = typeof(ApiSchemaResponseBase).ListColumn();
        // key properties_ required の順
        rtn.AddRange(typeof(ApiSchemaValueProperty).ListColumn(string.Empty, _SORT));
        rtn.AddRange(typeof(ApiSchemaValueProperty).ListColumn("properties_", _SORT + _SORT));
        rtn.AddRange(typeof(RequiredValueList).ListColumn(string.Empty, _SORT + _SORT + _SORT));
        return rtn;
    }
    public override IEnumerable<IEnumerable<string>> ListInsertValue(bool withCamma_) => ListInsertValueRequired(withCamma_);
    public IEnumerable<IEnumerable<string>> ListInsertValueRequired(bool withCamma_)
    {
        var rtn = new List<IEnumerable<string>>();
        //
        Request.Key = Resource.JSON_SCHEMA_REQUEST;
        rtn.AddRange(_listInserValue(Request, withCamma_));
        //
        Response.Key = Resource.JSON_SCHEMA_RESPONSE;
        rtn.AddRange(_listInserValue(Response, withCamma_));
        //
        Schemas.Key = Resource.JSON_SCHEMA_SCHEMAS;
        rtn.AddRange(_listInserValue(Schemas, withCamma_));
        //
        return rtn;
    }
    private IEnumerable<IEnumerable<string>> _listInserValue(ApiSchemaValue response_, bool withCamma_)
    {
        var rtn = new List<IEnumerable<string>>();
        //
        foreach (var required in response_.ListRequired.ListValue(withCamma_))
        {
            var list = base.ListValue(withCamma_,typeof(ApiSchemaResponseBase));
            //
            list.AddRange(response_.ListValue(withCamma_,typeof(ApiSchemaValueProperty))); // key
            list.Add((withCamma_ == true) ? $"''" : string.Empty);// properties_ key
            list.Add(required);
            rtn.Add(list);
        }
        //
        foreach (var prop in response_.ListProperty)
        {
            prop.Value.Key = prop.Key;
            //
            foreach (var required in prop.Value.ListRequired.ListValue(withCamma_))
            {
                var list = base.ListValue(withCamma_, typeof(ApiSchemaResponseBase));
                //
                list.AddRange(response_.ListValue(withCamma_, typeof(ApiSchemaValueProperty)));// key
                list.AddRange(prop.Value.ListValue(withCamma_, typeof(ApiSchemaValueProperty)));// properties_ key
                //
                list.Add(required);
                //
                rtn.Add(list);
            }
        }

        return rtn;
    }
    #endregion
}