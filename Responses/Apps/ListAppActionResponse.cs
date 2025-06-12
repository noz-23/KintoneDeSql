/*
 * Kintone De Sql
 * Copyright (c) 2025 noz-23
 *  https://github.com/noz-23/
 * 
 * Licensed under the MIT License 
 * 
 */
using KintoneDeSql.Attributes;
using KintoneDeSql.Data;
using KintoneDeSql.Extensions;
using KintoneDeSql.Interface;
using KintoneDeSql.Managers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace KintoneDeSql.Responses.Apps;

/// <summary>
/// https://cybozu.dev/ja/kintone/docs/rest-api/apps/settings/get-action-settings/
/// </summary>
[Table($"{SQLiteManager.SUB_DATABASE}.appAction")]

internal class ListAppActionResponse : BaseToData, ISqlTable
{
    //actions	オブジェクト	アクションの設定
    //actions.アクション名	オブジェクト	各アクションの設定
    [JsonPropertyName("actions")]
    Dictionary<string, AppActionResponse> ListAction { get; set; } = new ();


    #region NoJson
    /// <summary>
    /// propertiesのKey
    /// </summary>
    [ColumnEx("actions_key", Order = 10, TypeName = "TEXT")]
    public string ActionKey { get; set; } = string.Empty;  // 実利用はなし

    /// <summary>
    /// 上位のApp ID
    /// </summary>
    [ColumnEx("appId", Order = 1, TypeName = "TEXT")]
    public string AppId { get; set; } = string.Empty;
    #endregion

    #region ISqlTable
    public static string TableName(bool withCamma_) => typeof(ListAppActionResponse).TableName(withCamma_);
    public static List<string> ListCreateHeader(bool withCamma_)
    {
        var rtn = typeof(ListAppActionResponse).ListCreateHeader(withCamma_);
        rtn.AddRange(typeof(AppActionResponse).ListCreateHeader(withCamma_));
        return rtn;
    }
    public static List<string> ListInsertHeader(bool withCamma_)
    {
        var rtn = typeof(ListAppActionResponse).ListInsertHeader(withCamma_);
        rtn.AddRange(typeof(AppActionResponse).ListInsertHeader(withCamma_));
        return rtn;
    }

    public List<List<string>> ListInsertValue(bool withCamma_)
    {
        var rtn = new List<List<string>>();

        foreach (var action in ListAction)
        {
            ActionKey =action.Key;

            var list = base.ListValue(withCamma_);
            list.AddRange(action.Value.ListValue(withCamma_));
            rtn.Add(list);
        }

        return rtn;
    }
    #endregion


}
