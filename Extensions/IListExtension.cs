/*
 * Kintone De Sql
 * Copyright (c) 2025 noz-23
 *  https://github.com/noz-23/
 * 
 * Licensed under the MIT License 
 * 
 */
namespace KintoneDeSql.Extensions;

internal static class IListExtension
{
    /// <summary>
    /// 範囲追加
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="src_"></param>
    /// <param name="list_"></param>
    internal static void AddRange<T>(this IList<T> src_, IEnumerable<T> list_) => list_?.ToList().ForEach(item => src_.Add(item));


    /// <summary>
    /// Linq
    /// </summary>
    /// <param name="src_"></param>
    /// <param name="action_"></param>
    internal static void ForEach<T>(this IList<T> src_, Action<T> action_)=> src_.ToList().ForEach(action_);


}
