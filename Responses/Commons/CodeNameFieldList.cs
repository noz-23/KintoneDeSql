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
using KintoneDeSql.Properties;
using System.Text.Json;

namespace KintoneDeSql.Responses.Records;

/// <summary>
/// https://cybozu.dev/ja/kintone/docs/overview/field-types/
/// </summary>
internal sealed partial class FieldValueRegist
{
    private bool _userSelect = Regist("USER_SELECT", (json_) => new UserValueList(json_));
    private bool _organizationSelect = Regist("ORGANIZATION_SELECT", (json_) => new OrganizationValueList(json_));
    private bool _groupSelect = Regist("GROUP_SELECT", (json_) => new GroupValueList(json_));
}
internal class UserValueList : CodeNameFieldList, ICreateTable
{
    private const string _TABLE_NAME = $"{SQLiteManager.FIELD_DATABASE}.users";
    public UserValueList(string json_) : base(json_)
    {
    }
    public static string TableName(bool withCamma_) => (withCamma_ == true) ? $"'{_TABLE_NAME}'" : _TABLE_NAME;
    public override string SubTableName(bool withCamma_) => TableName(withCamma_);

    public override string ToString()
    {
        return string.Join("\n", _Value.Select(x_ => x_.ToString(Settings.Default.UserPrimary)));
    }
}

internal class OrganizationValueList : CodeNameFieldList, ICreateTable
{
    private const string _TABLE_NAME = $"{SQLiteManager.FIELD_DATABASE}.organizations";
    public OrganizationValueList(string json_) : base(json_)
    {
    }
    public static string TableName(bool withCamma_) => (withCamma_ == true) ? $"'{_TABLE_NAME}'" : _TABLE_NAME;
    public override string SubTableName(bool withCamma_) => TableName(withCamma_);

    public override string ToString()
    {
        return string.Join("\n", _Value.Select(x_ => x_.ToString(Settings.Default.OrganizationPrimary)));
    }
}

internal class GroupValueList : CodeNameFieldList, ICreateTable
{
    private const string _TABLE_NAME = $"{SQLiteManager.FIELD_DATABASE}.groups";

    public GroupValueList(string json_) : base(json_)
    {
    }
    public static string TableName(bool withCamma_) => (withCamma_ == true) ? $"'{_TABLE_NAME}'" : _TABLE_NAME;

    public override string SubTableName(bool withCamma_) => TableName(withCamma_);


    public override string ToString()
    {
        return string.Join("\n", _Value.Select(x_ => x_.ToString(Settings.Default.GroupPrimary)));
    }
}

internal class CodeNameFieldList: BaseFieldValue
{
    public CodeNameFieldList(string json_) : base(json_, FieldToDatabaseTypeEnum.LIST)
    {
        _Value = JsonSerializer.Deserialize<List<CodeNameValue>>(json_, Ooptions) ?? new();
    }
    protected List<CodeNameValue> _Value;

    #region SubTable
    public static IList<string> ListCreateHeader(bool withCamma_)
    {
        var rtn = ListSubDefaultCreateHeader(withCamma_);
        //
        var listUnique = ListSubDefaultInsertHeader(withCamma_);
        listUnique.AddRange(typeof(CodeNameValue).ListUniqueHeader(withCamma_));
        //
        rtn.AddRange(typeof(CodeNameValue).ListCreateHeader(withCamma_, listUnique));
        return rtn;
    }

    public override IList<string> ListSubInsertHeader(bool withCamma_)
    {
        var rtn = ListSubDefaultInsertHeader(withCamma_);
        rtn.AddRange(typeof(CodeNameValue).ListInsertHeader(withCamma_));
        return rtn;
    }

    public override IEnumerable<IEnumerable<string>> ListSubValue(bool withCamma_)
    {
        var rtn = new List<IEnumerable<string>>();
        foreach (var item in _Value)
        {
            var list = ListSubDefaultValue(withCamma_);
            list.AddRange(item.ListValue(withCamma_));
            //
            rtn.Add(list);
        }
        return rtn;
    }
    #endregion
    public override string ToString()
    {
        return string.Join("\n", _Value.Select(x_=>x_.Name));
    }

}
