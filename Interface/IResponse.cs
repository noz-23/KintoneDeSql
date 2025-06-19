/*
 * Kintone De Sql
 * Copyright (c) 2025 noz-23
 *  https://github.com/noz-23/
 * 
 * Licensed under the MIT License 
 * 
 */
namespace KintoneDeSql.Interface;

internal interface IResponseId
{
    string Id { get; set; }
}
internal interface IResponseCode
{
    string Code { get; set; }
}

internal interface IResponseCount
{
    int Count { get; }
}

