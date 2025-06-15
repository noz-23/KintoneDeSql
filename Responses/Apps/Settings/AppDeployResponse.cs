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

namespace KintoneDeSql.Responses.Apps.Settings;

/// <summary>
/// https://cybozu.dev/ja/kintone/docs/rest-api/apps/settings/deploy-app-settings/
/// </summary>
[Table($"{SQLiteManager.SUB_DATABASE}.appDeploy")]

internal class AppDeployResponse : BaseToData, ISqlTable
{
    //apps	配列	処理の状況を表すオブジェクトの配列
    [JsonPropertyName("apps")]
    public List<AppDeployValue> ListAppDeploy { get; set; } = new();

    #region ISqlTable
    public static string TableName(bool withCamma_) => typeof(AppDeployResponse).TableName(withCamma_);
    public static IEnumerable<string> ListCreateHeader(bool withCamma_)=> typeof(AppDeployValue).ListCreateHeader(withCamma_);
    public static IEnumerable<string> ListInsertHeader(bool withCamma_) => typeof(AppDeployValue).ListInsertHeader(withCamma_);
    public IEnumerable<IEnumerable<string>> ListInsertValue(bool withCamma_)
    {
        var rtn = new List<IList<string>>();
        ListAppDeploy.ForEach(app_ => rtn.Add(app_.ListValue(withCamma_)));
        return rtn;
    }
    #endregion
}
