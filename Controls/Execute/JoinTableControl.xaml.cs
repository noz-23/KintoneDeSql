/*
 * Kintone De Sql
 * Copyright (c) 2025 noz-23
 *  https://github.com/noz-23/
 * 
 * Licensed under the MIT License 
 * 
 */
using System.Windows;
using System.Windows.Controls;

namespace KintoneDeSql.Controls.Execute;

public delegate void SetTextCallBack();

/// <summary>
/// JoinTableControl.xaml の相互作用ロジック
/// </summary>
public partial class JoinTableControl : UserControl
{
    /// <summary>
    /// コンストラクタ
    /// </summary>
    public JoinTableControl()
    {
        InitializeComponent();
        //
        _leftJoinSelectControl.SetText = _setText;
        _rightJoinSelectControl.SetText = _setText;
    }

    /// <summary>
    /// SQL作成処理
    /// </summary>
    public PaseteCallBack? Paste = null;

    /// <summary>
    /// 結合処理
    /// </summary>
    public const string INNER_JOIN = "INNER JOIN";
    public const string LEFT_JOIN = "LEFT OUTER JOIN";
    public const string RIGHT_JOIN = "RIGHT OUTER JOIN";
    public const string FULL_JOIN = "FULL OUTER JOIN";

    private void _loaded(object sender_, RoutedEventArgs e_)
    {
        _comboBox.SelectedIndex = 0;
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
    /// 結合コンボボックス変更
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void _comboBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        _setText();
    }

    /// <summary>
    /// SQL 作成
    /// </summary>
    private void _setText()
    {
        string join = _comboBox.SelectedValue?.ToString() ?? string.Empty;
        if (string.IsNullOrEmpty(join) == true)
        {
            _textBox.Text = string.Empty;
            return;
        }

        if (string.IsNullOrEmpty(_leftJoinSelectControl.TableName) == true)
        {
            _textBox.Text = string.Empty;
            return;
        }
        if (string.IsNullOrEmpty(_rightJoinSelectControl.TableName) == true)
        {
            _textBox.Text = string.Empty;
            return;
        }
        if (_leftJoinSelectControl.ListSelect.Count == 0)
        {
            _textBox.Text = string.Empty;
            return;
        }

        if (_leftJoinSelectControl.ListSelect.Count != _rightJoinSelectControl.ListSelect.Count)
        {
            _textBox.Text = string.Empty;
            return;
        }
        var list =new List<string>();

        for (var i = 0; i < _leftJoinSelectControl.ListSelect.Count; i++)
        {
            var leftColumn = $"{_leftJoinSelectControl.TableName}.{_leftJoinSelectControl.ListSelect[i]}";
            var rigthColumn = $"{_rightJoinSelectControl.TableName}.{_rightJoinSelectControl.ListSelect[i]}";

            list.Add($"{leftColumn} = {rigthColumn}");
        }

        _textBox.Text = $"SELECT * FROM {_leftJoinSelectControl.TableName} {join} {_rightJoinSelectControl.TableName} on {string.Join(" AND ", list)};";
    }

}
