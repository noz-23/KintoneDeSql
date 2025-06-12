/*
 * Kintone De Sql
 * Copyright (c) 2025 noz-23
 *  https://github.com/noz-23/
 * 
 * Licensed under the MIT License 
 * 
 */
using KintoneDeSql.Attributes;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace KintoneDeSql.Extensions;

/// <summary>
/// Type 拡張
/// </summary>
internal static class MemberInfoExtension
{
    /// <summary>
    /// リフレクション系の属性取得
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="src_"></param>
    /// <returns></returns>
    public static T? GetAttribute<T>(this MemberInfo src_) where T : Attribute => (Attribute.GetCustomAttribute(src_, typeof(T)) as T);

    /// <summary>
    /// TableAttribute 定義の名前取得
    /// </summary>
    /// <param name="src_"></param>
    /// <returns></returns>
    public static string TableName(this MemberInfo src_, bool withCamma_)
    {
        var tableName = src_.GetAttribute<TableAttribute>()?.Name ?? src_.Name;
        return (withCamma_ == true) ? $"'{tableName}'" : $"{tableName}";
    }

    /// <summary>
    /// ColumnExAttribute でのCreate Table カラム名一覧
    /// </summary>
    /// <param name="src_"></param>
    /// <param name="withCamma_"></param>
    /// <returns></returns>
    public static List<string> ListCreateHeader(this Type src_, bool withCamma_) =>ListCreateHeader(src_.ListColumn(), withCamma_);

    public static List<string> ListCreateHeader(List<KeyValuePair<ColumnExAttribute, string>> listColumn_, bool withCamma_)
    {
        var rtn = new List<string>();
        var listUnique = new List<string>();
        ///
        foreach (var column in listColumn_)
        {
            var primary = column.Key.IsPrimary == true ? "PRIMARY KEY" : "";

            var columnName = (withCamma_ == true) ? $"'{column.Value}'" : $"{column.Value}";
            rtn.Add($"{columnName} {column.Key.TypeName} {primary}");

            if (column.Key.IsUnique == true)
            {
                var uniqueName = (withCamma_ == true) ? $"'{column.Value}'" : $"{column.Value}";

                listUnique.Add(uniqueName);
            }
        }

        if (listUnique.Any() == true)
        {
            rtn.Add($"UNIQUE({string.Join(",", listUnique)})");
        }

        return rtn;
    }


    /// <summary>
    /// ColumnExAttribute でのカラム名一覧
    /// </summary>
    /// <param name="src_"></param>
    /// <param name="withCamma_"></param>
    /// <returns></returns>
    public static List<string> ListInsertHeader(this Type src_, bool withCamma_)=> ListInsertHeader(src_.ListColumn(), withCamma_);

    public static List<string> ListInsertHeader(List<KeyValuePair<ColumnExAttribute, string>> listColumn_, bool withCamma_)
    {
        var rtn = new List<string>();
        foreach (var column in listColumn_)
        {
            var columnName = (withCamma_ == true) ? $"'{column.Value}'" : column.Value;

            rtn.Add(columnName);
        }

        return rtn;
    }

    /// <summary>
    /// ColumnExAttribute の抽出(展開あり)
    /// </summary>
    /// <param name="src_"></param>
    /// <param name="extractName"></param>
    /// <returns></returns>
    public static List<KeyValuePair<ColumnExAttribute,string>> ListColumn(this Type src_,string extractName ="")
    {
        var rtn = new List<KeyValuePair<ColumnExAttribute, string>>();
        var listPropetyInfo = src_.ListColumnExProperty();

        if ((listPropetyInfo?.Any() ?? false) == false)
        {
            return rtn;
        }
        foreach (var prop in listPropetyInfo)
        {
            var column = prop.GetAttribute<ColumnExAttribute>();
            if (column == null)
            {
                continue;
            }
            if (column.IsExtract == true)
            {
                rtn.AddRange(prop.PropertyType.ListColumn($"{extractName}{column.Name}_"));
                continue;
            }

            rtn.Add(new (column, $"{extractName}{column.Name}"));
        }
        return rtn;
    }

    /// <summary>
    /// ColumnEx属性の取得
    /// 処理はこちらを利用
    /// </summary>
    /// <param name="src_"></param>
    /// <returns></returns>
    public static IEnumerable<PropertyInfo> ListColumnExProperty(this Type src_) => src_.GetProperties(BindingFlags.Instance | BindingFlags.Public)?.Where(s_ => s_.GetAttribute<ColumnExAttribute>() != null).OrderBy(s_ => s_.GetAttribute<ColumnExAttribute>()?.Order);

    /// <summary>
    /// Column属性の取得
    /// 隠したい処理等で利用
    /// </summary>
    /// <param name="src_"></param>
    /// <returns></returns>

    public static IEnumerable<PropertyInfo> ListColumnProperty(this Type src_) => src_.GetProperties(BindingFlags.Instance | BindingFlags.Public)?.Where(s_ => s_.GetAttribute<ColumnAttribute>() != null).OrderBy(s_ => s_.GetAttribute<ColumnAttribute>()?.Order);
}
