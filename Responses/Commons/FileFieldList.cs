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
using System.Linq;
using KintoneDeSql.Data;

namespace KintoneDeSql.Responses.Records;

/// <summary>
/// https://cybozu.dev/ja/kintone/docs/overview/field-types/
/// </summary>
internal sealed partial class FieldValueRegist
{
    private bool _file = Regist("FILE", (json_) => new FileFieldList(json_));
}


[Table($"{SQLiteManager.FIELD_DATABASE}.files")]
internal class FileFieldList : BaseFieldValue, ICreateTable
{
    public FileFieldList(string json_) : base(json_, FieldToDatabaseTypeEnum.LIST)
    {
        _value = JsonSerializer.Deserialize<List<FileFieldValue>>(json_, Ooptions) ?? new();
    }
    private List<FileFieldValue> _value;
    public static string TableName(bool withCamma_) => typeof(FileFieldList).TableName(withCamma_);
    public static IList<string> ListCreateHeader(bool withCamma_)
    {
        var rtn = ListSubDefaultCreateHeader(withCamma_);
        //
        var listUnique = ListSubDefaultInsertHeader(withCamma_);
        listUnique.AddRange(typeof(FileFieldValue).ListUniqueHeader(withCamma_));
        //
        rtn.AddRange(typeof(FileFieldValue).ListCreateHeader(withCamma_, listUnique));
        return rtn;
    }

    #region SubTable
    public override string SubTableName(bool withCamma_) => TableName(withCamma_);
    public override IList<string> ListSubInsertHeader(bool withCamma_)
    {
        var rtn = ListSubDefaultInsertHeader(withCamma_);
        rtn.AddRange(typeof(FileFieldValue).ListInsertHeader(withCamma_));
        return rtn;
    }
    public override IEnumerable<IEnumerable<string>> ListSubValue(bool withCamma_)
    {
        var rtn = new List<IEnumerable<string>>();
        foreach (var item in _value)
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
        return string.Join("\n", _value.Select(file_=>file_.ToString()));
    }

}
