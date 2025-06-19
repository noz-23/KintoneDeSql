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
        //const int _LIMIT = KintoneManager.CYBOZU_LIMIT;
        //const int _MAX = 1;

        var win = new WaitWindow();
        var progresssBarCount = win.ProgressCount;
        win.Run = async () =>
        {
            //var offset = 0;
            //var count = 0;
            ////
            //do
            //{
            //    var response = await UsersRequest.Instance.Insert(offset, _LIMIT, true);
            //    if (response == null)
            //    {
            //        break;
            //    }
            //    if (response.ListUser.Count == 0)
            //    {
            //        break;
            //    }
            //    //
            //    count = response.ListUser.Count;
            //    offset += count;

            //    _userGroupInsert(response.ListUser);
            //} while (offset == _LIMIT);
            //
            //return offset;
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
        //win.Run += async () =>
        //{
        //    var offset = 0;
        //    var count = 0;

        //    var pluginCount = 0;
        //    _progresssBarCount?.Invoke(pluginCount, _MAX, "User Services");
        //    do
        //    {
        //        var response = await UserServicesRequest.Instance.Insert(offset, _LIMIT, true);

        //        if (response == null)
        //        {
        //            break;
        //        }
        //        if (response.ListUser.Count == 0)
        //        {
        //            break;
        //        }
        //        count = response.ListUser.Count;
        //        offset += count;
        //    } while (count == offset);
        //    _progresssBarCount?.Invoke(++pluginCount);
        //    //return 1;
        //};
        //
        win.ShowDialog();
        //_progresssBarCount = null;
        _loadDatabase();
    }

    /// <summary>
    /// ユーザーが所属するグループ情報の取得
    /// </summary>
    /// <param name="list_"></param>
    //private async void _userGroupInsert(IList<UserValue> list_)
    //{
    //    const int _LIMIT = KintoneManager.CYBOZU_LIMIT;

    //    var userCount = 0;
    //    _progresssBarCount?.Invoke(userCount, list_.Count, "Users");
    //    foreach (var user in list_)
    //    {
    //        var offset = 0;
    //        var count = 0;
    //        //
    //        var responseOrg = await OrganizationTitlesRequest.Instance.Insert(user.Code,true);
    //        do
    //        {
    //            var responseUsr = await UserGroupsRequest.Instance.Insert(user.Code,offset, _LIMIT, true);
    //            if (responseUsr == null)
    //            {
    //                break;
    //            }
    //            if (responseUsr.ListGroup.Count == 0)
    //            {
    //                break;
    //            }
    //            //
    //            count = responseUsr.ListGroup.Count;
    //            offset += count;
    //        } while (count == offset);
    //        _progresssBarCount?.Invoke(++userCount);
    //    }
    //}

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
