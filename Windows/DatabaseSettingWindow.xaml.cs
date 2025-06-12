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
/// DataBaseSettingWindow.xaml の相互作用ロジック
/// </summary>
public partial class DatabaseSettingWindow : Window
{
    public DatabaseSettingWindow()
    {
        InitializeComponent();
    }

    private void _loaded(object sender_, RoutedEventArgs e_)
    {
        LogFile.Instance.WriteLine(@"Load");
        //
    }

    private void _okClick(object sender_, RoutedEventArgs e_)
    {
        LogFile.Instance.WriteLine(@"Save");
        //
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
