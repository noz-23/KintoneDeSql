/*
 * Kintone De Sql
 * Copyright (c) 2025 noz-23
 *  https://github.com/noz-23/
 * 
 * Licensed under the MIT License 
 * 
 */
using KintoneDeSql.Enums;
using System.Text.Json;

namespace KintoneDeSql.Responses.Records;

/// <summary>
/// https://cybozu.dev/ja/kintone/docs/overview/field-types/
/// </summary>
internal sealed partial class FieldValueRegist
{
    private bool _recordNumber = Regist("RECORD_NUMBER", (json_) => new StringFieldValue(json_));
    private bool _id = Regist("$id", (json_) => new StringFieldValue(json_));
    private bool _id_ = Regist("__ID__", (json_) => new StringFieldValue(json_));
    private bool _revision = Regist("__REVISION__", (json_) => new StringFieldValue(json_));
    //
    private bool _createTime = Regist("CREATED_TIME", (json_) => new StringFieldValue(json_));
    private bool _updatedTime = Regist("UPDATED_TIME", (json_) => new StringFieldValue(json_));
    //
    private bool _singleLineText = Regist("SINGLE_LINE_TEXT", (json_) => new StringFieldValue(json_));
    private bool _multiLineText = Regist("MULTI_LINE_TEXT", (json_) => new StringFieldValue(json_));
    private bool _richText = Regist("RICH_TEXT", (json_) => new StringFieldValue(json_));
    private bool _number = Regist("NUMBER", (json_) => new StringFieldValue(json_));
    private bool _calc = Regist("CALC", (json_) => new StringFieldValue(json_));
    //
    private bool _radioButton = Regist("RADIO_BUTTON", (json_) => new StringFieldValue(json_));
    private bool _dropDown = Regist("DROP_DOWN", (json_) => new StringFieldValue(json_));
    //
    private bool _status = Regist("STATUS", (json_) => new StringFieldValue(json_));
    //
    private bool _date = Regist("DATE", (json_) => new StringFieldValue(json_));
    private bool _time = Regist("TIME", (json_) => new StringFieldValue(json_));
    private bool _dateTime = Regist("DATETIME", (json_) => new StringFieldValue(json_));
    //
    private bool _link = Regist("LINK", (json_) => new StringFieldValue(json_));
    //
}

internal class StringFieldValue : BaseFieldValue
{
    public StringFieldValue(string json_):base(json_, FieldToDatabaseTypeEnum.TEXT)
    {
        _value = (string.IsNullOrEmpty(json_) == true) ? json_: JsonSerializer.Deserialize<string>(json_, Ooptions) ?? json_;
    }

    private string _value;
    public override string ToString()
    {
        return _value;
    }

    public int Length { get => _value.Length; }

    public char this[int index_] { get => _value[index_]; }
}
