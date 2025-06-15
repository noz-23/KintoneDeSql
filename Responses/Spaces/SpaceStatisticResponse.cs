/*
 * Kintone De Sql
 * Copyright (c) 2025 noz-23
 *  https://github.com/noz-23/
 * 
 * Licensed under the MIT License 
 * 
 */
using KintoneDeSql.Data;
using KintoneDeSql.Extensions;
using KintoneDeSql.Interface;
using KintoneDeSql.Managers;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace KintoneDeSql.Responses.Spaces;

/// <summary>
/// https://cybozu.dev/ja/kintone/docs/rest-api/spaces/get-spaces-statistics/
/// </summary>
[Table($"{SQLiteManager.SPACE_DATABASE}.spaceStatistics")]
internal class SpaceStatisticResponse : BaseToData,ISqlTable
{
    // spaces	配列	スペースの使用状況の一覧
    [JsonPropertyName("spaces")]
    public List<SpaceStatisticValue> ListSpace { get; set; } = new ();

    #region ISqlTable
    public static string TableName(bool withCamma_) => typeof(SpaceStatisticResponse).TableName(withCamma_);
    public static IEnumerable<string> ListCreateHeader(bool withCamma_) => typeof(SpaceStatisticValue).ListCreateHeader(withCamma_);
    public static IEnumerable<string> ListInsertHeader(bool withCamma_) => typeof(SpaceStatisticValue).ListInsertHeader(withCamma_);
    public IEnumerable<IEnumerable<string>> ListInsertValue(bool withCamma_)
    {
        var rtn = new List<IList<string>>();
        ListSpace.ForEach(space_ => rtn.Add(space_.ListValue(withCamma_)));
        return rtn;
    }
    #endregion
}
