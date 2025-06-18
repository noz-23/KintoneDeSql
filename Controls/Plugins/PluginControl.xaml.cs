/*
 * Kintone De Sql
 * Copyright (c) 2025 noz-23
 *  https://github.com/noz-23/
 * 
 * Licensed under the MIT License 
 * 
 */
using KintoneDeSql.Managers;
using KintoneDeSql.Requests.Cybozu;
using KintoneDeSql.Requests.Plugins;
using KintoneDeSql.Responses.Cybozu.Groups;
using KintoneDeSql.Responses.Plugin;
using KintoneDeSql.Responses.Plugins;
using KintoneDeSql.Windows;
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
        _progresssBarCount = win.ProgressCount;

        win.Run = async () =>
        {
            var offset = 0;
            var count = 0;
            const int _LIMIT = KintoneManager.CYBOZU_LIMIT;
            do
            {
                var response = await PluginRequest.Instance.Insert(offset, _LIMIT, false);
                if (response == null)
                {
                    break;
                }
                if (response.ListPlugin == null)
                {
                    break;
                }                        //
                count = response.ListPlugin.Count;
                offset += count;
                //
                _pluginAppInsert(response.ListPlugin);

            } while (count == _LIMIT);

            return offset;
        };
        win.ShowDialog();
        _progresssBarCount = null;
        _loadDatabase();
    }

    private async void _pluginAppInsert(IList<PluginValue> list_)
    {
        const int _LIMIT = KintoneManager.RECORD_LIMIT;

        var pluginCount = 0;
        _progresssBarCount?.Invoke(pluginCount, list_.Count, "Plugin App");
        foreach (var plugIn in list_)
        {
            var offset = 0;
            var count = 0;

            do
            {
                var response = await PluginAppRequest.Instance.Insert(plugIn.Id, offset, _LIMIT,false);
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
                //
                _progresssBarCount?.Invoke(++pluginCount);
            } while (count == _LIMIT);
        }
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
