/*
 * Reprise Report Log Analyzer
 * Copyright (c) 2025 noz-23
 *  https://github.com/noz-23/
 * 
 * Licensed under the MIT License 
 * 
 */
using KintoneDeSql.Attributes;

namespace KintoneDeSql.Views;

/// <summary>
/// PRAGMA table_info(TableName)に対する応答
/// </summary>
internal class SqlTableView
{
    [ColumnEx("cid", Order = 11, TypeName = "TEXT")]
    public string CId { get; set; } = string.Empty;

    [ColumnEx("name", Order = 11, TypeName = "TEXT")]
    public string Name { get; set; } = string.Empty;

    [ColumnEx("type", Order = 11, TypeName = "TEXT")]
    public string Type { get; set; } = string.Empty;

    [ColumnEx("notnull", Order = 11, TypeName = "TEXT")]
    public string NotNull { get; set; } =string.Empty;

    [ColumnEx("dflt_value", Order = 11, TypeName = "TEXT")]
    public string DefaultValue { get; set; } = string.Empty;

    [ColumnEx("pk", Order = 11, TypeName = "TEXT")]
    public string PrimaryKey { get; set; } = string.Empty;
}
