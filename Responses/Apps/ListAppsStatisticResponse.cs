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

internal class ListAppsStatisticResponse : BaseToData, ISqlTable
{
    //apps	配列	アプリ情報の一覧
    [JsonPropertyName("apps")]
    public List<AppsStatisticResponse> ListApp { get; set; } = new();

    #region ISqlTable
    public static string TableName(bool withCamma_) => typeof(ListAppsStatisticResponse).TableName(withCamma_);
    public static List<string> ListCreateHeader(bool withCamma_) => typeof(AppsStatisticResponse).ListCreateHeader(withCamma_);
    public static List<string> ListInsertHeader(bool withCamma_) => typeof(AppsStatisticResponse).ListInsertHeader(withCamma_);
    public List<List<string>> ListInsertValue(bool withCamma_)
    {
        var rtn = new List<List<string>>();

        ListApp.ForEach(app_ => rtn.Add(app_.ListValue(withCamma_)));

        return rtn;
    }
    #endregion

}
