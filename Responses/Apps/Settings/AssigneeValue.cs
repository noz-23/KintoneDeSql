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
using KintoneDeSql.Responses.Apps.Permissions;
using System.Text.Json.Serialization;

namespace KintoneDeSql.Responses.Apps.Settings;

/// <summary>
/// https://cybozu.dev/ja/kintone/docs/rest-api/apps/settings/get-process-management-settings/
/// </summary>
internal class AssigneeValue : BaseToData
{
    //states.ステータス名.assignee.type	文字列	ステータスの作業者のタイプ
    [JsonPropertyName("type")]
    [ColumnEx("type", Order = 10, TypeName = "TEXT",IsUnique =true)]
    public string Type { get; set; } = string.Empty;

    //states.ステータス名.assignee.entities	配列	ステータスの作業者の一覧
    //states.ステータス名.assignee.entities[].entity.type	文字列	ステータスの作業者の指定形式
    //states.ステータス名.assignee.entities[].entity.code	文字列	ステータスの作業者のコード
    //states.ステータス名.assignee.entities[].includeSubs	真偽値	設定を下位組織に継承するかどうか
    [JsonPropertyName("entities")]
    public List<NotificationValue> ListEntity { get; set; } = new();

}
