/*
 * Kintone De Sql
 * Copyright (c) 2025 noz-23
 *  https://github.com/noz-23/
 * 
 * Licensed under the MIT License 
 * 
 */
namespace KintoneDeSql.Interface;

internal interface IAppRequest
{
    string _COMMAND{get;}

    Task<T?> Get<T>(string appId_);
    Task<T?> Insert<T>(string appId_);
}
