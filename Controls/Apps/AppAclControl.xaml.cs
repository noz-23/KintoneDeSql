/*
 * Kintone De Sql
 * Copyright (c) 2025 noz-23
 *  https://github.com/noz-23/
 * 
 * Licensed under the MIT License 
 * 
 */
using KintoneDeSql.Extensions;
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
/// AppAclControl.xaml の相互作用ロジック
/// </summary>
public partial class AppAclControl : BaseAppControl
{
    public AppAclControl():base()
    {
        //InitializeComponent();
    }

    public override string ControlTableName
    {
        get => ListAppAclResponse.TableName(false);
    }
    public override async Task ControlInsert(string appId_)
    {
        var response =await AppAclRequest.Instance.Insert(appId_);
    }

    //private AppTableView _appView =new ();

    //public event PropertyChangedEventHandler? PropertyChanged;
    //private void _notifyPropertyChanged([CallerMemberName] string propertyName_ = "")
    //{
    //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName_));
    //}
    //public DateTime? ControlDateTime
    //{
    //    get => _getTime();
    //    set
    //    {
    //        //_appView.AppAclDateTime = value;
    //        _setTime(value);
    //        _notifyPropertyChanged();
    //    }
    //}
    //private async void _loaded(object sender_, RoutedEventArgs e_)
    //{
    //    if (Window.GetWindow(this) is RecordDataTableWindow win)
    //    {
    //        _appView =win.AppView;
    //    }
    //    await _loadDatabase();
    //    _notifyPropertyChanged(nameof(ControlDateTime));
    //}
    //private void _autoGeneratingColumn(object sender_, DataGridAutoGeneratingColumnEventArgs e_)
    //{
    //    string header = e_.Column.Header?.ToString() ?? string.Empty;
    //    e_.Column.Header = header.Replace("_", "__");
    //}

    //private async void _getClick(object sender_, RoutedEventArgs e_)
    //{
    //    if (_dataGrid.ItemsSource is DataView view)
    //    {
    //        if (view.Count == 0)
    //        {
    //            var win = new WaitWindow();
    //            var progresssBarCount = win.ProgressCount;

    //            win.Run = async () =>
    //            {
    //                progresssBarCount?.Invoke(0, 1, "Acl");
    //                var response = await AppAclRequest.Instance.Insert(_appView.AppId);

    //                progresssBarCount?.Invoke(1);
    //                return 1;
    //            };
    //            win.ShowDialog();

    //            ControlDateTime = DateTime.Now;
    //            LogFile.Instance.WriteLine($"{ControlDateTime}");
    //        }
    //    }
    //    //}
    //    await _loadDatabase();
    //}

    //private async Task _loadDatabase()
    //{
    //    _dataGrid.ItemsSource = null;
    //    _dataGrid.ItemsSource = (await SQLiteManager.Instance.SelectTable(ListAppAclResponse.TableName(false), $"WHERE {Resource.COLUMN_APP_ID}='{_appView.AppId}'")).DefaultView;

    //}

    //private DateTime? _getTime()
    //{
    //    var list =SQLiteManager.Instance.SelectTable<TimeView>(false).Result;

    //    if (list.Any() ==true)
    //    {
    //        return list.FirstOrDefault()?.Time;
    //    }

    //    return null;
    //}
    //private void _setTime(DateTime? time_)
    //{
    //    if (time_ == null)
    //    {
    //        return;
    //    }

    //    var view = new TimeView()
    //    {
    //        Name = ListAppAclResponse.TableName(false),
    //        Id = _appView.AppId,
    //        Time = time_
    //    };
    //    SQLiteManager.Instance.InsertTable(typeof(TimeView).TableName(false), typeof(TimeView).ListInsertHeader(true), new List<List<string>>() { view.ListValue(true)});
    //}

}
