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
using KintoneDeSql.Responses.Records;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using System.Windows.Documents;

namespace KintoneDeSql.Responses.Apps;


/// <summary>
/// https://cybozu.dev/ja/kintone/docs/rest-api/apps/form/get-form-fields/
/// </summary>

[Table($"{SQLiteManager.SUB_DATABASE}.appFormFields")]
internal class ListAppFormFieldResponse:BaseToData ,ISqlTable
{
    // properties	オブジェクト	フィールドの設定
    [JsonPropertyName("properties")]
    public Dictionary<string, AppFormFieldResponse> ListProperty { get; set; } = new();

    // revision	文字列	アプリの設定のリビジョン番号
    [JsonPropertyName("revision")]
    [ColumnEx("revision", Order = 3, TypeName = "TEXT")]
    public string Revision { get; set; } = string.Empty;

    #region NoJson

    /// <summary>
    /// propertiesのKey
    /// </summary>
    [ColumnEx("properties_key", Order = 10, TypeName = "TEXT")]
    public string PropatiesKey { get; set; } = string.Empty;  // 実利用はなし

    /// <summary>
    /// 上位のApp ID
    /// </summary>
    [ColumnEx("appId", Order = 1, TypeName = "TEXT")]
    public string AppId { get; set; } = string.Empty;

    #endregion
    //
    #region ISqlTable
    public static string TableName(bool withCamma_) => typeof(ListAppFormFieldResponse).TableName(withCamma_);

    public static List<string> ListCreateHeader(bool withCamma_)
    {
        var list = typeof(ListAppFormFieldResponse).ListColumn();
        list.AddRange(typeof(AppFormFieldResponse).ListColumn("properties_"));
        return MemberInfoExtension.ListCreateHeader(list, withCamma_);
    }

    public static List<string> ListInsertHeader(bool withCamma_)
    {
        var list = typeof(ListAppFormFieldResponse).ListColumn();
        list.AddRange(typeof(AppFormFieldResponse).ListColumn("properties_"));
        return MemberInfoExtension.ListInsertHeader(list, withCamma_);
    }

    public List<List<string>> ListInsertValue(bool withCamma_) => _listInsertValue(ListProperty, withCamma_);

    private List<List<string>> _listInsertValue(Dictionary<string, AppFormFieldResponse> listFormField_, bool withCamma_, string subFieldKey_ = "")
    {
        var rtn = new List<List<string>>();

        foreach (var field in listFormField_)
        {
            PropatiesKey = (string.IsNullOrEmpty(subFieldKey_) == true) ? field.Key : subFieldKey_;
            field.Value.FieldKey = (string.IsNullOrEmpty(subFieldKey_) == true) ? string.Empty : field.Key;
            //

            var list = base.ListValue(withCamma_);
            list.AddRange(field.Value.ListValue(withCamma_));

            rtn.Add(list);

            if (field.Value.Type == "SUBTABLE")
            {
                rtn.AddRange(_listInsertValue(field.Value.ListField, withCamma_, PropatiesKey));
            }
        }
        return rtn;
    }
    #endregion

    #region AppTable
    private string _appTableName(bool withCamma_)
    {
        var tableName = SettingManager.Instance.TableName(AppId);
        return (withCamma_ == true) ? $"'{tableName}'" : tableName;
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
            var fieldName = (withCamma_ == true) ? $"'{field.Key}'" : field.Key;
            listApp.Add($"{fieldName} TEXT");
        }
        //
        appTableName = (withCamma_==true) ? $"'{appTableName}'" : appTableName;
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

    public delegate List<string> SetColumn(KeyValuePair<string, AppFormFieldResponse> pair, string tableName_, bool withCamma_);
    private static Dictionary<string, SetColumn> _listSetList = new()
    {
        { "CREATOR",(pair_,tableName_,withCamma_)=>MemberInfoExtension.ListCreateHeader(typeof(CreatorValue).ListColumn($"{pair_.Key}_"),withCamma_)
            //{
            //    var name = (withCamma_ == true) ? $"'{pair_.Key}_name'" : $"{pair_.Key}_name";
            //    var code = (withCamma_ == true) ? $"'{pair_.Key}_code'" : $"{pair_.Key}_code";
            //    return new List<string>() { $"{name} TEXT" , $"{code} TEXT" };
            //}
        },
        { "MODIFIER",(pair_,tableName_,withCamma_)=>  MemberInfoExtension.ListCreateHeader(typeof(ModifierValue).ListColumn($"{pair_.Key}_"),withCamma_)
            //{
            //    var name = (withCamma_ == true) ? $"'{pair_.Key}_name'" : $"{pair_.Key}_name";
            //    var code = (withCamma_ == true) ? $"'{pair_.Key}_code'" : $"{pair_.Key}_code";
            //    return new List<string>() { $"{name} TEXT" , $"{code} TEXT" };
            //}
        },
        { "SUBTABLE", (pair_,tableName_,withCamma_)=>
            {
                // サブテーブル作成処理
                var subTableName = $"{tableName_}_{pair_.Key}";
                
                var name = (withCamma_ == true) ? $"'{subTableName}'" : subTableName;
                var listSub =BaseFieldValue.ListSubCreateHeader(withCamma_);
                //

                var subTableId = (withCamma_ == true) ? $"'{Resource.COLUMN_SUB_TABLE_ID}'" : Resource.COLUMN_SUB_TABLE_ID;
                listSub.Add($"{subTableId} TEXT");               
                //
                foreach (var subfield in pair_.Value.ListField)
                {
                    var subFieldName = (withCamma_ == true) ? $"'{subfield.Key}'" : subfield.Key;
                    listSub.Add($"{subFieldName} TEXT");
                }
                SQLiteManager.Instance.DropTable(name);
                SQLiteManager.Instance.CreateTable(name, listSub);
                //
                SettingManager.Instance.ListSubTableView.Add(new(){AppId =pair_.Value.AppId,TableName= subTableName,Name=pair_.Key});
                //
                // 項目は項目で登録
                var subfieldName = (withCamma_ == true) ? $"'{pair_.Key}'": pair_.Key;
                return new List<string>() {$"{subfieldName} TEXT" };
            }
        }
    };
}
