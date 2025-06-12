/*
 * Kintone De Sql
 * Copyright (c) 2025 noz-23
 *  https://github.com/noz-23/
 * 
 * Licensed under the MIT License 
 * 
 */
using System.ComponentModel.DataAnnotations.Schema;

namespace KintoneDeSql.Attributes;

/// <summary>
/// ColumnAttributeの拡張
/// </summary>
internal class ColumnExAttribute : ColumnAttribute
{
    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="name"></param>
    public ColumnExAttribute() : base()
    {
    }

    public ColumnExAttribute(string name) : base(name)
    {
    }

    /// <summary>
    /// PrimaryKey か
    /// </summary>
    public bool IsPrimary{get;set;} =false;
    /// <summary>
    /// Unique か
    /// </summary>
    public bool IsUnique { get; set; } = false;
    /// <summary>
    /// クラスを展開するか
    /// </summary>
    public bool IsExtract { get; set; } = false;
}
