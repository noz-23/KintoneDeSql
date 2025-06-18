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
using KintoneDeSql.Responses.Cybozu.Groups;
using KintoneDeSql.Windows;
using System.Windows;
using System.Windows.Controls;

namespace KintoneDeSql.Controls.Cybozu;

/// <summary>
/// CybozuGroupsControl.xaml の相互作用ロジック
/// https://cybozu.dev/ja/common/docs/user-api/groups/
/// </summary>
public partial class GroupListControl : UserControl
{
    /// <summary>
    /// コンストラクタ
    /// </summary>
    public GroupListControl()
    {
        InitializeComponent();
        //
        _controlGroup.ControlTableName = GroupResponse.TableName(false);
        _controlUser.ControlTableName = GroupUserResponse.TableName(false);
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
        const int _LIMIT = KintoneManager.CYBOZU_LIMIT;

        var win = new WaitWindow();
        _progresssBarCount = win.ProgressCount;
        win.Run = async () =>
        {
            var count = 0;
            var offset = 0;
            //
            do
            {
                var response = await GroupsRequest.Instance.Insert(offset, _LIMIT, true);
                if (response == null)
                {
                    break;
                }
                if (response.ListGroup.Count == 0)
                {
                    break;
                }
                //
                count = response.ListGroup.Count;
                offset += count;

                _groupUserInsert(response.ListGroup);
            } while (count == _LIMIT);
            //
            return offset;
        };
        //
        win.ShowDialog();
        _loadDatabase();
    }

    /// <summary>
    /// グループユーザ情報の挿入
    /// </summary>
    /// <param name="list_"></param>
    private async void _groupUserInsert(IList<GroupValue> list_)
    {
        const int _LIMIT = KintoneManager.CYBOZU_LIMIT;

        var groupCount = 0;
        _progresssBarCount?.Invoke(groupCount, list_.Count, "Groups");
        foreach (var group in list_)
        {
            var offset = 0;
            var count = 0;

            do
            {
                var response = await GroupUsersRequest.Instance.Insert(group.Code,offset, _LIMIT,true);
                if (response == null)
                {
                    break;
                }
                if (response.ListUser.Count == 0)
                {
                    break;
                }
                //
                count = response.ListUser.Count;
                offset += count;
            } while (count == _LIMIT);
            _progresssBarCount?.Invoke(++groupCount);
        }
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
