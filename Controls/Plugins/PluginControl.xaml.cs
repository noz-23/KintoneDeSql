/*
 * Kintone De Sql
 * Copyright (c) 2025 noz-23
 *  https://github.com/noz-23/
 * 
 * Licensed under the MIT License 
 * 
 */
using KintoneDeSql.Managers;
using KintoneDeSql.Requests.Plugins;
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
    }

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
            progresssBarCount?.Invoke(0, 1, "Plugin");

            var offset = 0;
            var count = 0;
            const int _LIMIT = KintoneManager.CYBOZU_LIMIT;
            do
            {
                var response = await PluginsRequest.Instance.Insert(offset, _LIMIT);
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
            } while (count == _LIMIT);

            progresssBarCount?.Invoke(1);
            return 1;
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
    }
}
