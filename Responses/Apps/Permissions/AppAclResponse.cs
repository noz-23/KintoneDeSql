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

namespace KintoneDeSql.Responses.Apps.Permissions;

/// <summary>
/// https://cybozu.dev/ja/kintone/docs/rest-api/apps/settings/get-app-permissions/
/// </summary>
[Table($"{SQLiteManager.SUB_DATABASE}.appAcl")]
internal class AppAclResponse: BaseToData,ISqlTable,IAppTableId
{
    //rights	配列	アクセス権の設定の一覧
    [JsonPropertyName("rights")]
    public List<AppNotificationValue> ListRight { get; set; } = new();

    //revision	文字列	アプリの設定のリビジョン番号
    [JsonPropertyName("revision")]
    [ColumnEx("revision", Order = 2, TypeName = "TEXT")]
    public string Revision { get; set; } = string.Empty;

    #region NoJson
    /// <summary>
    /// 上位のアプリ ID
    /// </summary>
    [ColumnEx("appId", Order = 1, TypeName = "TEXT")]
    public string AppId { get; set; } = string.Empty;
    //
    #endregion

    #region ISqlTable
    public static string TableName(bool withCamma_) => typeof(AppAclResponse).TableName(withCamma_);
    public static IEnumerable<string> ListCreateHeader(bool withCamma_)=> MemberInfoExtension.ListCreateHeader(_listColumn(), withCamma_);
    public static IEnumerable<string> ListInsertHeader(bool withCamma_)=>MemberInfoExtension.ListInsertHeader(_listColumn(), withCamma_);

    private static IEnumerable<ColumnData> _listColumn()
    {
        const int _SORT = 100;
        var rtn = typeof(AppAclResponse).ListColumn();
        rtn.AddRange(typeof(AppNotificationValue).ListColumn(string.Empty, _SORT));
        return rtn;
    }

    public IEnumerable<IEnumerable<string>> ListInsertValue(bool withCamma_)
    {
        var rtn = new List<IEnumerable<string>>();
        foreach (var right in ListRight)
        {
            var list = ListValue(withCamma_);
            list.AddRange(right.ListValue(withCamma_));
            rtn.Add(list);
        }
        return rtn;
    }
    #endregion
}
