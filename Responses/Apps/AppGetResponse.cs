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
using System.Windows.Documents;

namespace KintoneDeSql.Responses.Apps;

/// <summary>
/// https://cybozu.dev/ja/kintone/docs/rest-api/apps/get-apps/
/// </summary>

[Table($"{SQLiteManager.SUB_DATABASE}.apps")]
internal class AppGetResponse : BaseToData,ISqlTable
{
    [JsonPropertyName("apps")]
    public List<AppGetValue> ListApp { get; set; } =new ();

    #region ISqlTable
    public static string TableName(bool withCamma_) => typeof(AppGetResponse).TableName(withCamma_);
    public static IEnumerable<string> ListCreateHeader(bool withCamma_) => typeof(AppGetValue).ListCreateHeader(withCamma_);
    public static IEnumerable<string> ListInsertHeader(bool withCamma_) => typeof(AppGetValue).ListInsertHeader(withCamma_);
    public IEnumerable<IEnumerable<string>> ListInsertValue(bool withCamma_)
    {
        var rtn = new List<IEnumerable<string>>();
        ListApp.ForEach(app_ => rtn.Add(app_.ListValue(withCamma_)));
        return rtn;
    }
    #endregion
}
