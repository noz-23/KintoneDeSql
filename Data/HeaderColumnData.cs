/*
 * Kintone De Sql
 * Copyright (c) 2025 noz-23
 *  https://github.com/noz-23/
 * 
 * Licensed under the MIT License 
 * 
 */
namespace KintoneDeSql.Data;

/// <summary>
/// 結合選択とアンダースコアが表示されない用
/// </summary>
internal class HeaderColumnData
{
    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="column_"></param>
    /// <param name="list_"></param>
    public HeaderColumnData(string column_, IList<string>? list_=null)
    {
        Column = column_;
        _listSelect = list_;
    }

    /// <summary>
    /// 元のカラム名
    /// </summary>
    public string Column { get; private set; } = string.Empty;

    /// <summary>
    /// 実際の表示
    /// </summary>
    public string Header
    {
        get
        {
            if (IsSelect)
            {
                // 選択位置の番号を追加
                return $"{Column} [{_listSelect?.IndexOf(Column) + 1}]";
            }
            return Column;

        }
    }

    /// <summary>
    /// カラム選択リスト
    /// </summary>
    private IList<string>? _listSelect = null;

    /// <summary>
    /// 選択カラムかの判定
    /// </summary>
    public bool IsSelect => _listSelect != null && _listSelect.Contains(Column);

    public override string ToString() => Header;
}