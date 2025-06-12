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
/// CybozuOrganizationsControl.xaml の相互作用ロジック
/// https://cybozu.dev/ja/common/docs/user-api/organizations/
/// </summary>
public partial class CybozuOrganizationsControl : UserControl
{
    /// <summary>
    /// コンストラクタ
    /// </summary>
    public CybozuOrganizationsControl()
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
        string header = e_.Column.Header?.ToString()??string.Empty;
        e_.Column.Header = header.Replace("_", "__");
    }

    /// <summary>
    /// Getボタン押下
    /// </summary>
    /// <param name="sender_"></param>
    /// <param name="e_"></param>
    private async void _getClick(object sender_, RoutedEventArgs e_)
    {
       if (_dataGridOrganizations.ItemsSource is DataView viewOrganizations)
        {
            if (viewOrganizations.Count == 0)
            {
                var win = new WaitWindow();
                _progresssBarCount = win.ProgressCount;

                win.Run = async () =>
                {
                    //
                    var response = await OrganizationsRequest.Instance.Insert();
                    LogFile.Instance.WriteLine($"Organizations[{response.ToString()}]");

                    int count = 0;
                    _progresssBarCount?.Invoke(0, response.ListOrganization.Count, "Organizations");
                    foreach (var organization in response.ListOrganization)
                    {
                        var responseUsers = await UserTitlesRequest.Instance.Insert(organization.Code);
                        _progresssBarCount?.Invoke(++count);
                    }
                    //
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
        _dataGridOrganizations.ItemsSource = null;
        _dataGridOrganizations.ItemsSource = (await SQLiteManager.Instance.SelectTable(ListOrganizationResponse.TableName(false))).DefaultView;
        //
        _dataGridUsers.ItemsSource = null;
        _dataGridUsers.ItemsSource = (await SQLiteManager.Instance.SelectTable(ListGroupUserResponse.TableName(false))).DefaultView; ;
    }
}
