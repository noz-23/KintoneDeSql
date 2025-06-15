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
using KintoneDeSql.Properties;
using KintoneDeSql.Responses.Commons;
using KintoneDeSql.Responses.Records;
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
    public static IList<string> ListCreateHeader(this Type src_, bool withCamma_) =>ListCreateHeader(src_.ListColumn(), withCamma_);
    public static IList<string> ListCreateHeader(IEnumerable<ColumnData> listColumn_, bool withCamma_)
    {
        var rtn = new List<string>();
        var listUnique = new List<string>();
        var listOrder = listColumn_.OrderBy(s_ => s_.Order);

        foreach (var column in listOrder)
        {
            if (string.IsNullOrEmpty(column.Name) == true)
            {
                continue;
            }

            var primary = column.Column.IsPrimary == true ? "PRIMARY KEY" : "";

            var columnName = (withCamma_ == true) ? $"'{column.Name}'" : $"{column.Name}";
            rtn.Add($"{columnName} {column.Column.TypeName} {primary}");

            if (column.Column.IsUnique == true)
            {
                var uniqueName = (withCamma_ == true) ? $"'{column.Name}'" : $"{column.Name}";

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
    public static IList<string> ListInsertHeader(this Type src_, bool withCamma_)=> ListInsertHeader(src_.ListColumn(), withCamma_);
    public static IList<string> ListInsertHeader(IEnumerable<ColumnData> listColumn_, bool withCamma_)
    {
        var rtn = new List<string>();
        foreach (var column in listColumn_)
        {
            if (string.IsNullOrEmpty(column.Name) == true)
            {
                continue;
            }
            //var columnName = (withCamma_ == true) ? $"'{column.Column.Name}'" : column.Column.Name;
            var columnName = (withCamma_ == true) ? $"'{column.Name}'" : column.Name;

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
    public static IList<ColumnData> ListColumn(this Type src_,string extractName ="", int order_=0)
    {
        var rtn = new List<ColumnData>();
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
                if (_listTsExtract.TryGetValue(prop.PropertyType, out var isExtract) == true)
                {
                    if (isExtract() == true)
                    {
                        rtn.AddRange(prop.PropertyType.ListColumn($"{extractName}{column.Name}_", order_ + column.Order));
                        continue;
                    }
                }
                else
                {
                    rtn.AddRange(prop.PropertyType.ListColumn($"{extractName}{column.Name}_", order_ + column.Order));
                    continue;
                }
            }

            rtn.Add(new(column, $"{extractName}{column.Name}", order_ + column.Order));
        }
        
        return rtn;
    }

    private delegate bool IsExtract();
    private static Dictionary<Type, IsExtract> _listTsExtract= new()
    {
        { typeof(CreatorValue),()=>Settings.Default.IsCreatorExtract},
        { typeof(ModifierValue),()=>Settings.Default.IsModifierExtract},
        { typeof(EntityValue),()=>Settings.Default.IsEntityExtract},
        { typeof(MentionValue),()=>Settings.Default.IsMentionExtract},
    };


    /// <summary>
    /// ColumnEx属性の取得
    /// 処理はこちらを利用
    /// </summary>
    /// <param name="src_"></param>
    /// <returns></returns>
    public static IEnumerable<PropertyInfo>? ListColumnExProperty(this Type src_) => src_.GetProperties(BindingFlags.Instance | BindingFlags.Public)?.Where(s_ => s_.GetAttribute<ColumnExAttribute>() != null).OrderBy(s_ => s_.GetAttribute<ColumnExAttribute>()?.Order);

    /// <summary>
    /// Column属性の取得
    /// 隠したい処理等で利用
    /// </summary>
    /// <param name="src_"></param>
    /// <returns></returns>

    public static IEnumerable<PropertyInfo>? ListColumnProperty(this Type src_) => src_.GetProperties(BindingFlags.Instance | BindingFlags.Public)?.Where(s_ => s_.GetAttribute<ColumnAttribute>() != null).OrderBy(s_ => s_.GetAttribute<ColumnAttribute>()?.Order);
}
