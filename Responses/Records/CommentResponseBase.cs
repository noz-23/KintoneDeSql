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
using KintoneDeSql.Extensions;
using KintoneDeSql.Responses.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace KintoneDeSql.Responses.Records;

/// <summary>
/// https://cybozu.dev/ja/kintone/docs/rest-api/records/get-comments/
/// </summary>
internal class CommentResponseBase:BaseToData
{
    // records 配列（オブジェクト）	レコードの一覧
    [JsonPropertyName("comments")]
    public List<CommentValue> ListComment { get; set; } = new();

    #region NoJson
    /// <summary>
    /// 上位のアプリ ID
    /// </summary>
    [ColumnEx("appId", Order = 1, TypeName = "TEXT", IsUnique = true)]
    public string AppId { get; set; } = string.Empty;

    /// <summary>
    /// 上位のレコード ID
    /// </summary>
    [ColumnEx("recordId", Order = 2, TypeName = "TEXT", IsUnique = true)]
    public string RecordId { get; set; } = string.Empty;
    #endregion
}
