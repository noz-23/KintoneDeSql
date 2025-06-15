/*
 * Kintone De Sql
 * Copyright (c) 2025 noz-23
 *  https://github.com/noz-23/
 * 
 * Licensed under the MIT License 
 * 
 */
namespace KintoneDeSql.Responses.Cybozu.Users;

/// <summary>
/// https://cybozu.dev/ja/common/docs/user-api/users/get-users/
/// https://cybozu.dev/ja/common/docs/user-api/overview/data-structure/#custom-item-value
/// </summary>
internal class CustomItemValueList : List<CustomItemValue>
{
    public override string ToString()
    {
        return string.Join(",", this.Select(x_ => x_.Value));
    }
}
