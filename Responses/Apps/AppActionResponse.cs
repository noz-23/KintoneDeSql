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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace KintoneDeSql.Responses.Apps;

/// <summary>
/// https://cybozu.dev/ja/kintone/docs/rest-api/apps/settings/get-action-settings/
/// </summary>
internal class MappingResponse: BaseToData
{
    //actions.アクション名.mappings[].srcType	文字列	コピー元の種類
    [JsonPropertyName("srcType")]
    [ColumnEx("srcType", Order = 100, TypeName = "TEXT")]
    public string SrcType { get; set; } = string.Empty;

    //actions.アクション名.mappings[].srcField	文字列	コピー元に指定されたフィールドのフィールドコード
    [JsonPropertyName("srcField")]
    [ColumnEx("srcField", Order = 101, TypeName = "TEXT")]
    public string SrcField { get; set; } = string.Empty;

    //actions.アクション名.mappings[].destField	文字列	コピー先に指定されたフィールドのフィールドコード
    [JsonPropertyName("destField")]
    [ColumnEx("destField", Order = 102, TypeName = "TEXT")]
    public string DestField { get; set; } = string.Empty;
}
internal class AppActionResponse: BaseToData
{
    //actions.アクション名.name	文字列	アクション名
    [JsonPropertyName("name")]
    [ColumnEx("name", Order = 2, TypeName = "TEXT")]
    public string Name { get; set; } = string.Empty;

    //actions.アクション名.id	文字列	アクションID
    [JsonPropertyName("id")]
    [ColumnEx("id", Order = 1, TypeName = "TEXT")]
    public string Id { get; set; } = string.Empty;

    //actions.アクション名.index	文字列	アクションの並び順
    [JsonPropertyName("index")]
    [ColumnEx("index", Order = 3, TypeName = "TEXT")]
    public string Index { get; set; } = string.Empty;

    //actions.アクション名.destApp	オブジェクト	コピー先のアプリの情報
    //actions.アクション名.destApp.app	文字列	コピー先のアプリのアプリID
    //actions.アクション名.destApp.code	文字列	コピー先のアプリのアプリコード
    [JsonPropertyName("destApp")]
    [ColumnEx("destApp", Order = 10,IsExtract =true)]
    public DestApp DestApp { get; set; } = new DestApp();


    //actions.アクション名.mappings	配列	フィールドの関連付けの一覧
    [JsonPropertyName("mappings")]
    public List<MappingResponse> ListMapping { get; set; } = new();

    //actions.アクション名.entities	配列	アクションの利用者の一覧
    //actions.アクション名.entities[].type	文字列	アクションの利用者の種類
    //actions.アクション名.entities[].code	文字列	アクションの利用者のコード
    [JsonPropertyName("entities")]
    public List<EntryValue> Entities { get; set; } = new();

    //revision	文字列	アプリの設定のリビジョン番号
    [JsonPropertyName("revision")]
    [ColumnEx("revision", Order = 10, TypeName = "TEXT")]
    public string Revision { get; set; } = string.Empty;
}
