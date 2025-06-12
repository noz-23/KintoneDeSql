/*
 * Kintone De Sql
 * Copyright (c) 2025 noz-23
 *  https://github.com/noz-23/
 * 
 * Licensed under the MIT License 
 * 
 */
using KintoneDeSql.Attributes;
using KintoneDeSql.Enums;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace KintoneDeSql.Responses.Records;

/// <summary>
/// https://cybozu.dev/ja/kintone/docs/overview/field-types/
/// </summary>
internal sealed partial class FieldValueRegist
{
    private bool _creaator = Regist("CREATOR", (json_) => JsonSerializer.Deserialize<CreatorValue>(json_, BaseFieldValue.Ooptions));
    private bool _modifier = Regist("MODIFIER", (json_) => JsonSerializer.Deserialize<ModifierValue>(json_, BaseFieldValue.Ooptions));
}

internal class CreatorValue : ItemValue
{
    /// 作成者
}
internal class ModifierValue : ItemValue
{
    /// 更新者
}

internal class ItemValue : BaseFieldValue
{
    public ItemValue() : base(string.Empty, FieldToDatabaseTypeEnum.EXTRACT)
    {
    }

    //apps[].creator.code 文字列 作成者のコード
    //comments[].creator.code	文字列	投稿者のコード
    [JsonPropertyName("code")]
    [ColumnEx("code", Order = 10, TypeName = "TEXT")]
    public string Code { get; set; } = string.Empty;

    //apps[].creator.name 文字列 作成者の名前
    //comments[].creator.name	文字列	投稿者名
    [JsonPropertyName("name")]
    [ColumnEx("name", Order = 11, TypeName = "TEXT")]
    public string Name { get; set; } = string.Empty;
}
