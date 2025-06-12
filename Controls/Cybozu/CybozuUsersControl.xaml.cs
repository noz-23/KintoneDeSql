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
/// CybozuUsersControl.xaml の相互作用ロジック
/// https://cybozu.dev/ja/common/docs/user-api/users/
/// </summary>
public partial class CybozuUsersControl : UserControl
{
    /// <summary>
    /// コンストラクタ
    /// </summary>
    public CybozuUsersControl()
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
        if (_dataGridUsers.ItemsSource is DataView viewUser)
        {
            if (viewUser.Count == 0)
            {
                var win = new WaitWindow();
                _progresssBarCount = win.ProgressCount;

                win.Run = async () =>
                {
                    var response = await UsersRequest.Instance.Insert();
                    LogFile.Instance.WriteLine($"Users[{response.ToString()}]");
                    //
                    int count = 0;
                    _progresssBarCount?.Invoke(0, response.ListUser.Count, "Users");

                    foreach (var user in response.ListUser)
                    {
                        var responseOrganizations = await OrganizationTitlesRequest.Instance.Insert(user.Code);
                        var responseGroups = await UserGroupsRequest.Instance.Insert(user.Code);
                        _progresssBarCount?.Invoke(++count);
                    }
                    return count;
                };
                //
                win.ShowDialog();
                _progresssBarCount = null;
            }
        }

        if (_dataGridServices.ItemsSource is DataView viewUserServices)
        {
            if (viewUserServices.Count == 0)
            {
                var win = new WaitWindow();
                _progresssBarCount = win.ProgressCount;
                //
                int count = 0;
                _progresssBarCount?.Invoke(0, 1, "User Services");

                win.Run = async () =>
                {
                    var response = await UserServicesRequest.Instance.Insert();
                    LogFile.Instance.WriteLine($"UserServicess[{response.ToString()}]");
                    _progresssBarCount?.Invoke(++count);
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
        _dataGridUsers.ItemsSource = null;
        _dataGridUsers.ItemsSource = (await SQLiteManager.Instance.SelectTable(ListUserResponse.TableName(false))).DefaultView;
        //
        _dataGridCustomItemValues.ItemsSource = null;
        _dataGridCustomItemValues.ItemsSource = (await SQLiteManager.Instance.SelectTable(ListCustomItemValueResponse.TableName(false))).DefaultView;
        //
        _dataGridServices.ItemsSource = null;
        _dataGridServices.ItemsSource = (await SQLiteManager.Instance.SelectTable(ListUsersServiceResponse.TableName(false))).DefaultView;
        //
        _dataGridOrganizationTitles.ItemsSource = null;
        _dataGridOrganizationTitles.ItemsSource = (await SQLiteManager.Instance.SelectTable(ListOrganizationTitleResponse.TableName(false))).DefaultView;
        //
        _dataGridGroups.ItemsSource = null;
        _dataGridGroups.ItemsSource = (await SQLiteManager.Instance.SelectTable(ListUserGroupResponse.TableName(false))).DefaultView;
    }
}
