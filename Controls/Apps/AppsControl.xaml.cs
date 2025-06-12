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
using KintoneDeSql.Requests.Apps;
using KintoneDeSql.Requests.Records;
using KintoneDeSql.Requests.Spaces;
using KintoneDeSql.Responses.Apps;
using KintoneDeSql.Responses.Spaces;
using KintoneDeSql.Views;
using KintoneDeSql.Windows;
using System.Windows;
using System.Windows.Controls;

namespace KintoneDeSql.Controls;

/// <summary>
/// AppsControl.xaml の相互作用ロジック
/// APP 情報
/// https://cybozu.dev/ja/kintone/docs/rest-api/apps/
/// </summary>
public partial class AppsControl : UserControl
{
    /// <summary>
    /// コンストラクタ
    /// </summary>
    public AppsControl()
    {
        InitializeComponent();
    }

    /// <summary>
    /// プログレスバー処理
    /// </summary>
    public WaitWindow.ProgressCountCallBack? _progresssBarCount = null;

    /// <summary>
    /// 読み込み表示
    /// </summary>
    /// <param name="sender_"></param>
    /// <param name="e_"></param>
    private async void _loaded(object sender_, RoutedEventArgs e_)
    {
        await _loadDatabase();
    }

    /// <summary>
    /// カラム名のアンダースコア表示対応
    /// </summary>
    /// <param name="sender_"></param>
    /// <param name="e_"></param>
    private void _autoGeneratingColumn(object sender_, DataGridAutoGeneratingColumnEventArgs e_)
    {
        string header = e_.Column.Header?.ToString() ?? string.Empty;
        e_.Column.Header = header.Replace("_", "__");
    }

    /// <summary>
    /// Getボタン押下
    /// </summary>
    /// <param name="sender_"></param>
    /// <param name="e_"></param>
    private async void _getClick(object sender_, RoutedEventArgs e_)
    {
        var response = await AppsRequest.Instance.Insert();
        if (response == null)
        {
            return;
        }
        LogFile.Instance.WriteLine($"Apps[{response.ListApp.Count}]");
        //
        var win = new WaitWindow();
        _progresssBarCount = win.ProgressCount;

        win.Run =async()=> await _insertApps(response);
        win.ShowDialog();
        _progresssBarCount = null;

        await _loadDatabase();
    }

    /// <summary>
    /// アプリ一覧からフォーム(フィールド)情報を取得しテーブル作成
    /// </summary>
    /// <param name="response_"></param>
    /// <returns></returns>
    private async Task<int> _insertApps(ListAppResponse response_)
    {
        int count = 0;
        _progresssBarCount?.Invoke(count, response_.ListApp.Count, "App");

        foreach (var app in response_.ListApp)
        {
            // 表示しているアプリ情報取得
            var view = SettingManager.Instance.AppView(app.AppId);
            if (view == null)
            {
                var tableName = (SettingManager.Instance.ExistsTableName(app.Name) == false) ? app.Name : $"{app.Name}_{app.AppId}";
                LogFile.Instance.WriteLine($"AppId:{app.AppId} Name:{app.Name} TableName{tableName}");

                // なければ追加
                view = new AppTableView(app) { TableName = tableName };
                SettingManager.Instance.ListAppTableView.Add(view);
            }
            //
            var responseForm = await AppFormFieldsRequest.Instance.Get(app.AppId);
            if (responseForm == null)
            {
                continue;
            }
            //
            _progresssBarCount?.Invoke(++count);
            //
            if (string.IsNullOrEmpty(view.Revision) == true
             || responseForm.Revision != view.Revision)
            {
                if (string.IsNullOrEmpty(view.Revision) == false)
                {
                    if (MessageBox.Show($"{app.Name} is Revision up?\nDo you want to 'delete' and 'update' ?", "Attention", MessageBoxButton.YesNo) != MessageBoxResult.Yes)
                    {
                        continue;
                    }
                }

                // Revisionが無ければ無条件追加
                SQLiteManager.Instance.InsertTable(ListAppFormFieldResponse.TableName(false), ListAppFormFieldResponse.ListInsertHeader(true), responseForm.ListInsertValue(true));
                // AppTable 作成
                responseForm.CreateAppTable(true);

                view.Revision = responseForm?.Revision ?? string.Empty;
                //
                var responseAdminNote = await AdminNotesRequest.Instance.Insert(app.AppId);

                continue;
            }

            //if (responseForm.Revision != view.Revision)
            //{
            //    if (MessageBox.Show($"{app.Name} is Revision up?\nDo you want to 'delete' and 'update' ?", "Attention", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            //    {
            //        // Revisionが無ければ無条件追加
            //        SQLiteManager.Instance.InsertTable(ListAppFormFieldResponse.TableName(false), ListAppFormFieldResponse.ListInsertHeader(true), responseForm.ListInsertValue(true));
            //        // AppTable 作成
            //        responseForm.CreateAppTable(true);
            //        view.Revision = responseForm?.Revision ?? string.Empty;
            //    }
            //}
        }


        await AppsStatisticRequest.Instance.Insert();
        return count;
    }


