/*
 * Kintone De Sql
 * Copyright (c) 2025 noz-23
 *  https://github.com/noz-23/
 * 
 * Licensed under the MIT License 
 * 
 */
using KintoneDeSql.Data;
using KintoneDeSql.Extensions;
using KintoneDeSql.Files;
using KintoneDeSql.Views;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace KintoneDeSql.Managers;

internal class SettingManager : BaseSingleton<SettingManager>,INotifyPropertyChanged
{
    #region INotifyPropertyChanged
    public event PropertyChangedEventHandler? PropertyChanged;
    public void NotifyPropertyChanged([CallerMemberName] string propertyName_ = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName_));
    }
    #endregion

    /// <summary>
    /// メインのアプリテーブル情報
    /// </summary>
    public ObservableCollection<AppTableView> ListAppTableView { get; set; } = new();
    /// <summary>
    /// サブテーブル情報
    /// </summary>
    public ObservableCollection<SubTableView> ListSubTableView { get; set; } = new();

    /// <summary>
    /// 初期化
    /// </summary>
    public void Create()
    {
        LogFile.Instance.WriteLine(@"Start");
    }

    /// <summary>
    /// ビューデータの読み込み
    /// </summary>
    public void LoadView()
    {
        ListAppTableView.Clear();
        ListAppTableView.AddRange(SQLiteManager.Instance.SelectTable<AppTableView>(false));

        ListSubTableView.Clear();
        ListSubTableView.AddRange(SQLiteManager.Instance.SelectTable<SubTableView>(false));
    }

    /// <summary>
    /// ビューデータの保存
    /// </summary>
    public void Save()
    {
        SQLiteManager.Instance.InsertTable(typeof(AppTableView).TableName(false), typeof(AppTableView).ListInsertHeader(true), ListAppTableView.ToList().Select(x_ => x_.ListValue(true)));
        SQLiteManager.Instance.InsertTable(typeof(SubTableView).TableName(false), typeof(SubTableView).ListInsertHeader(true), ListSubTableView.ToList().Select(x_ => x_.ListValue(true)));
    }

    
    /// <summary>
    /// AppID からテーブル名取得
    /// </summary>
    /// <param name="appId_"></param>
    /// <returns></returns>
    public string TableName(string appId_)=> AppView(appId_)?.TableName ?? $"Table_{appId_}";

    /// <summary>
    /// テーブル名重複判断
    /// </summary>
    /// <param name="tableName_"></param>
    /// <returns></returns>
    public bool ExistsTableName(string tableName_)=>ListAppTableView.ToList().Exists(x_ => x_.TableName == tableName_);

    /// <summary>
    /// AppIDから項目取得
    /// </summary>
    /// <param name="appId_"></param>
    /// <returns></returns>
    public AppTableView? AppView(string appId_) => ListAppTableView.ToList().Find(x_ => x_.AppId == appId_);
}
