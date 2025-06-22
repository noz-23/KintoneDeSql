/*
 * Kintone De Sql
 * Copyright (c) 2025 noz-23
 *  https://github.com/noz-23/
 * 
 * Licensed under the MIT License 
 * 
 */
using KintoneDeSql.Extensions;
using KintoneDeSql.Interface;
using KintoneDeSql.Managers;
using KintoneDeSql.Properties;
using KintoneDeSql.Requests.Apps.Settings;
using KintoneDeSql.Responses.Apps.Settings;
using KintoneDeSql.Views;
using KintoneDeSql.Windows;
using System.Windows;
using System.Windows.Controls;

/// <summary>
/// https://cybozu.dev/ja/kintone/docs/rest-api/apps/settings/get-process-management-settings/
/// </summary>
namespace KintoneDeSql.Controls.Apps.Settings;

/// <summary>
/// AppStatusControl.xaml の相互作用ロジック
/// </summary>
public partial class AppStatusControl : UserControl
{
    /// <summary>
    /// コンストラクタ
    /// </summary>
    public AppStatusControl()
    {
        InitializeComponent();
        //
        _mainControl.ControlTableName = AppStatusResponse.TableName(false);
        _statusControl.ControlTableName = AppStatusResponseStatus.TableName(false);
        _actionControl.ControlTableName = AppStatusResponseAction.TableName(false);
        _entityControl.ControlTableName = AppStatusResponseEntity.TableName(false);
    }

    /// <summary>
    /// App ID
    /// </summary>
    private string _appId  = string.Empty;

    /// <summary>
    /// Api Key
    /// </summary>
    private string _apiKey = string.Empty;

    /// <summary>
    /// 読み込み処理
    /// </summary>
    /// <param name="sender_"></param>
    /// <param name="e_"></param>
    private void _loaded(object sender_, RoutedEventArgs e_)
    {
        if (Window.GetWindow(this) is IAppTable win)
        {
            _appId = win.AppId;
            _apiKey = win.ApiKey;
            //
            _mainControl.ControlWhere = $"WHERE {Resource.COLUMN_APP_ID}='{_appId}'";
            _statusControl.ControlWhere = $"WHERE {Resource.COLUMN_APP_ID}='{_appId}'";
            _actionControl.ControlWhere = $"WHERE {Resource.COLUMN_APP_ID}='{_appId}'";
            _entityControl.ControlWhere = $"WHERE {Resource.COLUMN_APP_ID}='{_appId}'";
        }
        //
        _loadDatabase();
    }

    /// <summary>
    /// Getボタン押下
    /// </summary>
    /// <param name="sender_"></param>
    /// <param name="e_"></param>
    private void _getClick(object sender_, RoutedEventArgs e_)
    {
        var win = new WaitWindow();
        var progressCount = win.ProgressCount;

        win.Run = async () =>
        {
            const int _MAX = 1;
            var count = 0;
            //
            progressCount?.Invoke(count, _MAX, AppStatusResponse.TableName(false));
            var response = await AppStatusRequest.Instance.Insert(_appId,_apiKey);
            progressCount?.Invoke(count);
            //
            //return count;
        };

        win.ShowDialog();
        progressCount = null;
        //
        var view = new TimeView(_appId, AppActionResponse.TableName(false), DateTime.Now);
        SQLiteManager.Instance.InsertTable(typeof(TimeView).TableName(false), typeof(TimeView).ListInsertHeader(true), new List<IList<string>>() { view.ListValue(true) });
        //
        _loadDatabase();
    }

    /// <summary>
    /// 再読み込み
    /// </summary>
    private void _loadDatabase()
    {
        _mainControl.Load();
        _statusControl.Load();
        _actionControl.Load();
        _entityControl.Load();
    }
}
