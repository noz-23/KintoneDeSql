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
using System.Text.Json.Serialization;

namespace KintoneDeSql.Responses.Apps.Permissions;

/// <summary>
/// https://cybozu.dev/ja/kintone/docs/rest-api/apps/settings/get-record-permissions/
/// </summary>
internal class RecordNotificationValue : BaseToData
{
    //rights[].filterCond	文字列	レコードの条件
    [JsonPropertyName("filterCond")]
    [ColumnEx("filterCond", Order = 10, TypeName = "TEXT")]
    public string FilterCond { get; set; } = string.Empty;

    //rights[].entities	配列	アクセス権の設定対象の一覧
    [JsonPropertyName("entities")]
    public List<RecordNotificationValueBase> ListEntity { get; set; } = new();
}
