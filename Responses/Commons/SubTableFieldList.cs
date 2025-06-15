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
    private bool _subTable = Regist("SUBTABLE", (json_) => new SubTableFieldList(json_));
}
internal class SubTableFieldList : BaseFieldValue,IList<SubTableFieldValue>
{
    public SubTableFieldList(string json_) : base(json_, FieldToDatabaseTypeEnum.SUBTABLE)
    {
        _listValue = JsonSerializer.Deserialize<List<SubTableFieldValue>>(json_,Ooptions) ?? new();
    }

    private List<SubTableFieldValue> _listValue =new();

    #region IList
    public SubTableFieldValue this[int index_] { get => _listValue[index_]; set => _listValue[index_] = value; }

    public int Count => _listValue.Count;

    public bool IsReadOnly => false;

    public void Add(SubTableFieldValue item_) => _listValue.Add(item_);

    public void Clear() => _listValue.Clear();

    public bool Contains(SubTableFieldValue item_) => _listValue.Contains(item_);

    public void CopyTo(SubTableFieldValue[] array_, int arrayIndex_) => _listValue.CopyTo(array_, arrayIndex_);

    public IEnumerator<SubTableFieldValue> GetEnumerator() => _listValue.GetEnumerator();

    public int IndexOf(SubTableFieldValue item_) => _listValue.IndexOf(item_);
    public void Insert(int index_, SubTableFieldValue item_) => _listValue.Insert(index_, item_);
    public bool Remove(SubTableFieldValue item_) => _listValue.Remove(item_);

    public void RemoveAt(int index_) => _listValue.RemoveAt(index_);
    IEnumerator IEnumerable.GetEnumerator() => _listValue.GetEnumerator();

    #endregion

    #region SubTable
    /// <summary>
    /// レコードからテーブル作成
    /// 基本は　FormField で作成するので未使用
    /// Api Key 利用時
    /// </summary>

    public override string SubTableName(bool withCamma_)
    {
        var tableName = $"{SettingManager.Instance.TableName(AppId)}_{MainFieldName}";
        return (withCamma_ ==true) ? $"'{tableName}'" : tableName;
    }

    public override IList<string> ListSubCreateHeader(bool withCamma_)
    {
        var rtn = ListSubDefaultInsertHeader(withCamma_);

        rtn.AddRange(typeof(SubTableFieldValue).ListInsertHeader(withCamma_));

        var first = _listValue.FirstOrDefault();
        if (first == null)
        {
            return rtn;
        }
        rtn.AddRange(first.ListSubCreateHeader(withCamma_));

        return rtn;
    }

    public override IList<string> ListSubInsertHeader(bool withCamma_)
    {
        var rtn = ListSubDefaultInsertHeader(withCamma_);

        rtn.AddRange(typeof(SubTableFieldValue).ListInsertHeader(withCamma_));

        var first = _listValue.FirstOrDefault();
        if (first == null)
        {
            return rtn;
        }
        rtn.AddRange(first.ListSubInsertHeader(withCamma_));

        return rtn;
    }

    public override IEnumerable<IEnumerable<string>> ListSubValue(bool withCamma_)
    {
        var rtn = new List<IList<string>>();
        foreach (var record in _listValue)
        {
            var list = ListSubDefaultValue(withCamma_);
            list.AddRange(record.ListValue(withCamma_));

            rtn.Add(list);
        }

        return rtn;
    }
    #endregion

    public override string ToString()
    {
        return $"Count {_listValue.Count}";
    }
}