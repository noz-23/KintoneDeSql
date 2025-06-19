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
/// https://cybozu.dev/ja/kintone/docs/rest-api/apps/settings/get-action-settings/
/// </summary>
namespace KintoneDeSql.Controls.Apps.Settings;

/// <summary>
/// AppActionControl.xaml の相互作用ロジック
/// </summary>
public partial class AppActionControl : UserControl
{
    /// <summary>
    /// コンストラクタ
    /// </summary>
    public AppActionControl()
    {
        InitializeComponent();
        //
        _actionControl.ControlTableName = AppActionResponse.TableName(false);
        _entityControl.ControlTableName = AppActionResponseEntity.TableName(false);
        _mappingControl.ControlTableName = AppActionResponseMapping.TableName(false);
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
            _apiKey= win.ApiKey;
            //
            _actionControl.ControlWhere = $"WHERE {Resource.COLUMN_APP_ID}='{_appId}'";
            _entityControl.ControlWhere = $"WHERE {Resource.COLUMN_APP_ID}='{_appId}'";
            _mappingControl.ControlWhere = $"WHERE {Resource.COLUMN_APP_ID}='{_appId}'";
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
            const int _max = 1;
            var count = 0;
            //
            progressCount?.Invoke(count, _max, AppActionResponse.TableName(false));
            var response = await AppActionRequest.Instance.Insert(_appId, _apiKey);
            progressCount?.Invoke(count);
            //
            //return count;
        };

        win.ShowDialog();
        progressCount = null;
        //
        var view = new TimeView()
        {
            Name = AppActionResponse.TableName(false),
            Id = _appId,
            Time = DateTime.Now
        };
        SQLiteManager.Instance.InsertTable(typeof(TimeView).TableName(false), typeof(TimeView).ListInsertHeader(true), new List<IList<string>>() { view.ListValue(true) });
        //
        _loadDatabase();
    }

    /// <summary>
    /// 再読み込み
    /// </summary>
    private void _loadDatabase()
    {
        _actionControl.Load();
        _entityControl.Load();
        _mappingControl.Load();
    }
}
