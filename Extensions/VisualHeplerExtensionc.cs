/*
 * Reprise Report Log Analyzer
 * Copyright (c) 2024 noz-23
 *  https://github.com/noz-23/
 * 
 * Licensed under the MIT License 
 * 
 */
using System.Windows;
using System.Windows.Media;

namespace KintoneDeSql.Extensions;

/// <summary>
/// 表示位置からからアイテムの選択
/// </summary>
public static class VisualHeplerExtensionc
{
    public static T? GetParentOfType<T>(this DependencyObject src) where T : DependencyObject
    {
        while (src != null)
        {
            if (src is T rtn)
            {
                return rtn;
            }
            src = VisualTreeHelper.GetParent(src);
        }

        return null;
    }

    public static List<T> GetListChildOfType<T>(this DependencyObject src, List<T>? listChild_ = null) where T : DependencyObject
    {
        listChild_ ??= new List<T>();
        //
        var count = VisualTreeHelper.GetChildrenCount(src);
        for (var i = 0; i < count; i++)
        {
            var child = VisualTreeHelper.GetChild(src, i);
            if (child is T type)
            {
                listChild_.Add(type);
            }
            listChild_ = child.GetListChildOfType(listChild_);
        }

        return listChild_;
    }
}
