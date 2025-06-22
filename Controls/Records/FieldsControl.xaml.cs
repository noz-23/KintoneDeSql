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
    }
}
