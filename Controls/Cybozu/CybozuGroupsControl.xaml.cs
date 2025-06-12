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
using KintoneDeSql.Requests.Cybozu;
using KintoneDeSql.Responses.Cybozu;
using KintoneDeSql.Windows;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace KintoneDeSql.Controls;

/// <summary>
/// CybozuGroupsControl.xaml の相互作用ロジック
/// https://cybozu.dev/ja/common/docs/user-api/groups/
/// </summary>
public partial class CybozuGroupsControl : UserControl
{
    /// <summary>
    /// コンストラクタ
    /// </summary>
    public CybozuGroupsControl()
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
        if (_dataGridGroups.ItemsSource is DataView viewGroups)
        {
            if (viewGroups.Count == 0)
            {
                var win = new WaitWindow();
                _progresssBarCount = win.ProgressCount;

                win.Run = async () =>
                {
                    var response = await GroupsRequest.Instance.Insert();
                    LogFile.Instance.WriteLine($"Groups[{response}]");
                    //
                    int count = 0;
                    _progresssBarCount?.Invoke(0, response.ListGroup.Count,"Groups");

                    foreach (var group in response.ListGroup)
                    {
                        var responseUsers = await GroupUsersRequest.Instance.Insert(group.Code);
                        _progresssBarCount?.Invoke(++count);
                    }
                    return count;
                };
                //
                win.ShowDialog();
                _progresssBarCount = null;
            }
        }
        await _loadDatabase();
    }

    /// <summary>
    /// Tab切り替え
    /// </summary>
    /// <param name="sender_"></param>
    /// <param name="e_"></param>
    private async void _selectionChanged(object sender_, SelectionChangedEventArgs e_)
    {
        await _loadDatabase();
    }

    /// <summary>
    /// データベース読み込み
    /// </summary>
    /// <returns></returns>
    private async Task _loadDatabase()
    {
        _dataGridGroups.ItemsSource = null;
        _dataGridGroups.ItemsSource = (await SQLiteManager.Instance.SelectTable(ListGroupResponse.TableName(false))).DefaultView;
        //
        _dataGridUsers.ItemsSource = null;
        _dataGridUsers.ItemsSource = (await SQLiteManager.Instance.SelectTable(ListUserTitleRespons.TableName(false))).DefaultView; ;
    }
}
