/*
 * Kintone De Sql
 * Copyright (c) 2025 noz-23
 *  https://github.com/noz-23/
 * 
 * Licensed under the MIT License 
 * 
 */
using KintoneDeSql.Attributes;
using KintoneDeSql.Responses.Commons;
using System.Text.Json.Serialization;

namespace KintoneDeSql.Responses.Apps.Settings;

/// <summary>
/// https://cybozu.dev/ja/kintone/docs/rest-api/apps/settings/get-action-settings/
/// </summary>
internal class AppActionValue : AppActionValueBase
{
    //actions.アクション名.name	文字列	アクション名
    [JsonPropertyName("name")]
    [ColumnEx("name", Order = 3, TypeName = "TEXT")]
    public string Name { get; set; } = string.Empty;

    //actions.アクション名.index	文字列	アクションの並び順
    [JsonPropertyName("index")]
    [ColumnEx("index", Order = 5, TypeName = "TEXT")]
    public string Index { get; set; } = string.Empty;

    //actions.アクション名.destApp	オブジェクト	コピー先のアプリの情報
    //actions.アクション名.destApp.app	文字列	コピー先のアプリのアプリID
    //actions.アクション名.destApp.code	文字列	コピー先のアプリのアプリコード
    [JsonPropertyName("destApp")]
    [ColumnEx("destApp", Order = 20, TypeName = "TEXT", IsExtract = true)]
    public DestAppValue DestApp { get; set; } = new();

    //actions.アクション名.entities	配列	アクションの利用者の一覧
    //actions.アクション名.entities[].type	文字列	アクションの利用者の種類
    //actions.アクション名.entities[].code	文字列	アクションの利用者のコード
    [JsonPropertyName("entities")]
    [ColumnEx("entities", Order = 50, TypeName = "TEXT")]
    public EntityValueList ListEntity { get; set; } = new();

    //actions.アクション名.mappings	配列	フィールドの関連付けの一覧
    [JsonPropertyName("mappings")]
    public List<MappingValue> ListMapping { get; set; } = new();

    public override string ToString()
    {
        return $"{Name.ToString()} : {DestApp.ToString()}";
    }
}
