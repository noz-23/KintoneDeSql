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
using KintoneDeSql.Interface;
using KintoneDeSql.Managers;
using KintoneDeSql.Responses.Records;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace KintoneDeSql.Responses.Spaces;

/// <summary>
/// https://cybozu.dev/ja/kintone/docs/rest-api/spaces/get-space/
/// </summary>
[Table($"{SQLiteManager.SPACE_DATABASE}.spaces")]
internal class SpaceResponse : SpaceResponseAttachedApps, ISqlTable
{
    #region ISqlTable
    public static new string TableName(bool withCamma_) => typeof(SpaceResponse).TableName(withCamma_);
    public static new IEnumerable<string> ListCreateHeader(bool withCamma_) => typeof(SpaceResponse).ListCreateHeader(withCamma_);
    public static new IEnumerable<string> ListInsertHeader(bool withCamma_) => typeof(SpaceResponse).ListInsertHeader(withCamma_);
    public override IEnumerable<IEnumerable<string>> ListInsertValue(bool withCamma_)=> new List<IList<string>> { this.ListValue(withCamma_) }; 
    #endregion

}
