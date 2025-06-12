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
using KintoneDeSql.Requests.Records;
using KintoneDeSql.Responses.Apis;
using KintoneDeSql.Responses.Apps;
using KintoneDeSql.Responses.Records;
using KintoneDeSql.Views;
using System.ComponentModel;
using System.Data;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace KintoneDeSql.Windows;

/// <summary>
/// DataWindow.xaml の相互作用ロジック
/// </summary>
public partial class RecordDataTableWindow : Window, INotifyPropertyChanged
{
    /// <summary>
    ///  コンストラクタ
    /// </summary>
    private RecordDataTableWindow()
    {
        LogFile.Instance.WriteLine(@"START");
        InitializeComponent();
    }

    internal RecordDataTableWindow(AppTableView view_):this()
    {
        AppView = view_;
        //_dataTable = dataTable_;
        //_dataGrid.ItemsSource = _dataTable.DefaultView;
        //
        LogFile.Instance.WriteLine($"Select Records[{view_.TableName}]");
        //
        _notifyPropertyChanged(nameof(CommentDateTime));
        //_notifyPropertyChanged(nameof(AppViewDateTime));
        //_notifyPropertyChanged(nameof(AppFormLayoutDateTime));
    }

    /// <summary>
    /// プログレスバー処理
    /// </summary>
    public WaitWindow.ProgressCountCallBack? _progresssBarCount = null;
    //private Task<DataTable> _dataTableRecord { get => SQLiteManager.Instance.SelectTable($"'{_appView.TableName}'"); }
    //private Task<DataTable> _dataTableComment { get => SQLiteManager.Instance.SelectTable(ListCommentResponse.TableName(false), $"WHERE {Resource.COLUMN_APP_ID}='{_appView.AppId}'"); }
    //private Task<DataTable> _dataTableAppView { get => SQLiteManager.Instance.SelectTable(ListAppViewResponse.TableName(false), $"WHERE {Resource.COLUMN_APP_ID}='{_appView.AppId}'"); }
    //private Task<DataTable> _dataTableAppFormLayout { get => SQLiteManager.Instance.SelectTable(ListAppFormLayoutResponse.TableName(false), $"WHERE {Resource.COLUMN_APP_ID}='{_appView.AppId}'"); }


    internal AppTableView AppView { get; private set; } = new();

