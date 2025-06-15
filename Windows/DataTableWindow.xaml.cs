/*
 * Kintone De Sql
 * Copyright (c) 2025 noz-23
 *  https://github.com/noz-23/
 * 
 * Licensed under the MIT License 
 * 
 */
using System.Data;
using System.Windows;

namespace KintoneDeSql.Windows;

/// <summary>
/// DataTableWindow.xaml の相互作用ロジック
/// </summary>
public partial class DataTableWindow : Window
{
    /// <summary>
    /// コンストラクタ
    /// </summary>
    internal DataTableWindow()
    {
        InitializeComponent();
    }
    public DataTableWindow(DataTable dataTable_) : this()
    {
        _dataTable = dataTable_;
        _dataGrid.ItemsSource = null;
        _dataGrid.ItemsSource = _dataTable.DefaultView;
    }

    private DataTable? _dataTable=null;
}
