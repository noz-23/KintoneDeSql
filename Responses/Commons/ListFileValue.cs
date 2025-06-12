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
using KintoneDeSql.Responses.Commons;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace KintoneDeSql.Responses.Records;

/// <summary>
/// https://cybozu.dev/ja/kintone/docs/overview/field-types/
/// </summary>
internal sealed partial class FieldValueRegist
{
    private bool _file = Regist("FILE", (json_) => new ListFileValue(json_));
}


[Table($"{SQLiteManager.FIELD_DATABASE}.files")]
internal class ListFileValue : BaseFieldValue, ICreateTable
{
    public ListFileValue(string json_) : base(json_, FieldToDatabaseTypeEnum.LIST)
    {
        _value = JsonSerializer.Deserialize<List<FileValue>>(json_, Ooptions) ?? new();
    }
    private List<FileValue> _value;

    public static string TableName(bool withCamma_) => typeof(ListFileValue).TableName(withCamma_);

    public override string TableNameBase(bool withCamma_) => TableName(withCamma_);

    public static List<string> ListCreateHeader(bool withCamma_)
    {
        var rtn = ListSubCreateHeader(withCamma_);
        rtn.AddRange(typeof(FileValue).ListCreateHeader(withCamma_));
        return rtn;
    }
    public override List<string> ListInsertHeaderBase(bool withCamma_)
    {
        var rtn = ListSubInsertHeader(withCamma_);
        rtn.AddRange(typeof(FileValue).ListInsertHeader(withCamma_));
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
 }
