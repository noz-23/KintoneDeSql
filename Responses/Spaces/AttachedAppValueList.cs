/*
 * Kintone De Sql
 * Copyright (c) 2025 noz-23
 *  https://github.com/noz-23/
 * 
 * Licensed under the MIT License 
 * 
 */
namespace KintoneDeSql.Responses.Spaces;

/// <summary>
/// https://cybozu.dev/ja/kintone/docs/rest-api/spaces/get-space/
/// </summary>
internal class AttachedAppValueList : List<AttachedAppValue>
{   
    public override string ToString()
    {
        return string.Join("\n",this.Select(app_ => app_.ToString()));
    }
}
