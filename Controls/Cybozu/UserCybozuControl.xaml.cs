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
using KintoneDeSql.Responses.Cybozu.Users;
using KintoneDeSql.Windows;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace KintoneDeSql.Controls.Cybozu;

/// <summary>
/// CybozuUsersControl.xaml の相互作用ロジック
/// https://cybozu.dev/ja/common/docs/user-api/users/
/// </summary>
public partial class UserCybozuControl : UserControl
{
    /// <summary>
    /// コンストラクタ
    /// </summary>
    public UserCybozuControl()
    {
        InitializeComponent();
        //
        _controlUser.ControlTableName = UserResponse.TableName(false);
        _controlCustomItem.ControlTableName = UserResponseCustomItem.TableName(false);
        _controlGroup.ControlTableName = UserGroupResponse.TableName(false);
        _controlServices.ControlTableName = UsersServiceResponse.TableName(false);
        _controlOrganizationTitle.ControlTableName = OrganizationTitleResponse.TableName(false);

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
            await UsersRequest.Instance.InsertAll( KintoneManager.CYBOZU_LIMIT, true);
            _controlUser.Load();
            var dataView = _controlUser.GridDataView;
            if (dataView != null)
            {
                int count = 0;
                progresssBarCount?.Invoke(count, dataView.Count, "Users");

                foreach (DataRowView row in dataView)
                {
                    var code = row[Resource.COLUMN_RESPONSE_CODE].ToString();

                    if (string.IsNullOrEmpty(code) == false)
                    {
                        await UserGroupsRequest.Instance.InsertAll(code, KintoneManager.CYBOZU_LIMIT, true);
                    }
                    progresssBarCount?.Invoke(++count);
                }
                //
            }
            //
            await UserServicesRequest.Instance.InsertAll(KintoneManager.CYBOZU_LIMIT, true);
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
        _controlUser.Load();
        _controlCustomItem.Load();
        _controlGroup.Load();
        _controlServices.Load();
        _controlOrganizationTitle.Load();
    }
}
