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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace KintoneDeSql.Responses.Apps.Forms;

/// <summary>
/// https://cybozu.dev/ja/kintone/docs/rest-api/apps/form/get-form-fields/
/// </summary>
internal class AppFormFieldResponseBase: BaseToData
{
    // revision	文字列	アプリの設定のリビジョン番号
    [JsonPropertyName("revision")]
    [ColumnEx("revision", Order = 2, TypeName = "TEXT")]
    public string Revision { get; set; } = string.Empty;

    // properties	オブジェクト	フィールドの設定
    [JsonPropertyName("properties")]
    public Dictionary<string, AppFormFieldValue> ListProperty { get; set; } = new();

    #region NoJson
    public string AppId
    {
        get => _appId;
        set
        {
            _appId = value;
            foreach (var property in ListProperty.Values)
            {
                property.AppId = value;
                foreach (var field in property.ListField.Values)
                {
                    field.AppId = value;
                }
            }
        }
    }
    private string _appId = string.Empty;
    #endregion

}
