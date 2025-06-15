/*
 * Reprise Report Log Analyzer
 * Copyright (c) 2025 noz-23
 *  https://github.com/noz-23/
 * 
 * Licensed under the MIT License 
 * 
 */
using KintoneDeSql.Data;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace KintoneDeSql.Views;

internal class BaseView: BaseToData,INotifyPropertyChanged
{
    #region INotifyPropertyChanged
    /// <summary>
    /// 更新通知
    /// </summary>

    public event PropertyChangedEventHandler? PropertyChanged;
    protected virtual void _NotifyPropertyChanged([CallerMemberName] String propertyName_ = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName_));
    }
    #endregion

    /// <summary>
    /// プロパティの値を設定する
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="field_"></param>
    /// <param name="value_"></param>
    /// <param name="propertyName_"></param>
    /// <returns></returns>
    protected bool _SetValue<T>(ref T field_, T value_, [CallerMemberName] string propertyName_ = "")
    {
        if (EqualityComparer<T>.Default.Equals(field_, value_))
        {
            return false;
        }
        field_ = value_;
        _NotifyPropertyChanged(propertyName_);
        return true;
    }

}
