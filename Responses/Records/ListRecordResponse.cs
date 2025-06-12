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

internal class ResponseRecord
{
    // record オブジェクト  取得したレコードの情報
    [JsonPropertyName("record")]
    public Dictionary<string, RecordFieldResponse> Record { get; set; } = new();
}

[Table("records")]
internal class ListRecordResponse :BaseToData,IInsertTable
{
    // records 配列（オブジェクト）	レコードの一覧
    [JsonPropertyName("records")]
    public List<Dictionary<string, RecordFieldResponse>> ListRecord { get; set; } = new();

    // totalCount  文字列 レコードの件数
    [JsonPropertyName("totalCount")]
    public string TotalCount { get; set; } = string.Empty;

    /// <summary>
    /// 上位のアプリ ID
    /// </summary>
    [ColumnEx("appId", Order = 1, TypeName = "TEXT")]
    public string AppId { get; set; } = string.Empty;

    #region
    public string TableName(bool withCamma_) =>(withCamma_==true) ? $"'{SettingManager.Instance.TableName(AppId)}'": SettingManager.Instance.TableName(AppId);

    public List<string> ListInsertHeader(bool withCamma_)
    {
        var rtn = typeof(ListRecordResponse).ListInsertHeader(withCamma_);
        foreach (var pair in ListRecord.FirstOrDefault())
        {
            var field = pair.Value;
            if (BaseFieldValue.ListToInsertHeader.TryGetValue(field.Value.FieldType, out var run) == true)
            {
                rtn.AddRange(run(field, pair.Key, withCamma_));
            }
        }
        return rtn;
    }

    public List<List<string>> ListInsertValue(bool withCamma_)
    {
        var rtn = new List<List<string>>();
        foreach (var record in ListRecord)
        {
            var list = base.ListValue(withCamma_);
            foreach (var pair in record)
            {
                var field = pair.Value;
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
    public List<BaseFieldValue> ListSubTable()
    {
        var rtn = new List<BaseFieldValue>();
        foreach (var record in ListRecord)
        {
            var recordFieldId = record[Resource.COLUMN_MAIN_TABLE_ID];
            var recordId = recordFieldId?.Value.ToString() ?? string.Empty;
            //
            foreach (var mainFieldName in record.Keys)
            {
                var mainFieldValue = record[mainFieldName].Value;

                if (mainFieldValue?.FieldType ==FieldToDatabaseTypeEnum.LIST)
                {
                    mainFieldValue.AppId = AppId;
                    mainFieldValue.RecordId = recordId;
                    mainFieldValue.MainFieldName = mainFieldName;

                    rtn.Add(mainFieldValue);
                }

                if (mainFieldValue?.FieldType == FieldToDatabaseTypeEnum.SUBTABLE)
                {
                    mainFieldValue.AppId = AppId;
                    mainFieldValue.RecordId = recordId;
                    mainFieldValue.MainFieldName = mainFieldName;
                    rtn.Add(mainFieldValue);

                    if (mainFieldValue is ListSubTableValue subTable)
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
                                        subFieldValue.AppId = AppId;
                                        subFieldValue.RecordId = recordId;
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
