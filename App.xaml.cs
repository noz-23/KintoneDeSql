/*
 * Reprise Report Log Analyzer
 * Copyright (c) 2025 noz-23
 *  https://github.com/noz-23/
 * 
 * Licensed under the MIT License 
 * 
 */
using KintoneDeSql.Files;
using KintoneDeSql.Managers;
using System.Configuration;
using System.Data;
using System.Windows;

namespace KintoneDeSql;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private void _startup(object sender_, StartupEventArgs e_)
    {
        LogFile.Instance.Create();
        KintoneManager.Instance.Create();
    }
}
