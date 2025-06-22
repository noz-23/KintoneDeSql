/*
 * Kintone De Sql
 * Copyright (c) 2025 noz-23
 *  https://github.com/noz-23/
 * 
 * Licensed under the MIT License 
 * 
 */
using KintoneDeSql.Files;
using KintoneDeSql.Interface;
using KintoneDeSql.Managers;
using KintoneDeSql.Views;
using System.Data;
using System.Windows;

namespace KintoneDeSql.Windows;

/// <summary>
/// DataWindow.xaml の相互作用ロジック
/// </summary>
public partial class RecordDataTableWindow : Window, IAppTable//, INotifyPropertyChanged
{
    /// <summary>
    ///  コンストラクタ
    /// </summary>
    private RecordDataTableWindow()
    {
        LogFile.Instance.WriteLine(@"START");
        InitializeComponent();
        //
    }
    internal RecordDataTableWindow(AppTableView appView_):this()
    {
        LogFile.Instance.WriteLine($"Select Records[{appView_.TableName}]");
        _appView = appView_;
    }

    /// <summary>
    /// アプリ番号
    /// </summary>
    public string AppId
    {
        get => _appView.AppId;
        set { }
    }

    /// <summary>
    /// ApiKey
    /// </summary>
    public string ApiKey
    {
        get => _appView.ApiKey;
        set { }
    }

    /// <summary>
    /// プログレスバー処理
    /// </summary>
    //public WaitWindow.ProgressCountCallBack? _progresssBarCount = null;

    /// <summary>
    /// 表示しているApp 
    /// </summary>
    private AppTableView _appView = new();

    private void _loaded(object sender_, RoutedEventArgs e_)
    {
        _loadDatabase();

        // レコードIDが必要なコントロール
        _commentControl.RecordDataView = _dataGrid.ItemsSource as DataView;
        _recordAclEvaluateControl.RecordDataView = _dataGrid.ItemsSource as DataView;
    }

    /// <summary>
    /// データベースの読み込み
    /// </summary>
    private void _loadDatabase()
    {
        _dataGrid.ItemsSource = null;
        _dataGrid.ItemsSource =  SQLiteManager.Instance.SelectTable($"'{_appView.TableName}'").DefaultView;
    }
}
