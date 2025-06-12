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
using KintoneDeSql.Managers;
using KintoneDeSql.Responses.Commons;
using System.Collections;
using System.Text.Json;

namespace KintoneDeSql.Responses.Records;

/// <summary>
/// https://cybozu.dev/ja/kintone/docs/overview/field-types/
/// </summary>
internal sealed partial class FieldValueRegist
{
    private bool _subTable = Regist("SUBTABLE", (json_) => new ListSubTableValue(json_));
}
internal class ListSubTableValue : BaseFieldValue,IList<SubTableValue>
{
    public ListSubTableValue(string json_) : base(json_, FieldToDatabaseTypeEnum.SUBTABLE)
    {
        _value = JsonSerializer.Deserialize<List<SubTableValue>>(json_,Ooptions) ?? new();
    }

    private List<SubTableValue> _value;

    public SubTableValue this[int index_] { get => _value[index_]; set => _value[index_] = value; }

    public int Count => _value.Count;

    public bool IsReadOnly => false;

    public void Add(SubTableValue item_) => _value.Add(item_);

    public void Clear() => _value.Clear();

    public bool Contains(SubTableValue item_) => _value.Contains(item_);

    public void CopyTo(SubTableValue[] array_, int arrayIndex_) => _value.CopyTo(array_, arrayIndex_);

    public IEnumerator<SubTableValue> GetEnumerator() => _value.GetEnumerator();

    public int IndexOf(SubTableValue item_) => _value.IndexOf(item_);
    public void Insert(int index_, SubTableValue item_) => _value.Insert(index_, item_);
    public bool Remove(SubTableValue item_) => _value.Remove(item_);

    public void RemoveAt(int index_) => _value.RemoveAt(index_);
    IEnumerator IEnumerable.GetEnumerator() => _value.GetEnumerator();
    public override string TableNameBase(bool withCamma_)
    {
        var tableName = $"{SettingManager.Instance.TableName(AppId)}_{MainFieldName}";
        return (withCamma_ ==true) ? $"'{tableName}'" : tableName;
    }
    public override List<string> ListInsertHeaderBase(bool withCamma_)
    {
        var rtn = ListSubInsertHeader(withCamma_);

        rtn.AddRange(typeof(SubTableValue).ListInsertHeader(withCamma_));

        var first = _value.FirstOrDefault();
        rtn.AddRange(first.ListInsertHeaderBase(withCamma_));

        return rtn;
    }

    public override List<List<string>> ListInsertValueBase(bool withCamma_)
    {
        var rtn = new List<List<string>>();
        foreach (var record in _value)
        {
            var list = ListSubValue(withCamma_);
            list.AddRange(record.ListValue(withCamma_));

            rtn.Add(list);
        }

        return rtn;
    }

    public override string ToString()
    {
        return $"Count {_value.Count}";
    }
}