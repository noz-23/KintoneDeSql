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
using KintoneDeSql.Requests.Apps.Forms;
using KintoneDeSql.Requests.Apps.Settings;
using KintoneDeSql.Requests.Records;
using KintoneDeSql.Requests.Spaces;
using KintoneDeSql.Responses.Apps;
using KintoneDeSql.Responses.Apps.Settings;
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
        //
        _appControl.ControlTableName = AppGetResponse.TableName(false);
        _adminNoteControl.ControlTableName = AdminNotesResponse.TableName(false);
        _staticControl.ControlTableName = AppsStatisticResponse.TableName(false);
        _deployControl.ControlTableName = AppDeployResponse.TableName(false);
        _settingControl.ControlTableName = AppSettingResponse.TableName(false);
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
    private void _loaded(object sender_, RoutedEventArgs e_)
    {
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
        var response = await AppsGetRequest.Instance.Insert();
        if (response == null)
        {
            return;
        }
        LogFile.Instance.WriteLine($"Apps[{response.ListApp.Count}]");
        //
        var win = new WaitWindow();
        _progresssBarCount = win.ProgressCount;
        //
        win.Run =async()=> await _insertApps(response);
        win.ShowDialog();
        _progresssBarCount = null;
    }

    /// <summary>
    /// 各Appのレコード取得
    /// </summary>
    /// <param name="sender_"></param>
    /// <param name="e_"></param>
    private async void _insertAppTableClick(object sender_, RoutedEventArgs e_)
    {
        if (sender_ is Button button
         && button.DataContext is AppTableView app)
        {
            ///
            if(await _apiKeyCreateTable(app)==false)
            {
                return;
            }

            var win = new WaitWindow();
            LogFile.Instance.WriteLine($"AppId:{app.AppId} Name:{app.Name}");

            var progresssBarCount = win.ProgressCount;

            win.Run = async () =>
            {
                var count = 0;
                var lastId = "0";
                var offset = 0;
                const int _LIMIT = KintoneManager.RECORD_LIMIT;
                do
                {
                    var response = await RecordRequest.Instance.Insert(app.AppId, app.ApiKey, lastId, _LIMIT,true);
                    if (response == null)
                    {
                        return offset;
                    }
                    count = response.ListRecord.Count;
                    //
                    if (offset == 0 && count == _LIMIT)
                    {
                        var totalCount = Convert.ToInt32(response.TotalCount);
                        var apiTimes = totalCount / _LIMIT;

                        if (MessageBox.Show($"Get All {totalCount} records?\nUse Api Count {apiTimes} times", "Attention", MessageBoxButton.YesNo) == MessageBoxResult.No)
                        {
                            return count;
                        }

                        progresssBarCount?.Invoke(offset, totalCount, $"{app.TableName} Records");
                    }
                    offset += count;
                    var lastData = response.ListRecord.Last();
                    if (lastData != null)
                    {
                        if (lastData.TryGetValue(Resource.COLUMN_MAIN_TABLE_ID, out var val))
                        {
                            lastId = val.Value?.ToString() ?? "0";
                            LogFile.Instance.WriteLine($"LAST ID[{lastId}]");
                        }
                    }
                } while (count == _LIMIT);

                return offset;

            };
            win.ShowDialog();

            if (win.Count != 0)
            {
                app.Count = win.Count;
                app.RecordDateTime = DateTime.Now;
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
        if (sender_ is Button button 
         && button.DataContext is AppTableView app)
        {
            LogFile.Instance.WriteLine($"AppId:{app.AppId} Name:{app.Name}");
            var win = new RecordDataTableWindow(app);
            win.ShowDialog();
        }
    }

    /// <summary>
    /// サブテーブルのレコード表示
    /// </summary>
    /// <param name="sender_"></param>
    /// <param name="e_"></param>
    private void _selectSubTableClick(object sender_, RoutedEventArgs e_)
    {
        if (sender_ is Button button 
         && button.DataContext is SubTableView sub)
        {
            LogFile.Instance.WriteLine($"AppId:{sub.AppId} Name:{sub.Name}");
            var dataTable = SQLiteManager.Instance.SelectTable($"'{sub.TableName}'");
            var win = new DataTableWindow(dataTable);
            win.ShowDialog();
        }
    }

    /// <summary>
    /// ApiKey でテーブル作成
    /// </summary>
    /// <param name="app_"></param>
    private async Task<bool> _apiKeyCreateTable(AppTableView app_)
    {
        if (SQLiteManager.Instance.ExsistsTable(app_.TableName) == false)
        {
            if (string.IsNullOrEmpty(app_.TableName) == true)
            {
                MessageBox.Show($"TableName, AppId, ApiKey are Empty\nPlease inputs");
                return false;
            }

            // レコード情報からテーブル作成
            var response = await RecordRequest.Instance.Get(app_.AppId, app_.ApiKey, "0", 1,false);
            if (response == null)
            {
                return false;
            }
            //
            if (response.ListRecord.Any() == true)
            {
                SQLiteManager.Instance.CreateTable(app_.TableName, response.ListCreateHeader(true));
                foreach (var subTable in response.ListSubTable().Where(field_ => field_.FieldType == Enums.FieldToDatabaseTypeEnum.SUBTABLE))
                {
                    var subName = subTable.SubTableName(false);
                    SQLiteManager.Instance.DropTable(subName);
                    SQLiteManager.Instance.CreateTable(subName, subTable.ListSubCreateHeader(true));
                    //
                    SettingManager.Instance.ListSubTableView.Add(new() { AppId = app_.AppId, TableName = subName, Name = subTable.MainFieldName });
                }
            }
            else
            {
                return false;
            }
        }

        return true;
    }


    /// <summary>
    /// アプリ一覧からフォーム(フィールド)情報を取得しテーブル作成
    /// </summary>
    /// <param name="response_"></param>
    /// <returns></returns>
    private async Task<int> _insertApps(AppGetResponse response_)
    {
        int count = 0;
        _progresssBarCount?.Invoke(count, response_.ListApp.Count, "App");
        //
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
            var responseForm = await AppFormFieldsRequest.Instance.Get(app.AppId, string.Empty);
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
                AppFormFieldsRequest.Instance.Insert(responseForm);

                // AppTable 作成
                responseForm.CreateAppTable(true);
                //
                await AppSettingRequest.Instance.Insert(app.AppId, string.Empty);

                view.Revision = responseForm?.Revision ?? string.Empty;
                //
                var responseAdminNote = await AdminNotesRequest.Instance.Insert(app.AppId, string.Empty);
            }
        }

        _appDeploy(response_);
        _appStatistic(response_);

        return count;
    }

    /// <summary>
    /// アプリ設定を運用環境へ反映取得
    /// </summary>
    /// <param name="response_"></param>
    private async void _appDeploy(AppGetResponse response_)
    {
        int count = 0;
        _progresssBarCount?.Invoke(count, response_.ListApp.Count, "App Deploy");
        foreach (var list in response_.ListApp.Select(app_ => app_.AppId).Chunk(KintoneManager.API_LIMIT))
        {
            var response = await AppDeployRequest.Instance.Insert(list.ToList());
            count += list.Count();
            _progresssBarCount?.Invoke(count);

        }
    }

    /// <summary>
    /// アプリ使用状況を取得
    /// </summary>
    /// <param name="response_"></param>
    private async void _appStatistic(AppGetResponse response_)
    {
        var offset = 0;
        var count = 0;
        const int _LIMIT = KintoneManager.CYBOZU_LIMIT;
        do
        {
            var response = await AppsStatisticRequest.Instance.Insert(offset, _LIMIT, false);
            if (response == null)
            {
                break;
            }
            if (response.ListApp.Count == 0)
            {
                break;
            }
            //
            count = response.ListApp.Count;
            offset += count;
        } while (count == _LIMIT);
    }
}