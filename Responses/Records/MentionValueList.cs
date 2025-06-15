/*
 * Kintone De Sql
 * Copyright (c) 2025 noz-23
 *  https://github.com/noz-23/
 * 
 * Licensed under the MIT License 
 * 
 */
using KintoneDeSql.Responses.Commons;

namespace KintoneDeSql.Responses.Records;

/// <summary>
/// https://cybozu.dev/ja/kintone/docs/rest-api/records/get-comments/
/// </summary>
internal class MentionValueList: List<MentionValue>
{
    public override string ToString()
    {
        return string.Join("\n", this.Select(mention_ => mention_.ToString()));
    }
}
