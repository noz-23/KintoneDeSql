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
    private bool _checkBox = Regist("CHECK_BOX", (json_) => new ListCheckBoxValue(json_));
    private bool _multiSelect = Regist("MULTI_SELECT", (json_) => new ListMultiSelectValue(json_));
    private bool _category = Regist("CATEGORY", (json_) => new ListCategoryBoxValue(json_));
}

internal class ListCheckBoxValue:ListStringValue, ICreateTable
{
    private const string _TABLE_NAME = $"{SQLiteManager.FIELD_DATABASE}.checkBoxes";

    public ListCheckBoxValue(string json_):base(json_)
    {
    }
    public static string TableName(bool withCamma_) => (withCamma_==true) ? $"'{_TABLE_NAME}'" : _TABLE_NAME;
    public override string TableNameBase(bool withCamma_) => TableName(withCamma_);
}

internal class ListMultiSelectValue : ListStringValue, ICreateTable
{
    private const string _TABLE_NAME = $"{SQLiteManager.FIELD_DATABASE}.multiSelectes";

    public ListMultiSelectValue(string json_) : base(json_)
    {
    }
    public static string TableName(bool withCamma_) => (withCamma_ == true) ? $"'{_TABLE_NAME}'" : _TABLE_NAME;
    public override string TableNameBase(bool withCamma_) => TableName(withCamma_);
}
internal class ListCategoryBoxValue : ListStringValue, ICreateTable
{
    private const string _TABLE_NAME = $"{SQLiteManager.FIELD_DATABASE}.categries";

    public ListCategoryBoxValue(string json_) : base(json_)
    {
    }
    public static string TableName(bool withCamma_) => (withCamma_ == true) ? $"'{_TABLE_NAME}'" : _TABLE_NAME;

    public override string TableNameBase(bool withCamma_) => TableName(withCamma_);
}

internal class ListStringValue: BaseFieldValue//,IList<string>
{
    private const string _TEXT_COLUMN_NAME = "text";
    public ListStringValue(string json_) : base(json_, FieldToDatabaseTypeEnum.LIST)
    {
        _value = JsonSerializer.Deserialize<List<string>>(json_,Ooptions) ?? new ();
    }
    private List<string> _value;

    public static List<string> ListCreateHeader(bool withCamma_)
    {
        var rtn = ListSubCreateHeader(withCamma_);
        var columnName = (withCamma_ == true) ? $"'{_TEXT_COLUMN_NAME}'" : $"{_TEXT_COLUMN_NAME}";

        rtn.Add($"{columnName} TEXT");
        return rtn;
    }
    public override List<string> ListInsertHeaderBase(bool withCamma_)
    {
        var rtn = ListSubInsertHeader(withCamma_);
        var columnName = (withCamma_ == true) ? $"'{_TEXT_COLUMN_NAME}'" : $"{_TEXT_COLUMN_NAME}";

        rtn.Add($"{columnName}");
        return rtn;
    }
    public override List<List<string>> ListInsertValueBase(bool withCamma_)
    {
        var rtn = new List<List<string>>();
        foreach (var item in _value)
        {
            var list = ListSubValue(withCamma_);

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
