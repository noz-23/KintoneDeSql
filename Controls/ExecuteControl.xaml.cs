/*
 * Kintone De Sql
 * Copyright (c) 2025 noz-23
 *  https://github.com/noz-23/
 * 
 * Licensed under the MIT License 
 * 
 */
using KintoneDeSql.Managers;
using System.Windows;
using System.Windows.Controls;

namespace KintoneDeSql.Controls;

/// <summary>
/// ExecuteControl.xaml の相互作用ロジック
/// </summary>
public partial class ExecuteControl : UserControl
{
    /// <summary>
    /// コンストラクタ
    /// </summary>
    public ExecuteControl()
    {
        InitializeComponent();
    }

    /// <summary>
    /// カラム名のアンダースコア表示対応
    /// </summary>
    /// <param name="sender_"></param>
    /// <param name="e_"></param>
    private void _autoGeneratingColumn(object sender_, DataGridAutoGeneratingColumnEventArgs e_)
    {
        string header = e_.Column.Header.ToString();
        e_.Column.Header = header.Replace("_", "__");
    }

    /// <summary>
    /// クエリの実行
    /// </summary>
    /// <param name="sender_"></param>
    /// <param name="e_"></param>
    private void _excuteClick(object sender_, RoutedEventArgs e_)
    {
        var dataTable =SQLiteManager.Instance.ExecuteSql(_textBox.Text);
        _dataGrid.ItemsSource = null;
        _dataGrid.ItemsSource = dataTable.DefaultView;
    }
}
