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


    #region AppTable
    private string _appTableName(bool withCamma_)
    {
        var tableName = SettingManager.Instance.TableName(AppId);
        return withCamma_ == true ? $"'{tableName}'" : tableName;
    }

    public void CreateAppTable(bool withCamma_)
    {
        var appTableName = _appTableName(false);
        var listApp = typeof(_appTable).ListCreateHeader(withCamma_);
        //
        foreach (var field in ListProperty)
        {
            field.Value.AppId = AppId;

            if (_listSetList.TryGetValue(field.Value.Type, out var run) == true)
            {
                listApp.AddRange(run(field, appTableName, withCamma_));
                continue;
            }
            var fieldName = withCamma_ == true ? $"'{field.Key}'" : field.Key;
            listApp.Add($"{fieldName} TEXT");
        }
        //
        appTableName = withCamma_ == true ? $"'{appTableName}'" : appTableName;
        SQLiteManager.Instance.DropTable(appTableName);
        SQLiteManager.Instance.CreateTable(appTableName, listApp);
    }

    /// <summary>
    /// アプリTableの共通カラム
    /// </summary>
    private class _appTable
    {
        /// <summary>
        /// 上位のApp ID
        /// </summary>
        [ColumnEx("appId", Order = 1, TypeName = "TEXT")]
        public string AppId { get; set; } = string.Empty;

        /// <summary>
        /// リビジョン番号
        /// </summary>
        [ColumnEx("$revision", Order = 3, TypeName = "TEXT")]
        public string Revision { get; set; } = string.Empty;

        /// <summary>
        /// レコードID
        /// </summary>
        [ColumnEx("$id", Order = 2, TypeName = "TEXT")]
        public string TableId { get; set; } = string.Empty;

    }

    #endregion

    public delegate IEnumerable<string> SetColumn(KeyValuePair<string, AppFormFieldValue> pair, string tableName_, bool withCamma_);
    private static Dictionary<string, SetColumn> _listSetList = new()
    {
        { "CREATOR",(pair_,tableName_,withCamma_)=>MemberInfoExtension.ListCreateHeader(typeof(CreatorValue).ListColumn($"{pair_.Key}_"),withCamma_)
        },
        { "MODIFIER",(pair_,tableName_,withCamma_)=>MemberInfoExtension.ListCreateHeader(typeof(ModifierValue).ListColumn($"{pair_.Key}_"),withCamma_)
        },
        { "SUBTABLE", (pair_,tableName_,withCamma_)=>
            {
                // サブテーブル作成処理
                var subTableName = $"{tableName_}_{pair_.Key}";
                var name = withCamma_ == true ? $"'{subTableName}'" : subTableName;
                var subTableId = withCamma_ == true ? $"'{Resource.COLUMN_SUB_TABLE_ID}'" : Resource.COLUMN_SUB_TABLE_ID;
                //
                var listSub =BaseFieldValue.ListSubDefaultCreateHeader(withCamma_);
                listSub.Add($"{subTableId} TEXT");               
                //
                foreach (var subfield in pair_.Value.ListField)
                {
                    var subFieldName = withCamma_ == true ? $"'{subfield.Key}'" : subfield.Key;
                    listSub.Add($"{subFieldName} TEXT");
                }
                SQLiteManager.Instance.DropTable(name);
                SQLiteManager.Instance.CreateTable(name, listSub);
                //
                SettingManager.Instance.ListSubTableView.Add(new(){AppId =pair_.Value.AppId,TableName= subTableName,Name=pair_.Key});
                //
                // 項目は項目で登録
                var subfieldName = withCamma_ == true ? $"'{pair_.Key}'": pair_.Key;
                return new List<string>() {$"{subfieldName} TEXT" };
            }
        }
    };

}
