/*
 * Kintone De Sql
 * Copyright (c) 2025 noz-23
 *  https://github.com/noz-23/
 * 
 * Licensed under the MIT License 
 * 
 */
namespace KintoneDeSql.Interface;

/// <summary>
/// テーブル名
/// </summary>
internal interface ITableName
{
    static string TableName(bool withCamma_) =>string.Empty;
}

/// <summary>
/// テーブル作成
/// </summary>
internal interface ICreateTable: ITableName
{    
    static IEnumerable<string> ListCreateHeader(bool withCamma_) => new List<string>();
}

/// <summary>
/// テーブル挿入
/// </summary>
internal interface IInsertTable: ITableName
{
    static IEnumerable<string> ListInsertHeader(bool withCamma_) => new List<string>();
    IEnumerable<IEnumerable<string>> ListInsertValue(bool withCamma_);
}

internal interface ISqlTable : ICreateTable, IInsertTable
{
}