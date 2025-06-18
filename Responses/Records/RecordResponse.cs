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
using KintoneDeSql.Enums;
using KintoneDeSql.Extensions;
using KintoneDeSql.Interface;
using KintoneDeSql.Managers;
using KintoneDeSql.Properties;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace KintoneDeSql.Responses.Records;

/// <summary>
/// https://cybozu.dev/ja/kintone/docs/rest-api/records/get-records/
/// </summary>
[Table("records")]
internal class RecordResponse :BaseToData,IInsertTable
{
    // records 配列（オブジェクト）	レコードの一覧
    [JsonPropertyName("records")]
    public List<Dictionary<string, RecordFieldValue>> ListRecord { get; set; } = new();

    // totalCount  文字列 レコードの件数
    [JsonPropertyName("totalCount")]
    public string TotalCount { get; set; } = string.Empty;

    #region NoJson
    /// <summary>
    /// 上位のアプリ ID
    /// </summary>
    [ColumnEx("appId", Order = 1, TypeName = "TEXT")]
    public string AppId { get; set; } = string.Empty;
    #endregion

    #region ISqlTable
    public string TableName(bool withCamma_) =>(withCamma_==true) ? $"'{SettingManager.Instance.TableName(AppId)}'": SettingManager.Instance.TableName(AppId);

    /// <summary>
    /// レコードからテーブル作成
    /// 基本は　FormField で作成するので未使用
    /// Api Key 利用時
    /// </summary>
    /// <param name="withCamma_"></param>
    /// <returns></returns>
    public IEnumerable<string> ListCreateHeader(bool withCamma_)
    {
        var rtn = typeof(RecordResponse).ListCreateHeader(withCamma_);
        var record = ListRecord.FirstOrDefault();
        if (record == null)
        {
            return rtn;
        }
        //
        foreach (var pair in record)
        {
            var field = pair.Value;
            if (BaseFieldValue.ListToCreateHeader.TryGetValue(field.Value.FieldType, out var run) == true)
            {
                rtn.AddRange(run(field, pair.Key, withCamma_));
            }
        }
        return rtn;
    }
    public IEnumerable<string> ListInsertHeader(bool withCamma_)
    {
        var rtn = typeof(RecordResponse).ListInsertHeader(withCamma_);
        var record = ListRecord.FirstOrDefault();
        if (record == null)
        {
            return rtn;
        }
        //
        foreach (var pair in record)
        {
            var field = pair.Value;
            if (BaseFieldValue.ListToInsertHeader.TryGetValue(field.Value.FieldType, out var run) == true)
            {
                rtn.AddRange(run(field, pair.Key, withCamma_));
            }
        }
        return rtn;
    }
    public IEnumerable<IEnumerable<string>> ListInsertValue(bool withCamma_)
    {
        var rtn = new List<IList<string>>();
        foreach (var record in ListRecord)
        {
            var list = this.ListValue(withCamma_);
            foreach (var pair in record)
            {
                var field = pair.Value;
                if (field == null)
                {
                    continue;
                }
                //
                if (BaseFieldValue.ListToValue.TryGetValue(field.Value.FieldType, out var run) == true)
                {
                    list.AddRange(run(field, pair.Key, withCamma_));
                }
            }
            rtn.Add(list);
        }
        return rtn;
    }

    #endregion
    public IEnumerable<BaseFieldValue> ListSubTable()
    {
        var rtn = new List<BaseFieldValue>();
        foreach (var record in ListRecord)
        {
            var recordFieldId = record[Resource.COLUMN_MAIN_TABLE_ID];
            var recordRevision = record[Resource.COLUMN_MAIN_REVISION].Value.ToString();
            var recordId = recordFieldId?.Value?.ToString() ?? string.Empty;
            //
            foreach (var mainFieldName in record.Keys)
            {
                var mainFieldValue = record[mainFieldName].Value;

                if (mainFieldValue?.FieldType ==FieldToDatabaseTypeEnum.LIST)
                {
                    // 複数選択系は別のテーブルにも保存する
                    mainFieldValue.AppId = AppId;
                    mainFieldValue.RecordId = recordId;
                    mainFieldValue.Revision = recordRevision;
                    mainFieldValue.MainFieldName = mainFieldName;
                    rtn.Add(mainFieldValue);
                }

                if (mainFieldValue?.FieldType == FieldToDatabaseTypeEnum.SUBTABLE)
                {
                    // サブテーブル系は別のテーブルに保存
                    mainFieldValue.AppId = AppId;
                    mainFieldValue.RecordId = recordId;
                    mainFieldValue.Revision = recordRevision;
                    mainFieldValue.MainFieldName = mainFieldName;
                    rtn.Add(mainFieldValue);

                    if (mainFieldValue is SubTableFieldList subTable)
                    {
                        foreach (var table in subTable)
                        {
                            var tableId = table.Id ?? string.Empty;

                            foreach (var subFieldName in table.ListRecord.Keys)
                            {
                                if(table.ListRecord.TryGetValue(subFieldName, out var subField))
                                {
                                    var subFieldValue = subField.Value;
                                    if (subFieldValue == null)
                                    {
                                        continue;
                                    }

                                    if (subFieldValue.FieldType == FieldToDatabaseTypeEnum.LIST)
                                    {
                                        // 複数選択系は別のテーブルにも保存する
                                        subFieldValue.AppId = AppId;
                                        subFieldValue.RecordId = recordId;
                                        subFieldValue.Revision = recordRevision;
                                        subFieldValue.MainFieldName = mainFieldName;
                                        subFieldValue.SubFieldName = subFieldName;
                                        subFieldValue.SubTableId = tableId;
                                        rtn.Add(subFieldValue);
                                    }

                                }
                            }
                        }
                    }
                }
            }
        }
        return rtn;
    }
}
