/*
 * Kintone De Sql
 * Copyright (c) 2025 noz-23
 *  https://github.com/noz-23/
 * 
 * Licensed under the MIT License 
 * 
 */
using Dapper;
using System.Data;

namespace KintoneDeSql.Data;

/// <summary>
/// 文字列 → 時間変更
/// https://stackoverflow.com/questions/12510299/get-datetime-as-utc-with-dapper
/// </summary>
internal sealed class DateTimeHandler : SqlMapper.TypeHandler<DateTime?>
{
    public static readonly DateTimeHandler Default = new DateTimeHandler();

    /// <summary>
    /// コンストラクタ
    /// </summary>
    public DateTimeHandler()
    {

    }

    /// <summary>
    /// 文字列 → 時間変更
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public override DateTime? Parse(object value_)
    {
        if (string.IsNullOrEmpty(value_.ToString()) == true)
        {
            return null;
        }
        return DateTime.Parse(value_.ToString()!);
    }

    /// <summary>
    /// 時間変更 → 文字列
    /// </summary>
    /// <param name="parameter"></param>
    /// <param name="value"></param>

    public override void SetValue(IDbDataParameter parameter, DateTime? value_)
    {
        parameter.Value = value_?.ToString()??string.Empty;
    }
}