    public event PropertyChangedEventHandler? PropertyChanged;
    private void _notifyPropertyChanged([CallerMemberName] string propertyName_ = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName_));
    }
    public DateTime? CommentDateTime 
    {
        get => AppView.CommentDateTime;
        set
        {
            AppView.CommentDateTime = value;
            _notifyPropertyChanged();
        }
    }
    //public DateTime? AppViewDateTime
    //{
    //    get => AppView.AppViewDateTime;
    //    set
    //    {
    //        AppView.AppViewDateTime = value;
    //        _notifyPropertyChanged();
    //    }
    //}
    //public DateTime? AppFormLayoutDateTime
    //{
    //    get => AppView.AppFormLayoutDateTime;
    //    set
    //    {
    //        AppView.AppFormLayoutDateTime = value;
    //        _notifyPropertyChanged();
    //    }
    //}
    //public DateTime? ReportDateTime
    //{
    //    get => AppView.ReportDateTime;
    //    set
    //    {
    //        AppView.ReportDateTime = value;
    //        _notifyPropertyChanged();
    //    }
    //}

    private async void _loaded(object sender_, RoutedEventArgs e_)
    {
        //_dataGridRecord.ItemsSource = null;
        //_dataGridRecord.ItemsSource = (await _dataTableRecord).DefaultView;

        //_dataGridComment.ItemsSource = null;
        //_dataGridComment.ItemsSource = (await _dataTableComment).DefaultView;

        //_dataGridAppView.ItemsSource = null;
        //_dataGridAppView.ItemsSource = (await _dataTableAppView).DefaultView;

        //_dataGridAppFormLayout.ItemsSource = null;
        //_dataGridAppFormLayout.ItemsSource = (await _dataTableAppFormLayout).DefaultView;

        await _loadDatabase();
    }
    private void _autoGeneratingColumn(object sender_, DataGridAutoGeneratingColumnEventArgs e_)
    {
        string header = e_.Column.Header?.ToString()??string.Empty;
        e_.Column.Header = header.Replace("_", "__");
    }


    private async void _commentInsertClick(object sender_, RoutedEventArgs e_)
    {
        //var appId = _appView.AppId;
        //var dataTableRecord = await _dataTableRecord;
        //if (dataTableRecord.Rows.Count > 0)
        //{
        if (_dataGridComment.ItemsSource is DataView viewComment)
        {
            if (viewComment.Count == 0)
            {
                var win = new WaitWindow();
                _progresssBarCount = win.ProgressCount;

                win.Run = async () =>
                {
                    int count = 0;
                    if (_dataGridRecord.ItemsSource is DataView viewRecord)
                    {
                        //
                        _progresssBarCount?.Invoke(0, viewRecord.Count, "Comments");

                        foreach (DataRowView row in viewRecord)
                        {
                            var recordId = row[Resource.COLUMN_MAIN_TABLE_ID].ToString();
                            if (recordId == null)
                            {
                                continue;
                            }

                            var response = await CommentsRequest.Instance.Insert(AppView.AppId, recordId);
                            _progresssBarCount?.Invoke(++count);

                        }
                    }
                    return count;
                };
                win.ShowDialog();
                _progresssBarCount = null;

                CommentDateTime = DateTime.Now;
                LogFile.Instance.WriteLine($"{CommentDateTime.ToString()}");
            }
            //
        }
    }

    //private async void _appViewInsertClick(object sender_, RoutedEventArgs e_)
    //{
    //    //var dataTableAppView = await _dataTableAppView;
    //    if (_dataGridAppView.ItemsSource is DataView view)
    //    {
    //        if (view.Count == 0)
    //        {
    //            var win = new WaitWindow();
    //            _progresssBarCount = win.ProgressCount;

    //            win.Run = async () =>
    //            {
    //                _progresssBarCount?.Invoke(0, 1, "App View");

    //                //var appId = _view.AppId;
    //                var response = await AppViewsRequest.Instance.Insert(AppView.AppId);
    //                _progresssBarCount?.Invoke(1);

    //                return 1;
    //            };
    //            win.ShowDialog();
    //            _progresssBarCount = null;

    //            AppViewDateTime = DateTime.Now;
    //            LogFile.Instance.WriteLine($"{AppViewDateTime}");

    //        }
    //    }
    //    await _loadDatabase();
    //}


    //private async void _appFormLayoutInsertClick(object sender_, RoutedEventArgs e_)
    //{
    //    //var dataTableAppFormLayout = await _dataTableAppFormLayout;

    //    if (_dataGridAppFormLayout.ItemsSource is DataView view)
    //    {
    //        if (view.Count == 0)
    //        {
    //            var win = new WaitWindow();
    //            _progresssBarCount = win.ProgressCount;

    //            win.Run = async () =>
    //            {
    //                _progresssBarCount?.Invoke(0, 1, "App Form Layouts");
    //                //var appId = _view.AppId;
    //                var response = await AppFormLayoutsRequest.Instance.Insert(AppView.AppId);

    //                _progresssBarCount?.Invoke(1);
    //                return 1;
    //            };
    //            win.ShowDialog();
    //            _progresssBarCount = null;

    //            AppFormLayoutDateTime = DateTime.Now;
    //            LogFile.Instance.WriteLine($"{AppFormLayoutDateTime}");
    //        }
    //    }
    //    //}
    //    await _loadDatabase();
    //}

    //private async void _reportInsertClick(object sender_, RoutedEventArgs e_)
    //{
    //    //var dataTableAppFormLayout = await _dataTableAppFormLayout;

    //    if (_dataGridReport.ItemsSource is DataView view)
    //    {
    //        if (view.Count == 0)
    //        {
    //            var win = new WaitWindow();
    //            _progresssBarCount = win.ProgressCount;

    //            win.Run = async () =>
    //            {
    //                _progresssBarCount?.Invoke(0, 1, "Reports");
    //                var response = await ReportRequest.Instance.Insert(AppView.AppId);

    //                _progresssBarCount?.Invoke(1);
    //                return 1;
    //            };
    //            win.ShowDialog();
    //            _progresssBarCount = null;

    //            ReportDateTime = DateTime.Now;
    //            LogFile.Instance.WriteLine($"{ReportDateTime}");
    //        }
    //    }
    //    //}
    //    await _loadDatabase();
    //}

    /// <summary>
    /// データベースの読み込み
    /// </summary>
    /// <returns></returns>
    private async Task _loadDatabase()
    {
        _dataGridRecord.ItemsSource = null;
        _dataGridRecord.ItemsSource = (await SQLiteManager.Instance.SelectTable($"'{AppView.TableName}'")).DefaultView;

        _dataGridComment.ItemsSource = null;
        _dataGridComment.ItemsSource = (await SQLiteManager.Instance.SelectTable(ListCommentResponse.TableName(false), $"WHERE {Resource.COLUMN_APP_ID}='{AppView.AppId}'")).DefaultView;

        //_dataGridAppView.ItemsSource = null;
        //_dataGridAppView.ItemsSource = (await SQLiteManager.Instance.SelectTable(ListAppViewResponse.TableName(false), $"WHERE {Resource.COLUMN_APP_ID}='{AppView.AppId}'")).DefaultView;

        //_dataGridAppFormLayout.ItemsSource = null;
        //_dataGridAppFormLayout.ItemsSource = (await SQLiteManager.Instance.SelectTable(ListAppFormLayoutResponse.TableName(false), $"WHERE {Resource.COLUMN_APP_ID}='{AppView.AppId}'")).DefaultView;

        //_dataGridReport.ItemsSource = null;
        //_dataGridReport.ItemsSource = (await SQLiteManager.Instance.SelectTable(ListReportResponse.TableName(false), $"WHERE {Resource.COLUMN_APP_ID}='{AppView.AppId}'")).DefaultView;

    }


}
