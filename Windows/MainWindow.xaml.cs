/*
 * Kintone De Sql
 * Copyright (c) 2025 noz-23
 *  https://github.com/noz-23/
 * 
 * Licensed under the MIT License 
 * 
 */
using KintoneDeSql.Extensions;
using KintoneDeSql.Files;
using KintoneDeSql.Interface;
using KintoneDeSql.Managers;
using KintoneDeSql.Properties;
using KintoneDeSql.Views;
using System.Reflection;
using System.Windows;

namespace KintoneDeSql.Windows;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// datagridのヘッダに-_-を含むと正しく表示されない
/// https://resanaplaza.com/2021/04/17/%E3%80%90wpf%E3%80%91datagrid%E3%81%AE%E3%83%98%E3%83%83%E3%83%80%E3%81%AB-_-%E3%82%92%E5%90%AB%E3%82%80%E3%81%A8%E6%AD%A3%E3%81%97%E3%81%8F%E8%A1%A8%E7%A4%BA%E3%81%95%E3%82%8C%E3%81%AA%E3%81%84/
/// https://stackoverflow.com/questions/18227574/wpf-datagrid-with-recognizesaccesskey-turned-off
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void _kintoneSettingClick(object sender_, RoutedEventArgs e_)
    {
        var win = new KintoneSettingWindow();
        win.ShowDialog();

        KintoneManager.Instance.Domain(Settings.Default.KintoneDomain);
        KintoneManager.Instance.AccessAccount(Settings.Default.KintoneAccessId, Settings.Default.KintoneAccessPassword);
        KintoneManager.Instance.LoginAccount(Settings.Default.KintoneLoginId, Settings.Default.KintoneLoginPassword);

        _textBlock.Text = $"Access To {Settings.Default.KintoneDomain}.cybozu.com";

    }

    private void _databaseSettingClick(object sender_, RoutedEventArgs e_)
    {
        var win = new DatabaseSettingWindow();
        win.ShowDialog();

        _textBlock.Text = $"Access To {Settings.Default.KintoneDomain}.cybozu.com";
    }
    
    /// <summary>
    /// 読み込み処理
    /// </summary>
    /// <param name="sender_"></param>
    /// <param name="e_"></param>
    private void _loaded(object sender_, RoutedEventArgs e_)
    {
        init();
        LogFile.Instance.WriteLine($"Domein[{Settings.Default.KintoneDomain}.cybozu.com]");

        KintoneManager.Instance.Domain(Settings.Default.KintoneDomain);
        KintoneManager.Instance.AccessAccount(Settings.Default.KintoneAccessId, Settings.Default.KintoneAccessPassword);
        KintoneManager.Instance.LoginAccount(Settings.Default.KintoneLoginId, Settings.Default.KintoneLoginPassword);

        SettingManager.Instance.LoadView();

        _textBlock.Text = $"Access To {Settings.Default.KintoneDomain}.cybozu.com";

    }

    /// <summary>
    /// 保存処理
    /// </summary>
    /// <param name="sender_"></param>
    /// <param name="e_"></param>
    private void _closing(object sender_, System.ComponentModel.CancelEventArgs e_)
    {
        SettingManager.Instance.Save();
        SQLiteManager.Instance.Close();

    }

    /// <summary>
    /// 基本的なテーブル作成
    /// </summary>
    private void init()
    {
        SQLiteManager.Instance.CreateTable(typeof(AppTableView).TableName(false), typeof(AppTableView).ListCreateHeader(true));
        SQLiteManager.Instance.CreateTable(typeof(SubTableView).TableName(false), typeof(SubTableView).ListCreateHeader(true));
        //

        var _assembly = Assembly.GetExecutingAssembly();
        var listTable = _assembly.GetTypes()
                                    .Where(t_ => t_.GetInterfaces().Any(t_ => t_ == typeof(ICreateTable)) == true)
                                    .Distinct();
        foreach (var table in listTable)
        {
            var name = table.GetMethod(nameof(ICreateTable.TableName), BindingFlags.Static | BindingFlags.Public|BindingFlags.FlattenHierarchy)?.Invoke(null, new object[] { false });
            var list = table.GetMethod(nameof(ICreateTable.ListCreateHeader), BindingFlags.Static | BindingFlags.Public | BindingFlags.FlattenHierarchy)?.Invoke(null, new object[] { true });
            //
            if (name == null || list == null)
            {
                continue;
            }
            LogFile.Instance.WriteLine($"Name[{name}]");
            SQLiteManager.Instance.CreateTable(name as string ??string.Empty, list as List<string> ?? new ());
        }
    }
}