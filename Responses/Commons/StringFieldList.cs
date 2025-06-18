/*
 * Kintone De Sql
 * Copyright (c) 2025 noz-23
 *  https://github.com/noz-23/
 * 
 * Licensed under the MIT License 
 * 
 */
using KintoneDeSql.Enums;
using KintoneDeSql.Interface;
using KintoneDeSql.Managers;
using System.Text.Json;

namespace KintoneDeSql.Responses.Records;

/// <summary>
/// https://cybozu.dev/ja/kintone/docs/overview/field-types/
/// </summary>
internal sealed partial class FieldValueRegist
{
    private bool _checkBox = Regist("CHECK_BOX", (json_) => new CheckBoxValueList(json_));
    private bool _multiSelect = Regist("MULTI_SELECT", (json_) => new MultiSelectValueList(json_));
    private bool _category = Regist("CATEGORY", (json_) => new CategoryBoxValueList(json_));
}

internal class CheckBoxValueList:StringFieldList, ICreateTable
{
    private const string _TABLE_NAME = $"{SQLiteManager.FIELD_DATABASE}.checkBoxes";

    public CheckBoxValueList(string json_):base(json_)
    {
    }
    public static string TableName(bool withCamma_) => (withCamma_==true) ? $"'{_TABLE_NAME}'" : _TABLE_NAME;
    public override string SubTableName(bool withCamma_) => TableName(withCamma_);
}

internal class MultiSelectValueList : StringFieldList, ICreateTable
{
    private const string _TABLE_NAME = $"{SQLiteManager.FIELD_DATABASE}.multiSelectes";

    public MultiSelectValueList(string json_) : base(json_)
    {
    }
    public static string TableName(bool withCamma_) => (withCamma_ == true) ? $"'{_TABLE_NAME}'" : _TABLE_NAME;
    public override string SubTableName(bool withCamma_) => TableName(withCamma_);
}
internal class CategoryBoxValueList : StringFieldList, ICreateTable
{
    private const string _TABLE_NAME = $"{SQLiteManager.FIELD_DATABASE}.categries";

    public CategoryBoxValueList(string json_) : base(json_)
    {
    }
    public static string TableName(bool withCamma_) => (withCamma_ == true) ? $"'{_TABLE_NAME}'" : _TABLE_NAME;

    public override string SubTableName(bool withCamma_) => TableName(withCamma_);
}

internal class StringFieldList: BaseFieldValue
{
    private const string _TEXT_COLUMN_NAME = "text";
    public StringFieldList(string json_) : base(json_, FieldToDatabaseTypeEnum.LIST)
    {
        _value = JsonSerializer.Deserialize<List<string>>(json_,Ooptions) ?? new ();
    }
    private List<string> _value;
    public static IList<string> ListCreateHeader(bool withCamma_)
    {
        var rtn = ListSubDefaultCreateHeader(withCamma_);
        var columnName = (withCamma_ == true) ? $"'{_TEXT_COLUMN_NAME}'" : $"{_TEXT_COLUMN_NAME}";

        rtn.Add($"{columnName} TEXT");
        //
        rtn.Add($"UNIQUE({string.Join(",", ListSubDefaultInsertHeader(withCamma_))})");
        return rtn;
    }
    public override IList<string> ListSubInsertHeader(bool withCamma_)
    {
        var rtn = ListSubDefaultInsertHeader(withCamma_);
        var columnName = (withCamma_ == true) ? $"'{_TEXT_COLUMN_NAME}'" : $"{_TEXT_COLUMN_NAME}";

        rtn.Add($"{columnName}");
        return rtn;
    }
    public override IEnumerable<IEnumerable<string>> ListSubValue(bool withCamma_)
    {
        var rtn = new List<IEnumerable<string>>();
        foreach (var item in _value)
        {
            var list = ListSubDefaultValue(withCamma_);

            var val = (withCamma_ == true) ? $"'{item}'" : $"{item}";
            list.Add($"{val}");
            
            rtn.Add(list);
        }
        return rtn;
    }

    public override string ToString()
    {
        return string.Join("\n",_value);
    }
}
