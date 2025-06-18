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
using KintoneDeSql.Managers;
using KintoneDeSql.Properties;
using KintoneDeSql.Responses.Records;
using System.Text.Json.Serialization;
using System.Windows.Documents;

namespace KintoneDeSql.Responses.Apps.Forms;

/// <summary>
/// https://cybozu.dev/ja/kintone/docs/rest-api/apps/form/get-form-fields/
/// </summary>
internal class AppFormFieldResponseBase: BaseToData
{
    // revision	文字列	アプリの設定のリビジョン番号
    [JsonPropertyName("revision")]
    [ColumnEx("revision", Order = 2, TypeName = "TEXT")]
    public string Revision { get; set; } = string.Empty;

    // properties	オブジェクト	フィールドの設定
    [JsonPropertyName("properties")]
    public Dictionary<string, AppFormFieldValue> ListProperty { get; set; } = new();

    #region NoJson
    public string AppId
    {
        get => _appId;
        set
        {
            _appId = value;
            foreach (var property in ListProperty.Values)
            {
                property.AppId = value;
                foreach (var field in property.ListField.Values)
                {
                    field.AppId = value;
                }
            }
        }
    }
    private string _appId = string.Empty;
    #endregion


    #region AppTable
    /// <summary>
    /// Kintone Appテーブル名
    /// </summary>
    /// <param name="withCamma_"></param>
    /// <returns></returns>
    private string _appTableName(bool withCamma_)
    {
        var tableName = SettingManager.Instance.TableName(AppId);
        return withCamma_ == true ? $"'{tableName}'" : tableName;
    }

    /// <summary>
    /// Kintone Appテーブル作成
    /// </summary>
    /// <param name="withCamma_"></param>
    /// <returns></returns>
    public void CreateAppTable(bool withCamma_)
    {
        var tableName = _appTableName(false);
        var listColumn = typeof(_appTable).ListCreateHeader(withCamma_,new List<string>());
        var listUnique = typeof(_appTable).ListUniqueHeader(withCamma_);
        //
        foreach (var field in ListProperty)
        {
            field.Value.AppId = AppId;
            if (_listSetList.TryGetValue(field.Value.Type, out var run) == true)
            {
                listColumn.AddRange(run(field, tableName, withCamma_));
                continue;
            }
            var fieldName = withCamma_ == true ? $"'{field.Key}'" : field.Key;
            listColumn.Add($"{fieldName} TEXT");

            if (field.Value.Unique ==true)
            {
                // Unique設定
                listUnique.Add(fieldName);
            }
        }
        //
        tableName = ( withCamma_ == true) ? $"'{tableName}'" : tableName;
        if (listUnique.Any() == true)
        {
            listColumn.Add($"UNIQUE({string.Join(",", listUnique)})");
        }
        //
        SQLiteManager.Instance.DropTable(tableName);
        SQLiteManager.Instance.CreateTable(tableName, listColumn);
    }

    /// <summary>
    /// アプリTableの共通カラム
    /// https://cybozu.dev/ja/kintone/docs/overview/field-types/
    /// </summary>
    private class _appTable
    {
        /// <summary>
        /// 上位のApp ID
        /// </summary>
        [ColumnEx("appId", Order = 1, TypeName = "TEXT" ,IsUnique =true)]
        public string AppId { get; set; } = string.Empty;

        /// <summary>
        /// レコードID
        /// レコードID	__ID__
        /// </summary>
        [ColumnEx("$id", Order = 2, TypeName = "TEXT", IsUnique = true)]
        public string TableId { get; set; } = string.Empty;

        /// <summary>
        /// リビジョン番号
        /// リビジョン	__REVISION__
        /// </summary>
        [ColumnEx("$revision", Order = 3, TypeName = "TEXT")]
        public string Revision { get; set; } = string.Empty;
    }

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
    #endregion
}
