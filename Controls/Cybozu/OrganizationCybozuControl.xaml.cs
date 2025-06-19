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
    /// プログレスバー処理
    /// </summary>
    //public WaitWindow.ProgressCountCallBack? _progresssBarCount = null;

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

        var win = new WaitWindow();
        var progresssBarCount = win.ProgressCount;
        win.Run = async () =>
        {
            //var offset = 0;
            //var count = 0;
            ////
            //do
            //{
            //    var response = await OrganizationsRequest.Instance.Insert(offset, _LIMIT, true);
            //    if (response == null)
            //    {
            //        break;
            //    }
            //    if (response.ListOrganization.Count == 0)
            //    {
            //        break;
            //    }
            //    //
            //    count = response.ListOrganization.Count;
            //    offset += count;

            //    _userTitleInsert(response.ListOrganization);
            //} while (count == _LIMIT);
            //
            //return offset;

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
                //
                //_controlUserTitle.Load();
            }
        };
        //
        win.ShowDialog();
        //_progresssBarCount = null;
        _loadDatabase();
    }

    /// <summary>
    /// 組織に所属するユーザ取得
    /// </summary>
    /// <param name="list_"></param>
    //private async void _userTitleInsert(IList<OrganizationValue> list_)
    //{
    //    const int _LIMIT = KintoneManager.CYBOZU_LIMIT;

    //    int orgCount = 0;
    //    _progresssBarCount?.Invoke(orgCount, list_.Count, "Organizations");
    //    foreach (var organization in list_)
    //    {
    //        var offset = 0;
    //        var count = 0;

    //        do
    //        {
    //            var response = await UserTitlesRequest.Instance.Insert(organization.Code, offset, _LIMIT, true);
    //            if (response == null)
    //            {
    //                break;
    //            }
    //            if (response.ListUserTitle.Count == 0)
    //            {
    //                break;
    //            }
    //            //
    //            count = response.ListUserTitle.Count;
    //            offset += count;
    //        } while (count == _LIMIT);
    //        _progresssBarCount?.Invoke(++orgCount);
    //    }
    //}

    /// <summary>
    /// 再読み込み
    /// </summary>
    private void _loadDatabase()
    {
        _controlOrganization.Load();
        _controlUserTitle.Load();
    }
}
