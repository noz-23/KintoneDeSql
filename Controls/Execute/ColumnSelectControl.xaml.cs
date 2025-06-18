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
using KintoneDeSql.Managers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace KintoneDeSql.Controls.Execute;

/// <summary>
/// ColumnSelectControl.xaml の相互作用ロジック
/// 各データベースのカラム選択表示
/// </summary>
public partial class ColumnSelectControl : UserControl
{
    /// <summary>
    /// コンストラクタ
    /// </summary>
    public ColumnSelectControl()
    {
        InitializeComponent();
    }

    /// <summary>
    /// テーブル名
    /// </summary>
    private string _tableName = string.Empty;

    /// <summary>
    /// 上位のテキストへコピー処理
    /// </summary>
    public PaseteCallBack? Paste = null;

    /// <summary>
    /// 読み込み処理
    /// </summary>
    /// <param name="sender_"></param>
    /// <param name="e_"></param>
    private void _loaded(object sender_, RoutedEventArgs e_)
    {
        _load();
    }

    /// <summary>
    /// テーブル名のダブルクリック
    /// </summary>
    /// <param name="sender_"></param>
    /// <param name="e_"></param>
    private void _mouseDoubleClick(object sender_, System.Windows.Input.MouseButtonEventArgs e_)
    {
        if (sender_ is ListBox listbox)
        {
            var positon = e_.GetPosition(listbox);
            var hit = VisualTreeHelper.HitTest(listbox, positon);
            var visualHit = hit.VisualHit.GetParentOfType<ListBoxItem>();

            if (visualHit is ListBoxItem listboxItem)
            {
                _tableName = listboxItem.DataContext as string ?? string.Empty;
                LogFile.Instance.WriteLine($"TableName {_tableName}");

                if (string.IsNullOrEmpty(_tableName) == true)
                {
                    return;
                }
                var listColumn = SQLiteManager.Instance.ListColumn(_tableName);

                _columnListBox.Items.Clear();
                listColumn.ForEach(column_ => _columnListBox.Items.Add(column_));
                //
                _setText();
            }
        }
    }

    /// <summary>
    /// カラムの選択
    /// </summary>
    /// <param name="sender_"></param>
    /// <param name="e_"></param>
    private void _selectionColumnChanged(object sender_, SelectionChangedEventArgs e_)
    {
        _setText();
    }

    /// <summary>
    /// 貼り付け処理
    /// </summary>
    /// <param name="sender_"></param>
    /// <param name="e_"></param>
    private void _pasteClick(object sender_, RoutedEventArgs e_)
    {
        Clipboard.SetText(_textBox.Text);
        Paste?.Invoke(_textBox.Text);
    }

    /// <summary>
    /// 各リストの読み込み
    /// </summary>
    private void _load()
    {
        _mainListBox.Items.Clear();
        _subListBox.Items.Clear();
        _fieldListBox.Items.Clear();
        _recordListBox.Items.Clear();
        _spaceListBox.Items.Clear();
        _cybozuListBox.Items.Clear();
        //
        SQLiteManager.Instance.ListTableName(SQLiteManager.MAIN_MASTER).ForEach(item_ => _mainListBox.Items.Add(item_));
        SQLiteManager.Instance.ListTableName(SQLiteManager.SUB_MASTER).ForEach(item_ => _subListBox.Items.Add(item_));
        SQLiteManager.Instance.ListTableName(SQLiteManager.FIELD_MASTER).ForEach(item_ => _fieldListBox.Items.Add(item_));
        SQLiteManager.Instance.ListTableName(SQLiteManager.RECORD_MASTER).ForEach(item_ => _recordListBox.Items.Add(item_));
        SQLiteManager.Instance.ListTableName(SQLiteManager.SPACE_MASTER).ForEach(item_ => _spaceListBox.Items.Add(item_));
        SQLiteManager.Instance.ListTableName(SQLiteManager.CYBOZU_MASTER).ForEach(item_ => _cybozuListBox.Items.Add(item_));
        //
        _columnListBox.Items.Clear();
    }

    /// <summary>
    /// SQL作成
    /// </summary>
    private void _setText()
    {
        if (string.IsNullOrEmpty(_tableName) == true)
        {
            return;
        }
        var list = new List<string>();
        foreach (var col in _columnListBox.SelectedItems)
        {
            list.Add(col.ToString() ?? string.Empty);
        }

        var column =(list.Count ==0) ? "*": string.Join(",", list);
        _textBox.Text = $"SELECT {column} FROM {_tableName};";
    }
}
