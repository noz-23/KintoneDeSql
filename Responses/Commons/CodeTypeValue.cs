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
using System.Text.Json.Serialization;

namespace KintoneDeSql.Responses.Commons;

internal class MentionValue : CodeTypeValue
{
    public override string ToString()
    {
        switch (Settings.Default.MentionPrimary)
        {
            case "type": return this.Type;
            case "code": return this.Code;
            default: break;
        }
        var list = this.ListValue(false);
        return string.Join("\t", list);
    }
}
internal class EntityValue : CodeTypeValue
{
    public override string ToString()
    {
        switch (Settings.Default.EntityPrimary)
        {
            case "type": return this.Type;
            case "code": return this.Code;
            default: break;
        }
        var list = this.ListValue(false);
        return string.Join("\t", list);
    }
}

internal class CodeTypeValue : BaseToData
{
    // properties.フィールドコード.entities[].code 文字列 選択肢のユーザーのログイン名、またはグループや組織のコード
    // comments[].mentions[].code	文字列	宛先のユーザー／組織／グループコード
    [JsonPropertyName("code")]
    [ColumnEx("code", Order = 1, TypeName = "TEXT",IsUnique =true)]
    public string Code { get; set; } = string.Empty;

    // properties.フィールドコード.entities[].type 文字列 値の種類
    // comments[].mentions[].type	文字列	宛先のユーザー／組織／グループの種類
    [JsonPropertyName("type")]
    [ColumnEx("type", Order = 2, TypeName = "TEXT", IsUnique = true)]
    public string Type { get; set; } = string.Empty;
}
