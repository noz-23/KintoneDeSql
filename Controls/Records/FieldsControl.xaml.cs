/*
 * Kintone De Sql
 * Copyright (c) 2025 noz-23
 *  https://github.com/noz-23/
 * 
 * Licensed under the MIT License 
 * 
 */
using KintoneDeSql.Responses.Records;
using System.Windows;
using System.Windows.Controls;

namespace KintoneDeSql.Controls.Records;

/// <summary>
/// FieldsControl.xaml の相互作用ロジック
/// https://cybozu.dev/ja/kintone/docs/overview/field-types/
/// </summary>
public partial class FieldsControl : UserControl
{
    /// <summary>
    /// コンストラクタ
    /// </summary>
    public FieldsControl()
    {
        InitializeComponent();
        //
        _userControl.ControlTableName= UserValueList.TableName(false);
        _organizationControl.ControlTableName = OrganizationValueList.TableName(false);
        _groupControl.ControlTableName = GroupValueList.TableName(false);
        _fileControl.ControlTableName = FileFieldList.TableName(false);
        _checkBoxControl.ControlTableName = CheckBoxValueList.TableName(false);
        _multiSelectControl.ControlTableName = MultiSelectValueList.TableName(false);
        _categoryControl.ControlTableName = CategoryBoxValueList.TableName(false);
    }

    /// <summary>
    /// 読み込み表示
    /// </summary>
    /// <param name="sender_"></param>
    /// <param name="e_"></param>
    private void _loaded(object sender_, RoutedEventArgs e_)
    {
        //await _loadDatabase();
    }

    /// <summary>
    /// カラム名のアンダースコア表示対応
    /// </summary>
    /// <param name="sender_"></param>
    /// <param name="e_"></param>
    //private void _autoGeneratingColumn(object sender_, DataGridAutoGeneratingColumnEventArgs e_)
    //{
    //    string header = e_.Column.Header?.ToString()??string.Empty;
    //    e_.Column.Header = header.Replace("_", "__");
    //}

    /// <summary>
    /// データベース読み込み
    /// </summary>
    /// <returns></returns>
    //private async Task _loadDatabase()
    //{
    //    _dataGridUsers.ItemsSource = null;
    //    _dataGridUsers.ItemsSource = (await SQLiteManager.Instance.SelectTable(UserValueList.TableName(false))).DefaultView;
    //    //
    //    _dataGridOrganizations.ItemsSource = null;
    //    _dataGridOrganizations.ItemsSource = (await SQLiteManager.Instance.SelectTable(OrganizationValueList.TableName(false))).DefaultView;
    //    //
    //    _dataGridGroups.ItemsSource = null;
    //    _dataGridGroups.ItemsSource = (await SQLiteManager.Instance.SelectTable(GroupValueList.TableName(false))).DefaultView;
    //    //
    //    _dataGridFiles.ItemsSource = null;
    //    _dataGridFiles.ItemsSource = (await SQLiteManager.Instance.SelectTable(FileValueList.TableName(false))).DefaultView;
    //    //
    //    _dataGridCheckBoxes.ItemsSource = null;
    //    _dataGridCheckBoxes.ItemsSource = (await SQLiteManager.Instance.SelectTable(CheckBoxValueList.TableName(false))).DefaultView;
    //    //
    //    _dataGridMultiSelectes.ItemsSource = null;
    //    _dataGridMultiSelectes.ItemsSource = (await SQLiteManager.Instance.SelectTable(MultiSelectValueList.TableName(false))).DefaultView;
    //    //
    //    _dataGridCategries.ItemsSource = null;
    //    _dataGridCategries.ItemsSource = (await SQLiteManager.Instance.SelectTable(CategoryBoxValueList.TableName(false))).DefaultView;

    //}
}
