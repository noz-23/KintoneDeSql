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
using KintoneDeSql.Requests.Apps.Views;
using KintoneDeSql.Responses.Apps.Views;
using KintoneDeSql.Views;
using KintoneDeSql.Windows;
using System.Windows;
using System.Windows.Controls;

namespace KintoneDeSql.Controls.Apps.Views;

/// <summary>
/// ReportControl.xaml の相互作用ロジック
/// https://cybozu.dev/ja/kintone/docs/rest-api/apps/report/get-graph-settings/
/// </summary>
public partial class ReportControl : UserControl
{
    /// <summary>
    /// コンストラクタ
    /// </summary>
    public ReportControl()
    {
        InitializeComponent();
        //
        _reportControl.ControlTableName = ReportResponse.TableName(false);
        _aggregationControl.ControlTableName = ReportResponseAggregation.TableName(false);
        _groupControl.ControlTableName = ReportResponseGroup.TableName(false);
        _sortControl.ControlTableName = ReportResponseSort.TableName(false);
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
            _reportControl.ControlWhere = $"WHERE {Resource.COLUMN_APP_ID}='{_appId}'";
            _aggregationControl.ControlWhere = $"WHERE {Resource.COLUMN_APP_ID}='{_appId}'";
            _groupControl.ControlWhere = $"WHERE {Resource.COLUMN_APP_ID}='{_appId}'";
            _sortControl.ControlWhere = $"WHERE {Resource.COLUMN_APP_ID}='{_appId}'";
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
            progressCount?.Invoke(count, _max, _reportControl.ControlTableName);
            var response = await ReportRequest.Instance.Insert(_appId,_apiKey);
            progressCount?.Invoke(1);

            //return count;
        };

        win.ShowDialog();
        progressCount = null;
        //
        var view = new TimeView()
        {
            Name = ReportResponse.TableName(false),
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
        _reportControl.Load();
        _aggregationControl.Load();
        _groupControl.Load();
        _sortControl.Load();
    }
}
