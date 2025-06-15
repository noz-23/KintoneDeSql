/*
 * Kintone De Sql
 * Copyright (c) 2025 noz-23
 *  https://github.com/noz-23/
 * 
 * Licensed under the MIT License 
 * 
 */
using KintoneDeSql.Responses.Commons;

namespace KintoneDeSql.Responses.Apps.Forms;

/// <summary>
/// https://cybozu.dev/ja/kintone/docs/rest-api/apps/form/get-form-fields/
/// </summary>
internal class DisplayFieldValueList: List<DisplayFieldValue>
{
    public override string ToString()
    {
        return string.Join("\n", this);
    }
}
