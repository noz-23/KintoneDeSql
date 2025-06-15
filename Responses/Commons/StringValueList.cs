/*
 * Kintone De Sql
 * Copyright (c) 2025 noz-23
 *  https://github.com/noz-23/
 * 
 * Licensed under the MIT License 
 * 
 */
using KintoneDeSql.Attributes;

namespace KintoneDeSql.Responses.Commons;

internal class RequiredValueList : StringValueList
{
    #region NoJson
    // カラムのためで利用しない
    [ColumnEx("required", Order = 1, TypeName = "TEXT", IsUnique = true)]
    public string Required { get; set; } = string.Empty;
    #endregion
}
internal class FieldValueList : StringValueList
{
    #region NoJson
    // カラムのためで利用しない
    [ColumnEx("field", Order = 1, TypeName = "TEXT")]
    public string Field { get; set; } = string.Empty;
    #endregion
}
internal class StringValueList: List<string>
{
    public IEnumerable<string> ListValue(bool withComma_)=> this.Select(val_=>(withComma_ ==true)?$"'{val_}'" : val_);

    public override string ToString()
    {
        return string.Join("\n", this);
    }
}
