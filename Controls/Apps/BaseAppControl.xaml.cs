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
using KintoneDeSql.Requests;
using KintoneDeSql.Requests.Apps.Forms;
using KintoneDeSql.Views;
using KintoneDeSql.Windows;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace KintoneDeSql.Controls.Apps;

/// <summary>
/// BaseAppControl.xaml の相互作用ロジック
/// </summary>
public partial class BaseAppControl : UserControl, INotifyPropertyChanged
{
    /// <summary>
    /// コンストラクタ
    /// </summary>
    public BaseAppControl()
    {
        InitializeComponent();
        _gridRow.Height = new GridLength(0);
    }

    #region INotifyPropertyChanged

    public event PropertyChangedEventHandler? PropertyChanged;
    private void _notifyPropertyChanged([CallerMemberName] string propertyName_ = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName_));
    }
    #endregion

    /// <summary>
    ///  取得時間
    /// </summary>
    public DateTime? ControlDateTime
    {
        get => _getTime();
        set
        {
            _setTime(value);
            _notifyPropertyChanged();
        }
    }

    /// <summary>
    /// メインテーブル(上)
    /// </summary>
    public string ControlMainTableName
    {
        get => _mainControl?.ControlTableName??string.Empty;
        set => _mainControl.ControlTableName = value;
    }

    /// <summary>
    /// サブテーブル(下)
    /// </summary>
    public virtual string ControlSubTableName
    {
        get => _subControl?.ControlTableName ?? string.Empty;
        set
        {
            _subControl.ControlTableName = value;

            if (string.IsNullOrEmpty(_subControl.ControlTableName) == false)
            {
                // サブがある場合下を表示
                _gridRow.Height = GridLength.Auto;
            }
        }
    }

    /// <summary>
    /// プログレスバー処理
    /// </summary>
    protected WaitWindow.ProgressCountCallBack? _ProgressCount { get; set; } = null;

    /// <summary>
    /// App ID
    /// </summary>
    private string _appId = string.Empty;

    /// <summary>
    /// Api Key
    /// </summary>
    private string _apiKey = string.Empty;

    private string _controlAppWhere { get => $"WHERE {Resource.COLUMN_APP_ID}='{_appId}'"; }
    private string _controlTimeWhere{ get => $"WHERE name='{ControlMainTableName}' AND {Resource.COLUMN_SUB_TABLE_ID}='{_appId}'"; }

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
            _apiKey=win.ApiKey;
            //
            _mainControl.ControlWhere = _controlAppWhere;
            _subControl.ControlWhere = _controlAppWhere;
        }

        _loadDatabase();
        _notifyPropertyChanged(nameof(ControlDateTime));
    }

    /// <summary>
    /// Getボタン押下
    /// </summary>
    /// <param name="sender_"></param>
    /// <param name="e_"></param>
    private void _getClick(object sender_, RoutedEventArgs e_)
    {
        if (_mainControl.Count == 0)
        {
            var win = new WaitWindow();
            _ProgressCount = win.ProgressCount;

            win.Run = async () => await ControlInsert(_appId, _apiKey);

            win.ShowDialog();
            _ProgressCount = null;

            ControlDateTime = DateTime.Now;
            LogFile.Instance.WriteLine($"{ControlMainTableName} {ControlDateTime}");
        }

        _loadDatabase();
    }

    /// <summary>
    /// フォーカス処理
    /// </summary>
    /// <param name="sender_"></param>
    /// <param name="e_"></param>
    private void _gotFocus(object sender_, RoutedEventArgs e_)
    {
        _loadDatabase();
    }

    /// <summary>
    /// 挿入処理
    /// </summary>
    /// <param name="appId_"></param>
    /// <returns></returns>
    public virtual async Task<int> ControlInsert(string appId_, string apiKey_)
    {
        await Task.Delay(100);
        return 0;
    }

    /// <summary>
    /// 再読み込み
    /// </summary>
    private void _loadDatabase()
    {
        if (string.IsNullOrEmpty(ControlMainTableName) == false)
        {
            _mainControl.Load();
        }
        if (string.IsNullOrEmpty(ControlSubTableName) == false)
        {
            _subControl.Load();
        }
    }

    /// <summary>
    /// 挿入時間取得
    /// </summary>
    /// <returns></returns>
    private DateTime? _getTime()
    {
        var list = SQLiteManager.Instance.SelectTable<TimeView>(false, _controlTimeWhere);

        if (list.Any() == true)
        {
            return list.FirstOrDefault()?.Time;
        }

        return null;
    }

    /// <summary>
    /// 挿入時間保存
    /// </summary>
    /// <param name="time_"></param>
    private  void _setTime(DateTime? time_)
    {
        if (time_ == null)
        {
            return;
        }

        var view = new TimeView()
        {
            Name = ControlMainTableName,
            Id = _appId,
            Time = time_
        };
        SQLiteManager.Instance.InsertTable(typeof(TimeView).TableName(false), typeof(TimeView).ListInsertHeader(true), new List<IList<string>>() { view.ListValue(true) });
    }
}