    /// <summary>
    /// 各Appのレコード取得
    /// </summary>
    /// <param name="sender_"></param>
    /// <param name="e_"></param>
    private void _insertAppTableClick(object sender_, RoutedEventArgs e_)
    {
        if (sender_ is Button button)
        {
            if (button.DataContext is AppTableView app)
            {
                var win = new WaitWindow();
                LogFile.Instance.WriteLine($"AppId:{app.AppId} Name:{app.Name}");

                RecordsRequest.Instance.ProgresssBarCount = win.ProgressCount;
                win.Run = async () => await RecordsRequest.Instance.Insert(app.AppId);
                win.ShowDialog();

                if (win.Count != 0)
                {
                    app.Count = win.Count;
                    app.RecordDateTime = DateTime.Now;
                }
                RecordsRequest.Instance.ProgresssBarCount = null;
            }
        }
    }

    /// <summary>
    /// 各Appのレコード表示
    /// </summary>
    /// <param name="sender_"></param>
    /// <param name="e_"></param>
    private void _selectAppTableClick(object sender_, RoutedEventArgs e_)
    {
        if (sender_ is Button button)
        {
            if (button.DataContext is AppTableView app)
            {
                LogFile.Instance.WriteLine($"AppId:{app.AppId} Name:{app.Name}");
                var win = new RecordDataTableWindow(app);
                win.ShowDialog();
            }
        }
    }

    /// <summary>
    /// サブテーブルのレコード表示
    /// </summary>
    /// <param name="sender_"></param>
    /// <param name="e_"></param>
    private async void _selectSubTableClick(object sender_, RoutedEventArgs e_)
    {
        if (sender_ is Button button)
        {
            if (button.DataContext is SubTableView sub)
            {
                LogFile.Instance.WriteLine($"AppId:{sub.AppId} Name:{sub.Name}");
                var dataTable = await SQLiteManager.Instance.SelectTable($"'{sub.TableName}'");
                var win = new DataTableWindow(dataTable);
                win.ShowDialog();
            }
        }
    }

    /// <summary>
    /// データベースの読み込み
    /// </summary>
    /// <returns></returns>
    private async Task _loadDatabase()
    {
        _dataGridApps.ItemsSource = null;
        _dataGridApps.ItemsSource = (await SQLiteManager.Instance.SelectTable(ListAppResponse.TableName(false))).DefaultView;
        //
        _dataGridAppFormFields.ItemsSource = null;
        _dataGridAppFormFields.ItemsSource = (await SQLiteManager.Instance.SelectTable(ListAppFormFieldResponse.TableName(false))).DefaultView;
        //
        _dataGridAdminNotes.ItemsSource = null;
        _dataGridAdminNotes.ItemsSource = (await SQLiteManager.Instance.SelectTable(AdminNotesResponse.TableName(false))).DefaultView;
        //
        _dataGridStatics.ItemsSource = null;
        _dataGridStatics.ItemsSource = (await SQLiteManager.Instance.SelectTable(ListAppsStatisticResponse.TableName(false))).DefaultView;
    }
}