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
/// https://cybozu.dev/ja/kintone/docs/rest-api/apps/settings/get-record-permissions/
/// </summary>
internal class RecordNotificationValueAcl : NotificationValue
{
    //rights[].entities[].viewable	真偽値	レコードの閲覧が可能かどうか
    [JsonPropertyName("viewable")]
    [ColumnEx("viewable", Order = 20, TypeName = "NUMERIC")]
    public bool Viewable { get; set; } = false;

    //rights[].entities[].editable	真偽値	レコードの編集が可能かどうか
    [JsonPropertyName("editable")]
    [ColumnEx("editable", Order = 21, TypeName = "NUMERIC")]
    public bool Editable { get; set; } = false;

    //rights[].entities[].deletable	真偽値	レコードの削除が可能かどうか
    [JsonPropertyName("deletable")]
    [ColumnEx("deletable", Order = 22, TypeName = "NUMERIC")]
    public bool Deletable { get; set; } = false;

    //rights[].entities[].entity	オブジェクト	アクセス権の設定の対象
    //rights[].entities[].entity.type	文字列	アクセス権の設定対象の種類
    //rights[].entities[].entity.code	文字列	アクセス権の設定対象のコード
    //rights[].entities[].includeSubs	真偽値	設定を下位組織に継承するか
    // ->base
}