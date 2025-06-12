/*
 * Kintone De Sql
 * Copyright (c) 2025 noz-23
 *  https://github.com/noz-23/
 * 
 * Licensed under the MIT License 
 * 
 */
using KintoneDeSql.Attributes;
using KintoneDeSql.Responses.Records;
using System.Text.Json.Serialization;

namespace KintoneDeSql.Responses.Commons;
internal class FileValue: BaseFieldValue
{
    // "contentType": "text/plain",
    [JsonPropertyName("contentType")]
    [ColumnEx("contentType", Order = 10, TypeName = "TEXT")]
    public string ContentType { get; set; } = string.Empty;

    // "fileKey":"201202061155587E339F9067544F1A92C743460E3D12B3297",
    [JsonPropertyName("fileKey")]
    [ColumnEx("fileKey", Order = 11, TypeName = "TEXT")]
    public string FileKey { get; set; } = string.Empty;

    // "name": "17to20_VerupLog （1）.txt",
    [JsonPropertyName("name")]
    [ColumnEx("name", Order = 12, TypeName = "TEXT")]
    public string Name { get; set; } = string.Empty;

    // "size": "23175"
    [JsonPropertyName("size")]
    [ColumnEx("size", Order = 13, TypeName = "TEXT")]
    public string Size { get; set; } = string.Empty;
}
