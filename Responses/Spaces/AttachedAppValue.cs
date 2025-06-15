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
using KintoneDeSql.Properties;
using KintoneDeSql.Responses.Records;
using System.Text.Json.Serialization;

namespace KintoneDeSql.Responses.Spaces;

/// <summary>
/// https://cybozu.dev/ja/kintone/docs/rest-api/spaces/get-space/
/// </summary>
internal class AttachedAppValue : BaseToData
{
    //attachedApps[].threadId	文字列	スレッドID
    [JsonPropertyName("threadId")]
    [ColumnEx("threadId", Order = 10, TypeName = "TEXT", IsUnique = true)]
    public string ThreadId { get; set; } = string.Empty;

    //attachedApps[].appId	文字列	アプリID
    [JsonPropertyName("appId")]
    [ColumnEx("appId", Order = 11, TypeName = "TEXT", IsUnique = true)]
    public string AppId { get; set; } = string.Empty;

    //attachedApps[].code	文字列	アプリコード
    [JsonPropertyName("code")]
    [ColumnEx("code", Order = 20, TypeName = "TEXT")]
    public string Code { get; set; } = string.Empty;

    //attachedApps[].name	文字列	アプリの名前
    [JsonPropertyName("name")]
    [ColumnEx("name", Order = 21, TypeName = "TEXT")]
    public string Name { get; set; } = string.Empty;

    //attachedApps[].description	文字列	アプリの説明
    [JsonPropertyName("description")]
    [ColumnEx("description", Order = 30, TypeName = "TEXT")]
    public string Description { get; set; } = string.Empty;

    //attachedApps[].createdAt	文字列	アプリの作成日時
    [JsonPropertyName("createdAt")]
    [ColumnEx("createdAt", Order = 100, TypeName = "TEXT")]
    public string CreatedAt { get; set; } = string.Empty;

    //attachedApps[].creator	オブジェクト	アプリの作成者情報
    //attachedApps[].creator.code	文字列	作成者のコード
    //attachedApps[].creator.name	文字列	作成者の名前
    [JsonPropertyName("creator")]
    [ColumnEx("creator", Order = 101, TypeName = "TEXT", IsExtract = true)]
    public CreatorValue Creator { get; set; } = new();

    //attachedApps[].modifiedAt	文字列	アプリの更新日時
    [JsonPropertyName("modifiedAt")]
    [ColumnEx("modifiedAt", Order = 200, TypeName = "TEXT")]
    public string ModifiedAt { get; set; } = string.Empty;

    //attachedApps[].modifier	オブジェクト	アプリの更新者情報
    //attachedApps[].modifier.code	文字列	更新者のコード
    //attachedApps[].modifier.name	文字列	更新者の名前
    [JsonPropertyName("modifier")]
    [ColumnEx("modifier", Order = 201, TypeName = "TEXT", IsExtract = true)]
    public ModifierValue Modifier { get; set; } = new();
    public override string ToString()
    {
        switch (Settings.Default.AttachedAppPrimary)
        {
            case "name": return this.Name;
            case "code": return this.Code;
            case "appId": return this.AppId;
            default: break;
        }
        return $"{this.Code}\t{this.Name}\t{this.AppId}";
    }
}