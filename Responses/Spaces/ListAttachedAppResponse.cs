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

namespace KintoneDeSql.Responses.Spaces;

/// <summary>
/// https://cybozu.dev/ja/kintone/docs/rest-api/spaces/get-space/
/// </summary>

[Table($"{SQLiteManager.SPACE_DATABASE}.attachedApps")]
internal class ListAttachedAppResponse : List<AttachedAppResponse>, ISqlTable
{
    #region ISqlTable
    public static string TableName(bool withCamma_) => typeof(ListAttachedAppResponse).TableName(withCamma_);
    public static List<string> ListCreateHeader(bool withCamma_) => typeof(AttachedAppResponse).ListCreateHeader(withCamma_);
    public static List<string> ListInsertHeader(bool withCamma_) => typeof(AttachedAppResponse).ListInsertHeader(withCamma_);
    public List<List<string>> ListInsertValue(bool withCamma_)
    {
        var rtn = new List<List<string>>();

        this.ForEach(app_ => rtn.Add(app_.ListValue(withCamma_)));
        return rtn;
    }
    #endregion

    public override string ToString()
    {
        return string.Join("\n",this.Select(app_ => $"{app_.AppId}:{app_.Name}"));
    }
}
