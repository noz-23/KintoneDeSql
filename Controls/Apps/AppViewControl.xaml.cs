/*
 * Kintone De Sql
 * Copyright (c) 2025 noz-23
 *  https://github.com/noz-23/
 * 
 * Licensed under the MIT License 
 * 
 */
using KintoneDeSql.Files;
using KintoneDeSql.Managers;
using KintoneDeSql.Properties;
using KintoneDeSql.Requests.Apps;
using KintoneDeSql.Responses.Apps;
using KintoneDeSql.Views;
using KintoneDeSql.Windows;
using System.ComponentModel;
using System.Data;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace KintoneDeSql.Controls.Apps;

/// <summary>
/// AppViewControl.xaml の相互作用ロジック
/// </summary>
public partial class AppViewControl : UserControl, INotifyPropertyChanged
{
    public AppViewControl()
    {
        InitializeComponent();
    }

    private AppTableView _appView =new ();

    public event PropertyChangedEventHandler? PropertyChanged;
    private void _notifyPropertyChanged([CallerMemberName] string propertyName_ = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName_));
    }
    public DateTime? ControlDateTime
    {
        get => _appView.AppViewDateTime;
        set
        {
            _appView.AppViewDateTime = value;
            _notifyPropertyChanged();
        }
    }
    private async void _loaded(object sender_, RoutedEventArgs e_)
    {
        if (Window.GetWindow(this) is RecordDataTableWindow win)
        {
            _appView =win.AppView;
        }
        await _loadDatabase();
        _notifyPropertyChanged(nameof(ControlDateTime));
    }
    private void _autoGeneratingColumn(object sender_, DataGridAutoGeneratingColumnEventArgs e_)
    {
        string header = e_.Column.Header?.ToString() ?? string.Empty;
        e_.Column.Header = header.Replace("_", "__");
    }

    private async void _getClick(object sender_, RoutedEventArgs e_)
    {
        if (_dataGrid.ItemsSource is DataView view)
        {
            if (view.Count == 0)
            {
                var win = new WaitWindow();
                var progresssBarCount = win.ProgressCount;

                win.Run = async () =>
                {
                    progresssBarCount?.Invoke(0, 1, "App View");
                    var response = await AppViewsRequest.Instance.Insert(_appView.AppId);

                    progresssBarCount?.Invoke(1);
                    return 1;
                };
                win.ShowDialog();

                ControlDateTime = DateTime.Now;
                LogFile.Instance.WriteLine($"{ControlDateTime}");
            }
        }
        //}
        await _loadDatabase();
    }

    private async Task _loadDatabase()
    {
        _dataGrid.ItemsSource = null;
        _dataGrid.ItemsSource = (await SQLiteManager.Instance.SelectTable(ListAppViewResponse.TableName(false), $"WHERE {Resource.COLUMN_APP_ID}='{_appView.AppId}'")).DefaultView;
    }

}
