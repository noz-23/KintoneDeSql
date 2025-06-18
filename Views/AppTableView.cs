/*
 * Reprise Report Log Analyzer
 * Copyright (c) 2025 noz-23
 *  https://github.com/noz-23/
 * 
 * Licensed under the MIT License 
 * 
 */
using KintoneDeSql.Attributes;
using KintoneDeSql.Interface;
using KintoneDeSql.Managers;
using KintoneDeSql.Responses.Apps;
using System.ComponentModel.DataAnnotations.Schema;

namespace KintoneDeSql.Views;

/// <summary>
/// アプリテーブル管理
/// </summary>

[Table($"{SQLiteManager.APP_DATABASE}.appTableView")]
internal class AppTableView: BaseView, IAppTable
{
    /// <summary>
    /// コンストラクタ
    /// </summary>
    public AppTableView()
    {
    }
    public AppTableView(AppGetValue app_) :this()
    {
        AppId = app_?.AppId ?? string.Empty;
        Name = app_?.Name ?? string.Empty;
        ApiKey = string.Empty;

        Count = 0;
        RecordDateTime = null;
    }
    //

    [ColumnEx("appId", Order = 10, TypeName = "TEXT", IsPrimary =true)]
    public string AppId
    { 
        get=> _appId;
        set=>_SetValue(ref _appId,value); 
    }
    private string _appId = string.Empty;

    [ColumnEx("name", Order = 11, TypeName = "TEXT")]
    public string Name
    {
        get => _name;
        set => _SetValue(ref _name, value);
    }
    private string _name = string.Empty;


    [ColumnEx("revision", Order = 12, TypeName = "TEXT")]
    public string Revision
    { 
        get => _revision;
        set => _SetValue(ref _revision, value);
    }
    public string _revision = string.Empty;

    [ColumnEx("tableName", Order = 20, TypeName = "TEXT")]
    public string TableName
    {
        get => _tableName;
        set => _SetValue(ref _tableName, value);
    }
    private string _tableName = string.Empty;

    [ColumnEx("apiKey", Order = 21, TypeName = "TEXT")]
    public string ApiKey
    {
        get => _apiKey;
        set => _SetValue(ref _apiKey, value);
    }
    private string _apiKey = string.Empty;

    [ColumnEx("count", Order = 22, TypeName = "TEXT")]
    public int Count
    {
        get => _count;
        set => _SetValue(ref _count, value);
    }
    private int _count = 0;

    [ColumnEx("recordDateTime", Order = 30, TypeName = "TEXT")]
    public DateTime? RecordDateTime
    {
        get => _recordDateTime;
        set => _SetValue(ref _recordDateTime, value);
    }
    private DateTime? _recordDateTime = null;
}
