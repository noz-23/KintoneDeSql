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

namespace KintoneDeSql.Responses.Cybozu.Organizations;

/// <summary>
/// https://cybozu.dev/ja/common/docs/user-api/organizations/get-organizations/
/// </summary>
[Table($"{SQLiteManager.CYBOZU_DATABASE}.organization")]
internal class OrganizationResponse:ISqlTable
{
    [JsonPropertyName("organizations")]
    public List<OrganizationValue> ListOrganization { get; set; } = new();

    #region ISqlTable
    public static string TableName(bool withCamma_) => typeof(OrganizationResponse).TableName(withCamma_);
    public static IEnumerable<string> ListCreateHeader(bool withCamma_) => typeof(OrganizationValue).ListCreateHeader(withCamma_);
    public static IEnumerable<string> ListInsertHeader(bool withCamma_) => typeof(OrganizationValue).ListInsertHeader(withCamma_);
    public IEnumerable<IEnumerable<string>> ListInsertValue(bool withCamma_)
    {
        var rtn = new List<IList<string>>();
        ListOrganization.ForEach(org_ => rtn.Add(org_.ListValue(withCamma_)));
        return rtn;
    }
    #endregion
}
