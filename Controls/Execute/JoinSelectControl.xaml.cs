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
using KintoneDeSql.Managers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace KintoneDeSql.Controls.Execute;

/// <summary>
/// JoinSelectControl.xaml の相互作用ロジック
/// 結合するテーブル
/// </summary>
public partial class JoinSelectControl : UserControl
{
    /// <summary>
    /// コンストラクタ
    /// </summary>
    public JoinSelectControl()
    {
        InitializeComponent();
        //
    }

    /// <summary>
    /// SQL作成処理
    /// </summary>

    public SetTextCallBack? SetText = null;

    /// <summary>
    ///  選択したカラム名
    /// </summary>
    public IList<string> ListSelect { get; private set; } = new List<string>();

    /// <summary>
    /// 選択テーブル名
    /// </summary>
    public string TableName 
    {
        get=> _tableComboBox.SelectedItem?.ToString() ?? string.Empty;
    }

    /// <summary>
    /// 読み込み処理
    /// </summary>
    /// <param name="sender_"></param>
    /// <param name="e_"></param>
    private void _loaded(object sender_, RoutedEventArgs e_)
    {
        _databaseComboBox.SelectedIndex = 0;
    }

    /// <summary>
    /// データベースのコンボボックス変更
    /// </summary>
    /// <param name="sender_"></param>
    /// <param name="e_"></param>
    private void _databaseComboBoxSelectionChanged(object sender_, SelectionChangedEventArgs e_)
    {
        var selectedItem = _databaseComboBox.SelectedValue?.ToString() ??string.Empty;
        LogFile.Instance.WriteLine($"Select: {selectedItem}");

        if (string.IsNullOrEmpty(selectedItem) == true)
        {
            return;
        }

        _dataGrid.ItemsSource = null;
        _tableComboBox.Items.Clear();

        switch (selectedItem)
        {
            case SQLiteManager.MAIN_DATABASE: SQLiteManager.Instance.ListTableName(SQLiteManager.MAIN_MASTER).ForEach(item_ => _tableComboBox.Items.Add(item_));break;
            case SQLiteManager.SUB_DATABASE: SQLiteManager.Instance.ListTableName(SQLiteManager.SUB_MASTER).ForEach(item_ => _tableComboBox.Items.Add(item_)); break;
            case SQLiteManager.FIELD_DATABASE: SQLiteManager.Instance.ListTableName(SQLiteManager.FIELD_MASTER).ForEach(item_ => _tableComboBox.Items.Add(item_)); break;
            case SQLiteManager.RECORD_DATABASE: SQLiteManager.Instance.ListTableName(SQLiteManager.RECORD_MASTER).ForEach(item_ => _tableComboBox.Items.Add(item_)); break;
            case SQLiteManager.SPACE_DATABASE: SQLiteManager.Instance.ListTableName(SQLiteManager.SPACE_MASTER).ForEach(item_ => _tableComboBox.Items.Add(item_)); break;
            case SQLiteManager.CYBOZU_DATABASE: SQLiteManager.Instance.ListTableName(SQLiteManager.CYBOZU_MASTER).ForEach(item_ => _tableComboBox.Items.Add(item_)); break;
            default:break;
        }
        //
        SetText?.Invoke();
    }

    /// <summary>
    /// テーブル名のコンボボックス変更
    /// </summary>
    /// <param name="sender_"></param>
    /// <param name="e_"></param>
    private void _tableComboBoxSelectionChanged(object sender_, SelectionChangedEventArgs e_)
    {
        var tableName = _tableComboBox.SelectedItem?.ToString() ?? string.Empty;
        if (string.IsNullOrEmpty(tableName) == true)
        {
            return;
        }
        var data = SQLiteManager.Instance.SelectTable(tableName, "LIMIT 10");
        //
        _dataGrid.ItemsSource = null;
        _dataGrid.ItemsSource = data.DefaultView;
        ListSelect.Clear();
        //
        SetText?.Invoke();
    }


    /// <summary>
    /// カラム名の選択処理(ソート処理の上書き)
    /// </summary>
    /// <param name="sender_"></param>
    /// <param name="e_"></param>
    private void _dataGridSorting(object sender_, DataGridSortingEventArgs e_)
    {
        e_.Handled = true;// ソートキャンセル

        LogFile.Instance.WriteLine($"Header: {e_.Column.Header.ToString()}");

        if (e_.Column.Header is HeaderColumnData header)
        {
            if (ListSelect.Contains(header.Column))
            {
                // 選択解除
                ListSelect.Remove(header.Column);
                e_.Column.HeaderStyle = null;
            }
            else
            {
                // 選択追加
                ListSelect.Add(header.Column);
                var style = new Style(typeof(DataGridColumnHeader));
                style.Setters.Add(new Setter(Control.BackgroundProperty, Brushes.LightSalmon));
                e_.Column.HeaderStyle = style;


            }
            e_.Column.Header = new HeaderColumnData(header.Column, ListSelect);
            if (sender_ is DataGrid dataGrid)
            {
                foreach (var col in dataGrid.Columns)
                {
                    // 減ったことを考慮して更新
                    if (col.Header is HeaderColumnData colHeader)
                    {
                        if (colHeader.IsSelect == true)
                        {
                            col.Header = new HeaderColumnData(colHeader.Column, ListSelect);
                        }
                    }
                }
            }
        }
        SetText?.Invoke();
    }
}
