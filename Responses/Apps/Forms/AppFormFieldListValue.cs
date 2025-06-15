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

namespace KintoneDeSql.Responses.Apps.Forms;

/// <summary>
/// https://cybozu.dev/ja/kintone/docs/rest-api/apps/form/get-form-fields/
/// </summary>
internal class AppFormFieldListValue : AppFormFieldValueBase
{
    //properties.フィールドコード.entities[].code 文字列 選択肢のユーザーのログイン名、またはグループや組織のコード
    [JsonPropertyName("entities")]
    [ColumnEx("entities", Order = 50, TypeName = "TEXT")]
    public EntityValueList ListEntitry { get; set; } = new();

    // properties.フィールドコード.options	オブジェクト	選択肢の設定
    [JsonPropertyName("options")]
    public Dictionary<string,OptionValue> ListOption { get; set; } = new();
}
