/*
 * Kintone De Sql
 * Copyright (c) 2025 noz-23
 *  https://github.com/noz-23/
 * 
 * Licensed under the MIT License 
 * 
 */
using KintoneDeSql.Extensions;
using KintoneDeSql.Interface;
using KintoneDeSql.Managers;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace KintoneDeSql.Responses.Cybozu;

/// <summary>
/// https://cybozu.dev/ja/common/docs/user-api/organizations/get-organizations/
/// </summary>
[Table($"{SQLiteManager.CYBOZU_DATABASE}.organizations")]
internal class ListOrganizationResponse:ISqlTable
{
    [JsonPropertyName("organizations")]
    public List<OrganizationResponse> ListOrganization { get; set; } = new();

    #region ISqlTable
    public static string TableName(bool withCamma_) => typeof(ListOrganizationResponse).TableName(withCamma_);

    public static List<string> ListCreateHeader(bool withCamma_) => typeof(OrganizationResponse).ListCreateHeader(withCamma_);

    public static List<string> ListInsertHeader(bool withCamma_) => typeof(OrganizationResponse).ListInsertHeader(withCamma_);

    public List<List<string>> ListInsertValue(bool withCamma_)
    {
        var rtn = new List<List<string>>();

        ListOrganization.ForEach(item_ => rtn.Add(item_.ListValue(withCamma_)));

        return rtn;
    }
    #endregion
}
