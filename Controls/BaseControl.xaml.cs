/*
 * Kintone De Sql
 * Copyright (c) 2025 noz-23
 *  https://github.com/noz-23/
 * 
 * Licensed under the MIT License 
 * 
 */
using KintoneDeSql.Managers;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace KintoneDeSql.Controls;

/// <summary>
/// BaseControl.xaml の相互作用ロジック
/// </summary>
public partial class BaseControl : UserControl
{
    public BaseControl()
    {
        InitializeComponent();
    }

    /// <summary>
    /// GridViewに表示するテーブル
    /// </summary>
    public string ControlTableName{get;set;} = string.Empty;

    /// <summary>
    /// 表示条件
    /// </summary>
    public string ControlWhere { get; set; } = string.Empty;

    /// <summary>
    /// 読み込み処理
    /// </summary>
    /// <param name="sender_"></param>
    /// <param name="e_"></param>
    private void _loaded(object sender_, RoutedEventArgs e_)
    {
        Load();
    }

    /// <summary>
    /// 実際の表示
    /// </summary>
    public void Load()
    {
        if (string.IsNullOrEmpty(ControlTableName) == true)
        {
            return;
        }

        _dataGrid.ItemsSource = null;
        _dataGrid.ItemsSource = SQLiteManager.Instance.SelectTable(ControlTableName, ControlWhere).DefaultView;
    }

    /// <summary>
    /// フォーカスが移った場合
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void _gotFocus(object sender_, RoutedEventArgs e_)
    {
        Load();
    }

    /// <summary>
    /// 表示しているGridViewの数
    /// </summary>
    public int Count
    {
        get => GridDataView?.Count ?? 0;
    }


    /// <summary>
    /// 表示しているGridView
    /// </summary>
    public DataView? GridDataView
    {
        get
        {
            if (_dataGrid.ItemsSource is DataView view)
            {
                return view;
            }
            return null;
        }
    }

}
