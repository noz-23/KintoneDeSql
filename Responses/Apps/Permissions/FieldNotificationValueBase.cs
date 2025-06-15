/*
 * Kintone De Sql
 * Copyright (c) 2025 noz-23
 *  https://github.com/noz-23/
 * 
 * Licensed under the MIT License 
 * 
 */
using KintoneDeSql.Attributes;
using System.Text.Json.Serialization;

namespace KintoneDeSql.Responses.Apps.Permissions;

/// <summary>
/// https://cybozu.dev/ja/kintone/docs/rest-api/apps/settings/get-field-permissions/
/// </summary>
internal class FieldNotificationValueBase : NotificationValue
{
    //rights[].entities[].accessibility	文字列	フィールドに対して可能な操作
    [JsonPropertyName("accessibility")]
    [ColumnEx("accessibility", Order = 20, TypeName = "TEXT")]
    public string Accessibility { get; set; } = string.Empty;

    //rights[].entities[].entity	オブジェクト	アクセス権の設定の対象
    //rights[].entities[].entity.type	文字列	アクセス権の設定対象の種類
    //rights[].entities[].entity.code	文字列	アクセス権の設定対象のコード
    //rights[].entities[].includeSubs	真偽値	設定を下位組織に継承するかどうか
    // ->base
}
