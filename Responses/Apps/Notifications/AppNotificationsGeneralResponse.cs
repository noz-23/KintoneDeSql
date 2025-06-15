/*
 * Kintone De Sql
 * Copyright (c) 2025 noz-23
 *  https://github.com/noz-23/
 * 
 * Licensed under the MIT License 
 * 
 */
using KintoneDeSql.Attributes;
using KintoneDeSql.Data;
using KintoneDeSql.Extensions;
using KintoneDeSql.Interface;
using KintoneDeSql.Managers;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace KintoneDeSql.Responses.Apps.Notifications;

/// <summary>
/// https://cybozu.dev/ja/kintone/docs/rest-api/apps/settings/get-general-notification-settings/
/// </summary>
[Table($"{SQLiteManager.SUB_DATABASE}.notificationsGeneral")]
internal class AppNotificationsGeneralResponse : BaseToData, ISqlTable
{
    //notifications	配列（オブジェクト）	条件通知の設定を表すオブジェクトの配列
    [JsonPropertyName("notifications")]
    public List<AppNotificationsGeneralValue> ListNotifications { get; set; } = new();

    //notifyToCommenter	真偽値	コメントを書き込んだユーザーが、そのレコードにコメントが書き込まれたときに通知を受信するかどうか
    [JsonPropertyName("notifyToCommenter")]
    [ColumnEx("notifyToCommenter", Order = 3, TypeName = "NUMERIC")]
    public bool NotifyToCommenter { get; set; } = false;

    //revision	文字列	アプリの設定のリビジョン番号
    [JsonPropertyName("revision")]
    [ColumnEx("revision", Order = 2, TypeName = "TEXT")]
    public string Revision { get; set; } = string.Empty;

    #region NoJson
    /// <summary>
    /// 上位のアプリID
    /// </summary>
    [ColumnEx("appId", Order = 1, TypeName = "TEXT")]
    public string AppId { get; set; } = string.Empty;
    #endregion

    #region ISqlTable
    public static string TableName(bool withCamma_) => typeof(AppNotificationsGeneralResponse).TableName(withCamma_);
    public static IEnumerable<string> ListCreateHeader(bool withCamma_) => MemberInfoExtension.ListCreateHeader(_listColumn(), withCamma_);
    public static IEnumerable<string> ListInsertHeader(bool withCamma_) => MemberInfoExtension.ListInsertHeader(_listColumn(), withCamma_);
    private static IEnumerable<ColumnData> _listColumn()
    {
        const int _SORT = 100;
        var rtn = typeof(AppNotificationsGeneralResponse).ListColumn();
        rtn.AddRange(typeof(AppNotificationsGeneralValue).ListColumn(string.Empty,_SORT));
        return rtn;
    }
    public IEnumerable<IEnumerable<string>> ListInsertValue(bool withCamma_)
    {
        var rtn = new List<IEnumerable<string>>();
        foreach (var notif in ListNotifications)
        {
            var list = ListValue(withCamma_);
            list.AddRange(notif.ListValue(withCamma_));
            rtn.Add(list);
        }
        return rtn;
    }
    #endregion
}
