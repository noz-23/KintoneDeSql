/*
 * Kintone De Sql
 * Copyright (c) 2025 noz-23
 *  https://github.com/noz-23/
 * 
 * Licensed under the MIT License 
 * 
 */
using KintoneDeSql.Managers;
using KintoneDeSql.Requests.Spaces;
using KintoneDeSql.Responses.Spaces;
using KintoneDeSql.Windows;
using System.Windows;
using System.Windows.Controls;

namespace KintoneDeSql.Controls.Spaces;

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
        //
        _spaceControl.ControlTableName = SpaceResponse.TableName(false);
        _attachedAppControl.ControlTableName = SpaceResponseAttachedApps.TableName(false);
        _staticControl.ControlTableName = SpaceStatisticResponse.TableName(false);
        _memberControl.ControlTableName = SpaceMemberResponse.TableName(false);
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

        // 全スペースを取得するAPIがないため
        var spaceMax = Int32.Parse(_textBox.Text);
        win.Run = async () =>
        {
            var count = 0;
            progresssBarCount?.Invoke(0, spaceMax, "Spaces");
            //
            for (var i = 1; i < spaceMax; i++)
            {
                var response = await SpacesRequest.Instance.Insert($"{i}",false);                //
                var responseMember = await SpacesMemberRequest.Instance.Insert($"{i}", false);

                progresssBarCount?.Invoke(++count);

            }

            await SpacesStatisticsRequest.Instance.InsertAll(KintoneManager.CYBOZU_LIMIT, false);
        };
        //
        win.ShowDialog();
        _loadDatabase();
    }

    /// <summary>
    /// データベース読み込み
    /// </summary>
    /// <returns></returns>
    private void _loadDatabase()
    {
        _spaceControl.Load();
        _attachedAppControl.Load();
        _staticControl.Load();
        _memberControl.Load();
    }
}
