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

namespace KintoneDeSql.Responses.Cybozu;

/// <summary>
/// https://cybozu.dev/ja/common/docs/user-api/users/get-users-organizations/
/// </summary>
[Table($"{SQLiteManager.CYBOZU_DATABASE}.organizationTitles")]
internal class ListOrganizationTitleResponse:BaseToData, ISqlTable
{
    /// <summary>
    /// 上位のユーザーコード
    /// </summary>
    [JsonPropertyName("code")]
    [ColumnEx("code", Order = 10, TypeName = "TEXT", IsUnique = true)]
    public string Code { get; set; } = string.Empty;

    // organizationTitles	配列	ユーザーが所属する組織と役職の一覧
    [JsonPropertyName("organizationTitles")]
    public List<OrganizationTitleResponse> ListOrganizationTitle { get; set; } = new();

    #region ISqlTable
    public static string TableName(bool withCamma_) => typeof(ListOrganizationTitleResponse).TableName(withCamma_);

    public static List<string> ListCreateHeader(bool withCamma_)
    {
        var rtn = typeof(ListOrganizationTitleResponse).ListColumn();
        rtn.AddRange(typeof(OrganizationTitleResponse).ListColumn("organizationTitles_"));

        return MemberInfoExtension.ListCreateHeader(rtn, withCamma_);
    }

    public static List<string> ListInsertHeader(bool withCamma_)
    {
        var rtn = typeof(ListOrganizationTitleResponse).ListColumn();
        rtn.AddRange(typeof(OrganizationTitleResponse).ListColumn("organizationTitles_"));

        return MemberInfoExtension.ListInsertHeader(rtn, withCamma_);
    }
    public List<List<string>> ListInsertValue(bool withCamma_)
    {
        var rtn = new List<List<string>>();

        foreach (var organaization in ListOrganizationTitle)
        {
            var list = this.ListValue(withCamma_);
            list.AddRange(organaization.ListValue(withCamma_));
            rtn.Add(list);
        }
        return rtn;
    }
    #endregion

}
