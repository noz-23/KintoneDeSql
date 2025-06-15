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
using KintoneDeSql.Responses.Commons;
using System.Text.Json.Serialization;

namespace KintoneDeSql.Responses.Apps.Permissions;

/// <summary>
/// https://cybozu.dev/ja/kintone/docs/rest-api/apps/settings/get-per-record-notification-settings/
/// https://cybozu.dev/ja/kintone/docs/rest-api/apps/settings/get-general-notification-settings/
/// </summary>

internal class NotificationValue : BaseToData
{
    //notifications[].targets[].includeSubs	真偽値	設定を下位組織に継承するかどうか
    [JsonPropertyName("includeSubs")]
    [ColumnEx("includeSubs", Order = 1, TypeName = "NUMERIC")]
    public bool includeSubs { get; set; } = false;

    //notifications[].targets[].entity	オブジェクト	通知先の対象
    //notifications[].targets[].entity.type	文字列	通知先の対象の種類
    //notifications[].targets[].entity.code	文字列	通知先の対象のコード
    [JsonPropertyName("entity")]
    [ColumnEx("entity", Order = 50, TypeName = "TEXT", IsExtract = true)]
    public EntityValue Entity { get; set; } = new();

}