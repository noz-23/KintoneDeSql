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
public partial class BaseControl : UserControl//, INotifyPropertyChanged
{
    public BaseControl()
    {
        InitializeComponent();
    }

    //#region INotifyPropertyChanged
    //public event PropertyChangedEventHandler? PropertyChanged;
    //private void _notifyPropertyChanged([CallerMemberName] string propertyName_ = "")
    //{
    //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName_));
    //}
    //#endregion

    public string ControlTableName{get;set;} = string.Empty;
    public string ControlWhere { get; set; } = string.Empty;

    private void _loaded(object sender_, RoutedEventArgs e_)
    {
        Load();
    }
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
        //get => (_dataGrid.ItemsSource is DataView view) ? view.Count : 0;
        get => GridDataView?.Count ?? 0;
    }


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
