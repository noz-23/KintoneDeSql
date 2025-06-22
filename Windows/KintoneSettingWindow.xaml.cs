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

namespace KintoneDeSql.Windows;

/// <summary>
/// SettingWindow.xaml の相互作用ロジック
/// </summary>
public partial class KintoneSettingWindow : Window
{
    /// <summary>
    /// コンストラクタ
    /// </summary>
    public KintoneSettingWindow()
    {
        LogFile.Instance.WriteLine(@"Start");
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
        _domain.Text = Settings.Default.KintoneDomain;
        _accessId.Text = Settings.Default.KintoneAccessId;
        _accessPassword.Text = Settings.Default.KintoneAccessPassword;
        _kintoneId.Text = Settings.Default.KintoneLoginId;
        _kintonePassword.Text = Settings.Default.KintoneLoginPassword;
        _proxyAddress.Text = Settings.Default.ProxyAddress;
        _proxyUser.Text = Settings.Default.ProxyUser;
        _proxyPassword.Text = Settings.Default.ProxyPassword;
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
        Settings.Default.KintoneDomain = _domain.Text;
        Settings.Default.KintoneAccessId = _accessId.Text;
        Settings.Default.KintoneAccessPassword = _accessPassword.Text;
        Settings.Default.KintoneLoginId = _kintoneId.Text;
        Settings.Default.KintoneLoginPassword = _kintonePassword.Text;
        Settings.Default.ProxyAddress = _proxyAddress.Text;
        Settings.Default.ProxyUser = _proxyUser.Text;
        Settings.Default.ProxyPassword = _proxyPassword.Text;
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
}
