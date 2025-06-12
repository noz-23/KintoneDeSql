/*
 * Kintone De Sql
 * Copyright (c) 2025 noz-23
 *  https://github.com/noz-23/
 * 
 * Licensed under the MIT License 
 * 
 */
using KintoneDeSql.Enums;
using KintoneDeSql.Extensions;
using KintoneDeSql.Interface;
using KintoneDeSql.Managers;
using System.Text.Json;

namespace KintoneDeSql.Responses.Records;

/// <summary>
/// https://cybozu.dev/ja/kintone/docs/overview/field-types/
/// </summary>
internal sealed partial class FieldValueRegist
{
    private bool _userSelect = Regist("USER_SELECT", (json_) => new ListUserValue(json_));
    private bool _organizationSelect = Regist("ORGANIZATION_SELECT", (json_) => new ListOrganizationValue(json_));
    private bool _groupSelect = Regist("GROUP_SELECT", (json_) => new ListGroupValue(json_));
}
internal class ListUserValue : ListItemValue, ICreateTable
{
    private const string _TABLE_NAME = $"{SQLiteManager.FIELD_DATABASE}.users";
    public ListUserValue(string json_) : base(json_)
    {
    }
    public static string TableName(bool withCamma_) => (withCamma_ == true) ? $"'{_TABLE_NAME}'" : _TABLE_NAME;
    public override string TableNameBase(bool withCamma_) => TableName(withCamma_);
}

internal class ListOrganizationValue : ListItemValue, ICreateTable
{
    private const string _TABLE_NAME = $"{SQLiteManager.FIELD_DATABASE}.organization";
    public ListOrganizationValue(string json_) : base(json_)
    {
    }
    public static string TableName(bool withCamma_) => (withCamma_ == true) ? $"'{_TABLE_NAME}'" : _TABLE_NAME;
    public override string TableNameBase(bool withCamma_) => TableName(withCamma_);
}

internal class ListGroupValue : ListItemValue, ICreateTable
{
    private const string _TABLE_NAME = $"{SQLiteManager.FIELD_DATABASE}.groups";

    public ListGroupValue(string json_) : base(json_)
    {
    }
    public static string TableName(bool withCamma_) => (withCamma_ == true) ? $"'{_TABLE_NAME}'" : _TABLE_NAME;

    public override string TableNameBase(bool withCamma_) => TableName(withCamma_);
}

internal class ListItemValue: BaseFieldValue
{
    public ListItemValue(string json_) : base(json_, FieldToDatabaseTypeEnum.LIST)
    {
        _value = JsonSerializer.Deserialize<List<ItemValue>>(json_, Ooptions) ?? new();
    }
    private List<ItemValue> _value;

    public static List<string> ListCreateHeader(bool withCamma_)
    {
        var rtn = ListSubCreateHeader(withCamma_);
        rtn.AddRange(typeof(ItemValue).ListCreateHeader(withCamma_));
        return rtn;
    }
    public override List<string> ListInsertHeaderBase(bool withCamma_)
    {
        var rtn = ListSubInsertHeader(withCamma_);
        rtn.AddRange(typeof(ItemValue).ListInsertHeader(withCamma_));
        return rtn;
    }

    public override List<List<string>> ListInsertValueBase(bool withCamma_)
    {
        var rtn = new List<List<string>>();
        foreach (var item in _value)
        {
            var list = ListSubValue(withCamma_);
            list.AddRange(item.ListValue(withCamma_));
            //
            rtn.Add(list);
        }
        return rtn;
    }

    public override string ToString()
    {
        return string.Join("\n", _value.Select(x_=>x_.Name));
    }

}
