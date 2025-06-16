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
using static Dapper.SqlMapper;

namespace KintoneDeSql.Responses.Apps.Permissions;

/// <summary>
/// https://cybozu.dev/ja/kintone/docs/rest-api/apps/settings/get-field-permissions/
/// </summary>
internal class FieldNotificationValue : BaseToData
{
    //rights[].code	文字列	アクセス権を設定するフィールドコード
    [JsonPropertyName("code")]
    [ColumnEx("code", Order = 10, TypeName = "TEXT")]
    public string Code { get; set; } = string.Empty;

    //rights[].entities	配列	アクセス権の設定対象の一覧
    [JsonPropertyName("entities")]
    public List<FieldNotificationValueBase> ListEntity { get; set; } = new();

    public override string ToString()
    {
        return Code.ToString();
    }
}
