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
using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace KintoneDeSql.Windows;

/// <summary>
/// SettingWindow.xaml の相互作用ロジック
/// </summary>
public partial class KintoneSettingWindow : Window
{
    public KintoneSettingWindow()
    {
        LogFile.Instance.WriteLine(@"START");
        InitializeComponent();
    }
    private void _loaded(object sender_, RoutedEventArgs e_)
    {
        LogFile.Instance.WriteLine(@"Load");
        //
        _domain.Text = Settings.Default.KintoneDomain;
        _accessId.Text = Settings.Default.KintoneAccessId;
        _accessPassword.Text = Settings.Default.KintoneAccessPassword;
        _kintoneId.Text = Settings.Default.KintoneLoginId;
        _kintonePassword.Text = Settings.Default.KintoneLoginPassword;
        _kintoneApiToken.Text = Settings.Default.KintoneApiToken;
        _proxyAddress.Text = Settings.Default.ProxyAddress;
        _proxyUser.Text = Settings.Default.ProxyUser;
        _proxyPassword.Text = Settings.Default.ProxyPassword;
    }

    private void _okClick(object sender_, RoutedEventArgs e_)
    {
        LogFile.Instance.WriteLine(@"Save");
        //
        Settings.Default.KintoneDomain = _domain.Text;
        Settings.Default.KintoneAccessId = _accessId.Text;
        Settings.Default.KintoneAccessPassword = _accessPassword.Text;
        Settings.Default.KintoneLoginId = _kintoneId.Text;
        Settings.Default.KintoneLoginPassword = _kintonePassword.Text;
        Settings.Default.KintoneApiToken = _kintoneApiToken.Text;
        Settings.Default.ProxyAddress = _proxyAddress.Text;
        Settings.Default.ProxyUser = _proxyUser.Text;
        Settings.Default.ProxyPassword = _proxyPassword.Text;
        //
        Settings.Default.Save();

        Close();
    }

    private void _cancelClick(object sender_, RoutedEventArgs e_)
    {
        LogFile.Instance.WriteLine(@"Cancel");
        //
        Close();
    }
}
