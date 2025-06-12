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
using KintoneDeSql.Requests.Apis;
using KintoneDeSql.Responses.Apis;
using KintoneDeSql.Windows;
using System.Data;
using System.Net.Http;
using System.Windows;
using System.Windows.Controls;

namespace KintoneDeSql.Controls;

/// <summary>
/// ApisControl.xaml の相互作用ロジック
/// API情報
/// https://cybozu.dev/ja/kintone/docs/rest-api/apis/
/// </summary>
public partial class ApisControl : UserControl
{
    /// <summary>
    /// コンストラクタ
    /// </summary>
    public ApisControl()
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
        if (_dataGridApi.ItemsSource is DataView view)
        {
            if (view.Count == 0)
            {
                var win = new WaitWindow();
                _progresssBarCount = win.ProgressCount;
                //
                win.Run = async () =>
                {
                    var response = await ApisRequest.Instance.Insert();
                    LogFile.Instance.WriteLine($"[{response}]");

                    return await _getSchema( response);
                };
                //
                win.ShowDialog();
                _progresssBarCount = null;
            }
        }

        await _loadDatabase();
    }

    /// <summary>
    /// データベースの読み込み
    /// </summary>
    /// <returns></returns>
    private async Task _loadDatabase()
    {
        _dataGridApi.ItemsSource = null;
        _dataGridApi.ItemsSource = (await SQLiteManager.Instance.SelectTable(ListApiResponse.TableName(false)))?.DefaultView;
        //
        _dataGridSchema.ItemsSource = null;
        _dataGridSchema.ItemsSource = (await SQLiteManager.Instance.SelectTable(ApiSchemaResponse.TableName(false)))?.DefaultView;
    }

    /// <summary>
    /// Api Schema 情報の取得処理
    /// </summary>
    /// <param name="dataTable_"></param>
    /// <returns></returns>
    private async Task<int> _getSchema(ListApiResponse? list_)
    {
        if (list_ == null)
        {
            return 0;
        }
        //
        int count = 0;
        _progresssBarCount?.Invoke(count, list_.ListApi.Count, "API Schema");
        //
        foreach (var api in list_.ListApi)
        {
            var response = await KintoneManager.Instance.KintoneGet<ApiSchemaResponse?>(HttpMethod.Get, api.Value.Link);
            if (response == null)
            {
                continue;
            }
            SQLiteManager.Instance.InsertTable(ApiSchemaResponse.TableName(false), ApiSchemaResponse.ListInsertHeader(true), response.ListInsertValue(true));
            _progresssBarCount?.Invoke(++count);
        }
        return count;
    }
}
