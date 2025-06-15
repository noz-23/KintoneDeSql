/*
 * Kintone De Sql
 * Copyright (c) 2025 noz-23
 *  https://github.com/noz-23/
 * 
 * Licensed under the MIT License 
 * 
 */
using KintoneDeSql.Attributes;

namespace KintoneDeSql.Data;

internal class ColumnData
{
    public ColumnData(ColumnExAttribute column_, string name_, int order_)
    {
        Column = column_;
        Name = name_;
        Order = order_;
    }

    public readonly ColumnExAttribute Column;
    public readonly string Name;
    public readonly int Order;
}
