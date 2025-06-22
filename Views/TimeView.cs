/*
 * Reprise Report Log Analyzer
 * Copyright (c) 2025 noz-23
 *  https://github.com/noz-23/
 * 
 * Licensed under the MIT License 
 * 
 */
using KintoneDeSql.Attributes;
using KintoneDeSql.Managers;
using System.ComponentModel.DataAnnotations.Schema;

namespace KintoneDeSql.Views;

/// <summary>
/// レコード系の取得時間
/// </summary>
[Table($"{SQLiteManager.APP_DATABASE}.timeView")]
internal class TimeView : BaseView
{
    private TimeView()
    { 
    }

    public TimeView(string id_, string name_, DateTime? time_)
    {
        Id = id_;
        Name = name_;
        Time = time_;
    }

    /// <summary>
    /// App ID or Space ID
    /// </summary>
    [ColumnEx("id", Order = 2, TypeName = "TEXT", IsUnique = true)]
    public string? Id
    {
        get => _id;
        set => _SetValue(ref _id, value);
    }
    private string? _id = null;

    /// <summary>
    /// 取得テーブル名
    /// </summary>
    [ColumnEx("name", Order = 1, TypeName = "TEXT", IsUnique = true)]
    public string? Name
    {
        get => _name;
        set => _SetValue(ref _name, value);
    }
    private string? _name = null;

    /// <summary>
    /// 取得時間
    /// </summary>
    [ColumnEx("time", Order = 3, TypeName = "TEXT")]
    public DateTime? Time
    {
        get => _time;
        set => _SetValue(ref _time, value);
    }
    private DateTime? _time = null;
}
