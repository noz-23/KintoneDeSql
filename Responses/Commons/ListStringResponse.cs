/*
 * Kintone De Sql
 * Copyright (c) 2025 noz-23
 *  https://github.com/noz-23/
 * 
 * Licensed under the MIT License 
 * 
 */
namespace KintoneDeSql.Responses.Commons;

internal class ListStringResponse: List<string>
{
    public override string ToString()
    {
        return string.Join("\n", this);
    }
}
