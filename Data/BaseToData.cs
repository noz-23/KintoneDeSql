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
using KintoneDeSql.Properties;
using KintoneDeSql.Responses.Records;

namespace KintoneDeSql.Data;

/// <summary>
/// リフレクションを利用したカラム名やデータ取得のベース
/// </summary>
internal class BaseToData
{
    /// <summary>
    /// コンストラクタ
    /// </summary>
    public BaseToData()
    {
    }

    /// <summary>
    /// ColumExの値取得
    /// </summary>
    /// <param name="withCamma_"></param>
    /// <returns></returns>

    public virtual IList<string> ListValue(bool withCamma_)=> ListValue(withCamma_, this.GetType());

    public virtual IList<string> ListValue(bool withCamma_, Type type_)
    {
        var rtn = new List<string>();
        var listPropetyInfo = type_.ListColumnExProperty();

        if ((listPropetyInfo?.Any() ?? false) == false)
        {
            return rtn;
        }
        //
        foreach (var prop in listPropetyInfo)
        {
            var column = prop.GetAttribute<ColumnExAttribute>();
            if (column == null)
            {
                continue;
            }
            if (column.IsExtract == true)
            {
                if (Settings.Default.IsCreatorExtract == false && prop.PropertyType == typeof(CreatorValue))
                {
                    var crt = prop.GetValue(this)?.ToString() ?? string.Empty;
                    rtn.Add((withCamma_ == true) ? $"'{crt}'" : crt);
                    continue;
                }
                if (Settings.Default.IsModifierExtract == false && prop.PropertyType == typeof(ModifierValue))
                {
                    var mod = prop.GetValue(this)?.ToString() ?? string.Empty;
                    rtn.Add((withCamma_ == true) ? $"'{mod}'" : mod);
                    continue;
                }

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
