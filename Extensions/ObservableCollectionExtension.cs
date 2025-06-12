/*
 * Kintone De Sql
 * Copyright (c) 2025 noz-23
 *  https://github.com/noz-23/
 * 
 * Licensed under the MIT License 
 * 
 */
using System.Collections.ObjectModel;

namespace KintoneDeSql.Extensions;

internal static class ObservableCollectionExtension
{
    /// <summary>
    /// 範囲追加
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="src_"></param>
    /// <param name="collection_"></param>
    internal static void AddRange<T>(this ObservableCollection<T> src_, ICollection<T> collection_)=> collection_?.ToList().ForEach(item => src_.Add(item));
}
