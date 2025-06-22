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
public partial class PluginRequiredControl : UserControl//, INotifyPropertyChanged
{
    /// <summary>
    /// コンストラクタ
    /// </summary>
    public PluginRequiredControl()
    {
        InitializeComponent();
        //
        _controlRequired.ControlTableName = PluginRequiredResponse.TableName(false);
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
            int count = 0;
            progresssBarCount?.Invoke(count, 1, "Plugin Required");
            await PluginRequiredRequest.Instance.InsertAll(KintoneManager.CYBOZU_LIMIT, false);
            progresssBarCount?.Invoke(++count);
        };
        win.ShowDialog();
        _loadDatabase();
    }

    /// <summary>
    /// 再読み込み
    /// </summary>
    private void _loadDatabase()
    {
        _controlRequired.Load();
    }
}
