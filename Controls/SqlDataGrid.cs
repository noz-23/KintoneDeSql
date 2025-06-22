
/*
 * Kintone De Sql
 * Copyright (c) 2025 noz-23
 *  https://github.com/noz-23/
 * 
 * Licensed under the MIT License 
 * 
 */
using KintoneDeSql.Data;
using System.Windows.Controls;

namespace KintoneDeSql.Controls;

public class SqlDataGrid: DataGrid
{
    /// <summary>
    /// コンストラクタ
    /// </summary>
    public SqlDataGrid() : base() 
    {
        CanUserAddRows = false;
        IsReadOnly = true;
        SelectionMode= DataGridSelectionMode.Single;
        // 行の仮想化有効
        EnableColumnVirtualization = true;
        EnableRowVirtualization = true;
        SetValue(VirtualizingPanel.IsVirtualizingProperty, true);
        SetValue(VirtualizingPanel.VirtualizationModeProperty, VirtualizationMode.Recycling);
        //
        AutoGeneratingColumn += _autoGeneratingColumn;
    }

    /// <summary>
    /// カラム名のアンダースコア表示対応
    /// </summary>
    /// <param name="sender_"></param>
    /// <param name="e_"></param>
    private void _autoGeneratingColumn(object? sender_, DataGridAutoGeneratingColumnEventArgs e_)
    {
        string header = e_.Column.Header?.ToString() ?? string.Empty;
        // e_.Column.Header = header.Replace("_", "__"); // ←これの替わり
        e_.Column.Header = new HeaderColumnData(header);
    }
}
