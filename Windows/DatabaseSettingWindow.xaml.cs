/*
 * Kintone De Sql
 * Copyright (c) 2025 noz-23
 *  https://github.com/noz-23/
 * 
 * Licensed under the MIT License 
 * 
 */
using KintoneDeSql.Files;
using KintoneDeSql.Properties;
using System.Windows;
using System.Windows.Controls;

namespace KintoneDeSql.Windows;

/// <summary>
/// DataBaseSettingWindow.xaml の相互作用ロジック
/// </summary>
public partial class DatabaseSettingWindow : Window
{
    /// <summary>
    /// コンストラクタ
    /// </summary>
    public DatabaseSettingWindow()
    {
        InitializeComponent();
    }

    /// <summary>
    /// 読み込み処理
    /// </summary>
    /// <param name="sender_"></param>
    /// <param name="e_"></param>
    private void _loaded(object sender_, RoutedEventArgs e_)
    {
        LogFile.Instance.WriteLine(@"Load");
        //
        _creatorCheckBox.IsChecked = Settings.Default.IsCreatorExtract;
        _setCheckedRadio(_creatorStackPanel.Children, Settings.Default.CreatorPrimary);
        //
        _modifierCheckBox.IsChecked = Settings.Default.IsModifierExtract;
        _setCheckedRadio(_modifierStackPanel.Children, Settings.Default.ModifierPrimary);
        //
        _entityCheckBox.IsChecked = Settings.Default.IsEntityExtract;
        _setCheckedRadio(_entityStackPanel.Children, Settings.Default.EntityPrimary);
        //
        _setCheckedRadio(_attachedAppStackPanel.Children, Settings.Default.AttachedAppPrimary);
        //
        _setCheckedRadio(_fileStackPanel.Children, Settings.Default.FilePrimary);
        //
    }

    /// <summary>
    /// OKボタン押下
    /// </summary>
    /// <param name="sender_"></param>
    /// <param name="e_"></param>
    private void _okClick(object sender_, RoutedEventArgs e_)
    {
        LogFile.Instance.WriteLine(@"Save");
        //
        Settings.Default.IsCreatorExtract = _creatorCheckBox.IsChecked ??true;
        Settings.Default.CreatorPrimary = _getCheckedRadio(_creatorStackPanel.Children);
        //
        Settings.Default.IsModifierExtract = _modifierCheckBox.IsChecked ?? true;
        Settings.Default.ModifierPrimary = _getCheckedRadio(_modifierStackPanel.Children);
        //
        Settings.Default.IsEntityExtract = _entityCheckBox.IsChecked ?? true;
        Settings.Default.EntityPrimary = _getCheckedRadio(_entityStackPanel.Children);
        //
        Settings.Default.AttachedAppPrimary = _getCheckedRadio(_attachedAppStackPanel.Children);
        //
        Settings.Default.FilePrimary = _getCheckedRadio(_fileStackPanel.Children);
        //
        Settings.Default.Save();

        Close();
    }

    /// <summary>
    /// Cancelボタン押下
    /// </summary>
    /// <param name="sender_"></param>
    /// <param name="e_"></param>
    private void _cancelClick(object sender_, RoutedEventArgs e_)
    {
        LogFile.Instance.WriteLine(@"Cancel");
        //
        Close();
    }


    /// <summary>
    /// ラジオボタンのチェック状態設定
    /// </summary>
    /// <param name="children_"></param>
    /// <param name="primary_"></param>
    private void _setCheckedRadio(UIElementCollection children_, string primary_)
    {
        foreach (var child in children_)
        {
            if (child is RadioButton radio)
            {
                radio.IsChecked = (radio.Tag.ToString() == primary_);
            }
        }
    }

    /// <summary>
    /// ラジオボタンのチェック状態取得
    /// </summary>
    /// <param name="children_"></param>
    /// <returns></returns>
    private string _getCheckedRadio(UIElementCollection children_)
    {
        foreach (var child in children_)
        {
            if (child is RadioButton radio)
            {
                if (radio.IsChecked == true)
                {
                    return radio.Tag?.ToString() ?? string.Empty;
                }
            }
        }
        return string.Empty;
    }
}
