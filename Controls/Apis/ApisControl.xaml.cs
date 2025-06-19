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
using System.Net.Http;
using System.Windows;
using System.Windows.Controls;

namespace KintoneDeSql.Controls.Apis;

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
        //
        _controlApi.ControlTableName = ApiGetResponse.TableName(false);
        _controlSchema.ControlTableName = ApiSchemaResponse.TableName(false);
        _controlProperty.ControlTableName = ApiSchemaResponseProperty.TableName(false);
        _controlRequired.ControlTableName = ApiSchemaResponseRequired.TableName(false);
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
        LogFile.Instance.WriteLine($"Get");
        //
        var win = new WaitWindow();
        _progresssBarCount = win.ProgressCount;
        //
        win.Run = async () =>
        {
            var response = await ApisGetRequest.Instance.Insert();
            //return await _getSchema(response);
            await _getSchema(response);
        };
        //
        win.ShowDialog();
        _progresssBarCount = null;
        //
        _loadDatabase();
    }

    /// <summary>
    /// Api Schema 情報の取得処理
    /// </summary>
    /// <param name="response_"></param>
    /// <returns></returns>
    private async Task<int> _getSchema(ApiGetResponse? response_)
    {
        LogFile.Instance.WriteLine($"Apis [{response_?.ListApi.Count}]");
        //
        int count = 0;
        if (response_ != null)
        {
            _progresssBarCount?.Invoke(count, response_.ListApi.Count, "API Schema");
            //
            foreach (var api in response_.ListApi)
            {
                var response = await KintoneManager.Instance.KintoneGet<ApiSchemaResponse?>(HttpMethod.Get, api.Value.Link);
                if (response != null)
                {
                    SQLiteManager.Instance.InsertTable(ApiSchemaResponse.TableName(false), ApiSchemaResponse.ListInsertHeader(true), response.ListInsertValue(true));
                    SQLiteManager.Instance.InsertTable(ApiSchemaResponseRequired.TableName(false), ApiSchemaResponseRequired.ListInsertHeader(true), response.ListInsertValueRequired(true));
                    SQLiteManager.Instance.InsertTable(ApiSchemaResponseProperty.TableName(false), ApiSchemaResponseProperty.ListInsertHeader(true), response.ListInsertValueProperty(true));
                }
                //
                _progresssBarCount?.Invoke(++count);
            }
        }
        return count;
    }

    /// <summary>
    /// 再読み込み
    /// </summary>
    private void _loadDatabase()
    {
        _controlApi.Load();
        _controlSchema.Load();
        _controlProperty.Load();
        _controlRequired.Load();
    }
}
