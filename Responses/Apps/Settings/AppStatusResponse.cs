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

namespace KintoneDeSql.Responses.Apps.Settings;

/// <summary>
/// https://cybozu.dev/ja/kintone/docs/rest-api/apps/settings/get-process-management-settings/
/// </summary>
[Table($"{SQLiteManager.SUB_DATABASE}.appStatus")]

internal class AppStatusResponse: AppStatusResponseAction, ISqlTable
{
    #region ISqlTable
    public static new string TableName(bool withCamma_) => typeof(AppStatusResponse).TableName(withCamma_);
    public static new IEnumerable<string> ListCreateHeader(bool withCamma_) => MemberInfoExtension.ListCreateHeader(_listColumn(), withCamma_);
    public static new IEnumerable<string> ListInsertHeader(bool withCamma_) => MemberInfoExtension.ListInsertHeader(_listColumn(), withCamma_);
    private static IEnumerable<ColumnData> _listColumn() => typeof(AppStatusResponse).ListColumn();
    public override IEnumerable<IEnumerable<string>> ListInsertValue(bool withCamma_) => new List<IEnumerable<string>>() { base.ListValue(withCamma_)};
    #endregion
}
