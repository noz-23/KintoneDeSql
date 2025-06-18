/*
 * Kintone De Sql
 * Copyright (c) 2025 noz-23
 *  https://github.com/noz-23/
 * 
 * Licensed under the MIT License 
 * 
 */
using KintoneDeSql.Interface;
using KintoneDeSql.Properties;
using KintoneDeSql.Responses.Apps.Forms;
using System.Windows;
using System.Windows.Controls;

/// <summary>
/// https://cybozu.dev/ja/kintone/docs/rest-api/apps/form/get-form-layout/
/// </summary>
namespace KintoneDeSql.Controls.Apps.Forms;

/// <summary>
/// AppFormFieldControl.xaml の相互作用ロジック
/// </summary>
public partial class AppFormFieldControl : UserControl
{
    /// <summary>
    /// コンストラクタ
    /// </summary>
    public AppFormFieldControl()
    {
        InitializeComponent();
        //
        _fieldControl.ControlTableName = AppFormFieldResponse.TableName(false);
        _entityControl.ControlTableName = AppFormFieldResponseEntity.TableName(false);
        _optionControl.ControlTableName = AppFormFieldResponseOption.TableName(false);
        _displayFieldControl.ControlTableName = AppFormFieldResponseDisplayField.TableName(false);
        _fieldMappingControl.ControlTableName = AppFormFieldResponseFieldMapping.TableName(false);
    }

    /// <summary>
    /// App ID
    /// </summary>
    private string _appId  = string.Empty;

    /// <summary>
    /// Api Key
    /// </summary>
    private string _apiKey = string.Empty;

    /// <summary>
    /// 読み込み処理
    /// </summary>
    /// <param name="sender_"></param>
    /// <param name="e_"></param>
    private void _loaded(object sender_, RoutedEventArgs e_)
    {
        if (Window.GetWindow(this) is IAppTable win)
        {
            _appId = win.AppId;
            _apiKey = win.ApiKey;
            //
            _fieldControl.ControlWhere = $"WHERE {Resource.COLUMN_APP_ID}='{_appId}'";
            _entityControl.ControlWhere = $"WHERE {Resource.COLUMN_APP_ID}='{_appId}'";
            _optionControl.ControlWhere = $"WHERE {Resource.COLUMN_APP_ID}='{_appId}'";
            _displayFieldControl.ControlWhere = $"WHERE {Resource.COLUMN_APP_ID}='{_appId}'";
            _fieldMappingControl.ControlWhere = $"WHERE {Resource.COLUMN_APP_ID}='{_appId}'";
        }
    }
}
