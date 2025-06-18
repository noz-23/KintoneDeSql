/*
 * Kintone De Sql
 * Copyright (c) 2025 noz-23
 *  https://github.com/noz-23/
 * 
 * Licensed under the MIT License 
 * 
 */
using KintoneDeSql.Attributes;
using KintoneDeSql.Data;
using KintoneDeSql.Extensions;
using KintoneDeSql.Interface;
using KintoneDeSql.Managers;
using KintoneDeSql.Properties;
using KintoneDeSql.Responses.Commons;
using KintoneDeSql.Responses.Records;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using System.Windows.Documents;

namespace KintoneDeSql.Responses.Apps.Forms;


/// <summary>
/// https://cybozu.dev/ja/kintone/docs/rest-api/apps/form/get-form-fields/
/// </summary>

[Table($"{SQLiteManager.SUB_DATABASE}.appFormFields_Option")]

internal class AppFormFieldResponseOption: AppFormFieldResponseBase, ISqlTable
{
    //
    #region ISqlTable
    public static string TableName(bool withCamma_) => typeof(AppFormFieldResponseOption).TableName(withCamma_);
    public static IEnumerable<string> ListCreateHeader(bool withCamma_) => MemberInfoExtension.ListCreateHeader(_listColumn(), withCamma_);
    public static IEnumerable<string> ListInsertHeader(bool withCamma_) => MemberInfoExtension.ListInsertHeader(_listColumn(), withCamma_);

    private static IEnumerable<ColumnData> _listColumn()
    {
        const int _SORT = 100;
        var rtn = typeof(AppFormFieldResponseOption).ListColumn();
        rtn.AddRange(typeof(AppFormFieldValueBase).ListColumn(string.Empty, _SORT));
        rtn.AddRange(typeof(OptionValue).ListColumn("options_", _SORT+ _SORT));
        return rtn;
    }
    public virtual IEnumerable<IEnumerable<string>> ListInsertValue(bool withCamma_) => _listInsertValue(ListProperty, withCamma_);
    public IEnumerable<IEnumerable<string>> ListInsertValueOption(bool withCamma_) => _listInsertValue(ListProperty, withCamma_);
    private IEnumerable<IEnumerable<string>> _listInsertValue(Dictionary<string, AppFormFieldValue> listFormField_, bool withCamma_, string subFieldKey_ = "")
    {
        var rtn = new List<IEnumerable<string>>();

        foreach (var field in listFormField_)
        {

            field.Value.MainKey = string.IsNullOrEmpty(subFieldKey_) == true ? field.Key : subFieldKey_;
            field.Value.SubKey = string.IsNullOrEmpty(subFieldKey_) == true ? string.Empty : field.Key;
            foreach (var option in field.Value.ListOption)
            {
                option.Value.Key =option.Key;

                var list = ListValue(withCamma_);
                //
                list.AddRange(field.Value.ListValueBase(withCamma_));
                list.AddRange(option.Value.ListValue(withCamma_));
                rtn.Add(list);
            }
            //

            if (field.Value.Type == "SUBTABLE")
            {
                rtn.AddRange(_listInsertValue(field.Value.ListField, withCamma_, field.Key));
            }
        }
        return rtn;
    }
    #endregion
}
