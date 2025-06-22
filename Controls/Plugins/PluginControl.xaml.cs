/*
 * Kintone De Sql
 * Copyright (c) 2025 noz-23
 *  https://github.com/noz-23/
 * 
 * Licensed under the MIT License 
 * 
 */
using KintoneDeSql.Managers;
using KintoneDeSql.Properties;
using KintoneDeSql.Requests.Plugins;
using KintoneDeSql.Responses.Plugin;
using KintoneDeSql.Responses.Plugins;
using KintoneDeSql.Windows;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace KintoneDeSql.Controls.Plugins;

/// <summary>
/// AppAclControl.xaml の相互作用ロジック
/// </summary>
public partial class PluginsControl : UserControl//, INotifyPropertyChanged
{
    /// <summary>
    /// コンストラクタ
    /// </summary>
    public PluginsControl()
    {
        InitializeComponent();
        //
        _pluginControl.ControlTableName = PluginResponsee.TableName(false);
        _pluginAppControl.ControlTableName = PluginAppResponse.TableName(false);
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
        var progresssBarCount = win.ProgressCount;

        win.Run = async () =>
        {
            await PluginRequest.Instance.InsertAll(KintoneManager.CYBOZU_LIMIT, false);
            _pluginControl.Load();
            var dataView = _pluginControl.GridDataView;
            if (dataView != null)
            {
                int count = 0;
                progresssBarCount?.Invoke(count, dataView.Count, "Plugins");

                foreach (DataRowView row in dataView)
                {
                    var id = row[Resource.COLUMN_RESPONSE_ID].ToString();

                    if (string.IsNullOrEmpty(id) == false)
                    {
                        await PluginAppRequest.Instance.InsertAll(id, KintoneManager.CYBOZU_LIMIT, false);
                    }
                    progresssBarCount?.Invoke(++count);
                }
                //
            }
        };
        win.ShowDialog();
        _loadDatabase();
    }

    /// <summary>
    /// 再読み込み
    /// </summary>
    private void _loadDatabase()
    {
        _pluginControl.Load();
        _pluginAppControl.Load();
    }
}
