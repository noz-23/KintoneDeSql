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
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace KintoneDeSql.Responses.Apps;

/// <summary>
/// https://cybozu.dev/ja/kintone/docs/rest-api/apps/settings/get-app-permissions/
/// </summary>
[Table($"{SQLiteManager.SUB_DATABASE}.appAcl")]
internal class ListAppAclResponse: BaseToData,ISqlTable
{
    //rights	配列	アクセス権の設定の一覧
    [JsonPropertyName("rights")]
    public List<AppAclResponse> ListRight { get; set; } = new();

    #region NoJson
    /// <summary>
    /// 上位のアプリ ID
    /// </summary>
    [ColumnEx("appId", Order = 1, TypeName = "TEXT")]
    public string AppId { get; set; } = string.Empty;
    //
    #endregion

    #region ISqlTable
    public static string TableName(bool withCamma_) => typeof(ListAppAclResponse).TableName(withCamma_);
    public static List<string> ListCreateHeader(bool withCamma_)
    {
        var rtn = typeof(ListAppAclResponse).ListCreateHeader(withCamma_);
        rtn.AddRange(typeof(AppAclResponse).ListCreateHeader(withCamma_));
        return rtn;
    }
    public static List<string> ListInsertHeader(bool withCamma_)
    {
        var rtn = typeof(ListAppAclResponse).ListInsertHeader(withCamma_);
        rtn.AddRange(typeof(AppAclResponse).ListInsertHeader(withCamma_));
        return rtn;
    }

    public List<List<string>> ListInsertValue(bool withCamma_)
    {
        var rtn = new List<List<string>>();

        foreach (var right in ListRight)
        {
            var list = base.ListValue(withCamma_);
            list.AddRange(right.ListValue(withCamma_));
            rtn.Add(list);
        }

        return rtn;
    }
    #endregion
}
