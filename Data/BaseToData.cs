/*
 * Kintone De Sql
 * Copyright (c) 2025 noz-23
 *  https://github.com/noz-23/
 * 
 * Licensed under the MIT License 
 * 
 */
using KintoneDeSql.Attributes;
using KintoneDeSql.Extensions;

namespace KintoneDeSql.Data;

/// <summary>
/// リフレクションを利用したカラム名やデータ取得のベース
/// </summary>
internal class BaseToData
{
    /// <summary>
    /// ColumExの値取得
    /// </summary>
    /// <param name="withCamma_"></param>
    /// <returns></returns>
    public virtual List<string> ListValue(bool withCamma_)
    {
        var rtn = new List<string>();
        var listPropetyInfo = this.GetType().ListColumnExProperty();

        if ((listPropetyInfo?.Any() ?? false) == false)
        {
            return rtn;
        }

        foreach (var prop in listPropetyInfo)
        {
            var column = prop.GetAttribute<ColumnExAttribute>();
            if (column == null)
            {
                continue;
            }
            if (column.IsExtract == true)
            {
                // 展開設定がある場合は展開
                var valData = prop.GetValue(this);
                if (valData == null)
                {
                    // null の場合、インスタンスを作成(初期値)入れ
                    valData = Activator.CreateInstance(prop.PropertyType);
                }
                if (valData is BaseToData data)
                {
                    rtn.AddRange(data.ListValue(withCamma_));
                    continue;
                }
            }

            // 値入れ
            var val = prop.GetValue(this)?.ToString() ?? string.Empty;                       
            rtn.Add((withCamma_ ==true) ? $"'{val}'" :val);
        }
        return rtn;
    }

    /// <summary>
    /// 文字列
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        var list = this.ListValue(false);
        return string.Join("\t", list);
    }
}
