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
using KintoneDeSql.Requests.Spaces;
using KintoneDeSql.Responses.Spaces;
using KintoneDeSql.Windows;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace KintoneDeSql.Controls;

/// <summary>
/// SpaceControl.xaml の相互作用ロジック
/// https://cybozu.dev/ja/kintone/docs/rest-api/spaces/
/// </summary>
public partial class SpaceControl : UserControl
{
    /// <summary>
    /// コンストラクタ
    /// </summary>
    public SpaceControl()
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
        if (_dataGridSpace.ItemsSource is DataView viewSpace)
        {
            if (viewSpace.Count == 0)
            {
                var win = new WaitWindow();
                _progresssBarCount = win.ProgressCount;

                var spaceMax = Int32.Parse(_textBox.Text);

                win.Run = async () =>
                {
                    int count = 0;
                    _progresssBarCount?.Invoke(0, spaceMax, "Spaces");
                    //
                    for (var i = 1; i < spaceMax; i++)
                    {
                        var response = await SpacesRequest.Instance.Insert($"{i}");
                        LogFile.Instance.WriteLine($"[{response}]");
                        _progresssBarCount?.Invoke(++count);

                    }
                    return count;
                };
                //
                win.ShowDialog();
                _progresssBarCount = null;
            }
        }
        if (_dataGridStatics.ItemsSource is DataView viewStatics)
        {
            if (viewStatics.Count == 0)
            {
                var win = new WaitWindow();
                _progresssBarCount = win.ProgressCount;
                //
                win.Run = async () =>
                {
                    int count = 0;
                    _progresssBarCount?.Invoke(0, 1, "Space Statistics");

                    var response = await SpacesStatisticsRequest.Instance.Insert();
                    LogFile.Instance.WriteLine($"[{response.ToString()}]");
                    _progresssBarCount?.Invoke(++count);

                    return count;
                };
                //
                win.ShowDialog();
                _progresssBarCount = null;
            }
        }
    }

    /// <summary>
    /// データベース読み込み
    /// </summary>
    /// <returns></returns>
    private async Task _loadDatabase()
    {
        _dataGridSpace.ItemsSource = null;
        _dataGridSpace.ItemsSource = (await SQLiteManager.Instance.SelectTable(SpaceResponse.TableName(false))).DefaultView;
        //
        _dataGridApp.ItemsSource = null;
        _dataGridApp.ItemsSource = (await SQLiteManager.Instance.SelectTable(ListAttachedAppResponse.TableName(false))).DefaultView;
        //
        _dataGridStatics.ItemsSource = null;
        _dataGridStatics.ItemsSource = (await SQLiteManager.Instance.SelectTable(ListSpacesStatisticResponse.TableName(false))).DefaultView;
    }

}
