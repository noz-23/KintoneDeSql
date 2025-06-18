/*
 * Kintone De Sql
 * Copyright (c) 2025 noz-23
 *  https://github.com/noz-23/
 * 
 * Licensed under the MIT License 
 * 
 */
using KintoneDeSql.Attributes;
using KintoneDeSql.Interface;
using KintoneDeSql.Managers;
using System.ComponentModel.DataAnnotations.Schema;

namespace KintoneDeSql.Views;

/// <summary>
/// サブテーブル管理
/// </summary>

[Table($"{SQLiteManager.APP_DATABASE}.subTableView")]
internal class SubTableView : BaseView,IAppTableId
{
    [ColumnEx("appId", Order = 10, TypeName = "TEXT", IsPrimary = true)]
    public string AppId
    {
        get => _appId;
        set => _SetValue(ref _appId, value);
    }
    private string _appId = string.Empty;

    [ColumnEx("name", Order = 11, TypeName = "TEXT")]
    public string Name
    {
        get => _name;
        set => _SetValue(ref _name, value);
    }
    private string _name = string.Empty;

    [ColumnEx("tableName", Order = 20, TypeName = "TEXT")]
    public string TableName
    {
        get => _tableName;
        set => _SetValue(ref _tableName, value);
    }
    private string _tableName = string.Empty;
}
