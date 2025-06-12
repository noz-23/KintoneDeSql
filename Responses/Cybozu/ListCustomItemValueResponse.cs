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

namespace KintoneDeSql.Responses.Cybozu;

/// <summary>
/// https://cybozu.dev/ja/common/docs/user-api/overview/data-structure/#custom-item-value
/// </summary>

[Table($"{SQLiteManager.CYBOZU_DATABASE}.customItems")]
internal class ListCustomItemValueResponse : List<CustomItemValueResponse>, ISqlTable
{
    #region ISqlTable
    public static string TableName(bool withCamma_) => typeof(ListCustomItemValueResponse).TableName(withCamma_);

    public static List<string> ListCreateHeader(bool withCamma_) => typeof(CustomItemValueResponse).ListCreateHeader(withCamma_);

    public static List<string> ListInsertHeader(bool withCamma_) => typeof(CustomItemValueResponse).ListInsertHeader(withCamma_);
    public List<List<string>> ListInsertValue(bool withCamma_)
    {
        var rtn = new List<List<string>>();

        this.ForEach(item_ => rtn.Add(item_.ListValue(withCamma_)));

        return rtn;
    }
    #endregion

    public override string ToString()
    {
        return string.Join(",",this.Select(x_=>x_.Value));
    }
}
