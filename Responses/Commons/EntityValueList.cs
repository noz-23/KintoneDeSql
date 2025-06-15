/*
 * Kintone De Sql
 * Copyright (c) 2025 noz-23
 *  https://github.com/noz-23/
 * 
 * Licensed under the MIT License 
 * 
 */
namespace KintoneDeSql.Responses.Commons;

internal class EntityValueList:List<EntityValue>
{
    public override string ToString()=> string.Join("\n", this.Select(entity_ => entity_.ToString()));
}
