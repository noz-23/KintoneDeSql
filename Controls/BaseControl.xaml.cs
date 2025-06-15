/*
 * Kintone De Sql
 * Copyright (c) 2025 noz-23
 *  https://github.com/noz-23/
 * 
 * Licensed under the MIT License 
 * 
 */
using KintoneDeSql.Managers;
using System.ComponentModel;
using System.Data;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace KintoneDeSql.Controls;

/// <summary>
/// BaseControl.xaml の相互作用ロジック
/// </summary>
public partial class BaseControl : UserControl, INotifyPropertyChanged
{
    public BaseControl()
    {
        InitializeComponent();
    }

 
    public event PropertyChangedEventHandler? PropertyChanged;
    private void _notifyPropertyChanged([CallerMemberName] string propertyName_ = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName_));
    }

    public string ControlTableName{get;set;} = string.Empty;

    public string ControlWhere { get; set; } = string.Empty;


    private void _loaded(object sender_, RoutedEventArgs e_)
    {
        Load();
    }
    /// <summary>
    /// カラム名のアンダースコア表示対応
    /// </summary>
    /// <param name="sender_"></param>
    /// <param name="e_"></param>
    //private void _autoGeneratingColumn(object sender_, DataGridAutoGeneratingColumnEventArgs e_)
    //{
    //    string header = e_.Column.Header?.ToString() ?? string.Empty;
    //    e_.Column.Header = header.Replace("_", "__");
    //}

    public void Load()
    {
        if (string.IsNullOrEmpty(ControlTableName) == true)
        {
            return;
        }
        _dataGrid.ItemsSource = null;
        _dataGrid.ItemsSource = SQLiteManager.Instance.SelectTable(ControlTableName, ControlWhere).DefaultView;
    }

    private void _gotFocus(object sender, RoutedEventArgs e)
    {
        Load();
    }

    public int Count
    {
        get
        {
            if (_dataGrid.ItemsSource is DataView view)
            {
                return view.Count;
            }
            return 0;
        }
    }
}
