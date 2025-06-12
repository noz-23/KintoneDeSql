/*
 * Kintone De Sql
 * Copyright (c) 2025 noz-23
 *  https://github.com/noz-23/
 * 
 * Licensed under the MIT License 
 * 
 */
namespace KintoneDeSql.Data;

/// <summary>
/// シングルトンクラスの基底
/// </summary>
/// <typeparam name="T"></typeparam>
internal abstract class BaseSingleton<T> where T : class, new()
{
    public static T Instance = new T();
}
