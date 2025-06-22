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
using KintoneDeSql.Responses.Cybozu.Groups;
using KintoneDeSql.Windows;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace KintoneDeSql.Controls.Cybozu;

/// <summary>
/// CybozuGroupsControl.xaml の相互作用ロジック
/// https://cybozu.dev/ja/common/docs/user-api/groups/
/// </summary>
public partial class GroupCybozuControl : UserControl
{
    /// <summary>
    /// コンストラクタ
    /// </summary>
    public GroupCybozuControl()
    {
        InitializeComponent();
        //
        _controlGroup.ControlTableName = GroupResponse.TableName(false);
        _controlUser.ControlTableName = GroupUserResponse.TableName(false);
    }

    /// <summary>
    /// プログレスバー処理
    /// </summary>
    //public WaitWindow.ProgressCountCallBack? _progresssBarCount = null;

    /// <summary>
    /// 読み込み表示
    /// </summary>
    /// <param name="sender_"></param>
    /// <param name="e_"></param>
    private  void _loaded(object sender_, RoutedEventArgs e_)
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
            await GroupsRequest.Instance.InsertAll(KintoneManager.CYBOZU_LIMIT, true);
            _controlGroup.Load();
            var dataView = _controlGroup.GridDataView;
            if (dataView != null)
            {
                int count = 0;
                progresssBarCount?.Invoke(count, dataView.Count, "Groups");

                foreach (DataRowView row in dataView)
                {
                    var code = row[Resource.COLUMN_RESPONSE_CODE].ToString();

                    if (string.IsNullOrEmpty(code) == false)
                    {
                        await GroupUsersRequest.Instance.InsertAll(code, KintoneManager.CYBOZU_LIMIT, true);
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
        _controlGroup.Load();
        _controlUser.Load();
    }
}
