/*
 * Kintone De Sql
 * Copyright (c) 2025 noz-23
 *  https://github.com/noz-23/
 * 
 * Licensed under the MIT License 
 * 
 */
namespace KintoneDeSql.Interface;

internal interface IAppTableId
{
    string AppId { get; set; }
}
internal interface IAppRecordId
{
    string RecordId { get; set; }
}

internal interface IAppTableKey
{
    string ApiKey { get; set; }
}

internal interface IAppTable: IAppTableId, IAppTableKey
{ 
}