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

namespace KintoneDeSql.Responses.Apps;

[Table($"{SQLiteManager.SUB_DATABASE}.appsStatistics")]

internal class AppsStatisticResponse : BaseToData, ISqlTable
{
    //apps	配列	アプリ情報の一覧
    [JsonPropertyName("apps")]
    public List<AppsStatisticValue> ListApp { get; set; } = new();

    #region ISqlTable
    public static string TableName(bool withCamma_) => typeof(AppsStatisticResponse).TableName(withCamma_);
    public static IEnumerable<string> ListCreateHeader(bool withCamma_) => typeof(AppsStatisticValue).ListCreateHeader(withCamma_);
    public static IEnumerable<string> ListInsertHeader(bool withCamma_) => typeof(AppsStatisticValue).ListInsertHeader(withCamma_);
    public IEnumerable<IEnumerable<string>> ListInsertValue(bool withCamma_)
    {
        var rtn = new List<IList<string>>();
        ListApp.ForEach(app_ => rtn.Add(app_.ListValue(withCamma_)));
        return rtn;
    }
    #endregion
}
