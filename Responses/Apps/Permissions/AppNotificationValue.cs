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
/// https://cybozu.dev/ja/kintone/docs/rest-api/apps/settings/get-app-permissions/
/// </summary>
internal class AppNotificationValue:BaseToData
{
    //rights[].includeSubs	真偽値	設定を下位組織に継承するかどうか
    [JsonPropertyName("includeSubs")]
    [ColumnEx("includeSubs", Order = 10, TypeName = "NUMERIC")]
    public bool IncludeSubs { get; set; } = false;

    //rights[].appEditable	真偽値	アプリの管理が可能かどうか
    [JsonPropertyName("appEditable")]
    [ColumnEx("appEditable", Order = 11, TypeName = "NUMERIC")]
    public bool AppEditable { get; set; } = false;

    //rights[].recordViewable	真偽値	レコードの閲覧が可能かどうか
    [JsonPropertyName("recordViewable")]
    [ColumnEx("recordViewable", Order = 12, TypeName = "NUMERIC")]
    public bool RecordViewable { get; set; } = false;

    //rights[].recordAddable	真偽値	レコードの追加が可能かどうか
    [JsonPropertyName("recordAddable")]
    [ColumnEx("recordAddable", Order = 13, TypeName = "NUMERIC")]
    public bool RecordAddable { get; set; } = false;

    //rights[].recordEditable	真偽値	レコードの編集が可能かどうか
    [JsonPropertyName("recordEditable")]
    [ColumnEx("recordEditable", Order = 14, TypeName = "NUMERIC")]
    public bool RecordEditable { get; set; } = false;

    //rights[].recordDeletable	真偽値	レコードの削除が可能かどうか
    [JsonPropertyName("recordDeletable")]
    [ColumnEx("recordDeletable", Order = 15, TypeName = "NUMERIC")]
    public bool RecordDeletable { get; set; } = false;

    //rights[].recordImportable	真偽値	ファイルの読み込みが可能かどうか
    [JsonPropertyName("recordImportable")]
    [ColumnEx("recordImportable", Order = 16, TypeName = "NUMERIC")]
    public bool RecordImportable { get; set; } = false;

    //rights[].recordExportable	真偽値	ファイルの書き出しが可能かどうか
    [JsonPropertyName("recordExportable")]
    [ColumnEx("recordExportable", Order = 17, TypeName = "NUMERIC")]
    public bool RecordExportable { get; set; } = false;

    //rights[].entity	オブジェクト	アクセス権の設定の対象
    //rights[].entity.type	文字列	アクセス権の設定対象の種類
    //rights[].entity.code	文字列	アクセス権の設定対象のコード
    [JsonPropertyName("entity")]
    [ColumnEx("entity", Order = 50, TypeName = "TEXT", IsExtract = true)]
    public EntityValue Entity { get; set; } = new();


}
