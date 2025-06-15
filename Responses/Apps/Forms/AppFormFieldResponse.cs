/*
 * Kintone De Sql
 * Copyright (c) 2025 noz-23
 *  https://github.com/noz-23/
 * 
 * Licensed under the MIT License 
 * 
 */
using KintoneDeSql.Data;
using KintoneDeSql.Extensions;
using KintoneDeSql.Interface;
using KintoneDeSql.Managers;
using System.ComponentModel.DataAnnotations.Schema;

namespace KintoneDeSql.Responses.Apps.Forms;
/// <summary>
/// https://cybozu.dev/ja/kintone/docs/rest-api/apps/form/get-form-fields/
/// </summary>
[Table($"{SQLiteManager.SUB_DATABASE}.appFormFields")]
internal class AppFormFieldResponse: AppFormFieldResponseDisplayField, ISqlTable
{
    //
    #region ISqlTable
    public static new string TableName(bool withCamma_) => typeof(AppFormFieldResponse).TableName(withCamma_);
    public static new IEnumerable<string> ListCreateHeader(bool withCamma_) => MemberInfoExtension.ListCreateHeader(_listColumn(), withCamma_);
    public static new IEnumerable<string> ListInsertHeader(bool withCamma_) => MemberInfoExtension.ListInsertHeader(_listColumn(), withCamma_);
    private static IEnumerable<ColumnData> _listColumn()
    {
        const int _SORT = 100;
        var rtn = typeof(AppFormFieldResponse).ListColumn();
        rtn.AddRange(typeof(AppFormFieldValue).ListColumn(string.Empty, _SORT));
        return rtn;
    }
    public override IEnumerable<IEnumerable<string>> ListInsertValue(bool withCamma_) => _listInsertValue(ListProperty, withCamma_);

    private IEnumerable<IEnumerable<string>> _listInsertValue(Dictionary<string, AppFormFieldValue> listFormField_, bool withCamma_, string subFieldKey_ = "")
    {
        var rtn = new List<IEnumerable<string>>();

        foreach (var field in listFormField_)
        {
            field.Value.MainKey = string.IsNullOrEmpty(subFieldKey_) == true ? field.Key : subFieldKey_;
            field.Value.SubKey = string.IsNullOrEmpty(subFieldKey_) == true ? string.Empty : field.Key;

            var list = ListValue(withCamma_);
            //
            list.AddRange(field.Value.ListValue(withCamma_));
            rtn.Add(list);
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
