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
using KintoneDeSql.Requests.Cybozu;
using KintoneDeSql.Responses.Cybozu.Organizations;
using KintoneDeSql.Windows;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace KintoneDeSql.Controls.Cybozu;

/// <summary>
/// CybozuOrganizationsControl.xaml の相互作用ロジック
/// https://cybozu.dev/ja/common/docs/user-api/organizations/
/// </summary>
public partial class OrganizationCybozuControl : UserControl
{
    /// <summary>
    /// コンストラクタ
    /// </summary>
    public OrganizationCybozuControl()
    {
        InitializeComponent();
        //
        _controlOrganization.ControlTableName = OrganizationResponse.TableName(false);
        _controlUserTitle.ControlTableName = UserTitleResponse.TableName(false); 
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
            await OrganizationsRequest.Instance.InsertAll( KintoneManager.CYBOZU_LIMIT, true);
            _controlOrganization.Load();
            var dataView = _controlOrganization.GridDataView;
            if (dataView != null)
            {
                int count = 0;
                progresssBarCount?.Invoke(count, dataView.Count, "Organizations");

                foreach (DataRowView row in dataView)
                {
                    var code = row[Resource.COLUMN_RESPONSE_CODE].ToString();

                    if (string.IsNullOrEmpty(code) == false)
                    {
                        await UserTitlesRequest.Instance.InsertAll(code, KintoneManager.CYBOZU_LIMIT, true);
                    }
                    progresssBarCount?.Invoke(++count);
                }
            }
        };
        //
        win.ShowDialog();
        _loadDatabase();
    }

    /// <summary>
    /// 再読み込み
    /// </summary>
    private void _loadDatabase()
    {
        _controlOrganization.Load();
        _controlUserTitle.Load();
    }
}
