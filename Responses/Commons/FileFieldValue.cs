/*
 * Kintone De Sql
 * Copyright (c) 2025 noz-23
 *  https://github.com/noz-23/
 * 
 * Licensed under the MIT License 
 * 
 */
using KintoneDeSql.Attributes;
using KintoneDeSql.Properties;
using KintoneDeSql.Responses.Records;
using System.Text.Json.Serialization;

namespace KintoneDeSql.Responses.Commons;
internal class FileFieldValue: BaseFieldValue
{
    // "contentType": "text/plain",
    [JsonPropertyName("contentType")]
    [ColumnEx("contentType", Order = 100, TypeName = "TEXT")]
    public string ContentType { get; set; } = string.Empty;

    // "fileKey":"201202061155587E339F9067544F1A92C743460E3D12B3297",
    [JsonPropertyName("fileKey")]
    [ColumnEx("fileKey", Order = 101, TypeName = "TEXT", IsUnique = true)]
    public string FileKey { get; set; } = string.Empty;

    // "name": "17to20_VerupLog （1）.txt",
    [JsonPropertyName("name")]
    [ColumnEx("name", Order = 102, TypeName = "TEXT")]
    public string Name { get; set; } = string.Empty;

    // "size": "23175"
    [JsonPropertyName("size")]
    [ColumnEx("size", Order = 103, TypeName = "TEXT")]
    public string Size { get; set; } = string.Empty;

    public override string ToString()
    {
        switch (Settings.Default.FilePrimary)
        {
            case "name": return this.Name;
            case "fileKey": return this.FileKey;
            default: break;
        }
        var list = this.ListValue(false);
        return $"{this.Name}\t{this.FileKey}";

    }
}
