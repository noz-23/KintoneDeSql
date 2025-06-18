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
using KintoneDeSql.Files;
using KintoneDeSql.Properties;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

namespace KintoneDeSql.Responses.Records;

internal sealed partial class FieldValueRegist:BaseSingleton<FieldValueRegist>
{
    /// <summary>
    /// 作成した時点で各値が入り登録
    /// </summary>
    internal void Create()
    {
        LogFile.Instance.WriteLine($"Create {typeof(FieldValueRegist)}");
    }

    /// <summary>
    /// 登録処理
    /// </summary>
    /// <param name="type_"></param>
    /// <param name="callBack_"></param>
    /// <returns></returns>
    internal static bool Regist(string type_, BaseFieldValue.GetValueCallBack callBack_)=> BaseFieldValue.Regist(type_, callBack_);
}

internal class BaseFieldValue: BaseToData
{
    protected BaseFieldValue()
    {
        _json = string.Empty;
    }
    protected BaseFieldValue(string json_, FieldToDatabaseTypeEnum fieldType_)
    {
        _json = json_;
        FieldType = fieldType_;
    }

    private string _json = string.Empty;

    //
    #region Common

    public readonly FieldToDatabaseTypeEnum FieldType = FieldToDatabaseTypeEnum.TEXT;
    public static readonly JsonSerializerOptions Ooptions = new JsonSerializerOptions
    {
        Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
        WriteIndented = true
    };

    // [ColumnEx]はありなしで判別つかない場合があるので、使わない
    [Column("appId",Order=1, TypeName = "TEXT")]
    public string AppId { get; set; } = string.Empty;

    [Column("recordId", Order = 2, TypeName = "TEXT")]
    public string RecordId { get; set; } = string.Empty;

    [Column("revision", Order = 3, TypeName = "TEXT")]
    public string Revision { get; set; } = string.Empty;

    [Column("mainFieldName", Order = 4, TypeName = "TEXT")]
    public string MainFieldName { get; set; } = string.Empty;

    [Column("subFieldName", Order = 5, TypeName = "TEXT")]
    public string SubFieldName { get; set; } = string.Empty;

    [Column("subTableId", Order = 6, TypeName = "TEXT")]
    public string SubTableId { get; set; } = string.Empty;
    //
    #endregion

    public delegate BaseFieldValue GetValueCallBack( string json_);
    private static Dictionary<string, GetValueCallBack> _listValue = new();
    public static BaseFieldValue Get(string type_, string json_)
    {
        if (_listValue.TryGetValue(type_, out var callback))
        {
            return callback(json_);
        }

        return new BaseFieldValue(json_, FieldToDatabaseTypeEnum.TEXT);
    }
    public static bool Regist(string type_, GetValueCallBack callBack_)
    {
        _listValue.Add(type_, callBack_);
        return true;
    }

    #region Field

    public delegate IEnumerable<string> FieldToStringCallBack(RecordFieldValue field_, string fieldName_, bool withCamma_);

    /// <summary>
    /// レコードからテーブル作成
    /// 基本は　FormField で作成するので未使用
    /// Api Key 利用時
    /// </summary>
    public static Dictionary<FieldToDatabaseTypeEnum, FieldToStringCallBack> ListToCreateHeader = new()
    {
        {FieldToDatabaseTypeEnum.TEXT,(field_,fieldName_,withCamma_)=>
            {
                var primary = (fieldName_ == Resource.COLUMN_MAIN_TABLE_ID) ? "PRIMARY KEY" : "";

                var fieldName =(withCamma_ ==true) ? $"'{fieldName_}'" : $"{fieldName_}";
                return new List<string>(){$"{fieldName} TEXT {primary}"};
            }
        },
        {FieldToDatabaseTypeEnum.EXTRACT,(field_,fieldName_,withCamma_)=>
            {
                var rtn = new List<string>();
                foreach (var column in  field_.ListColumn())
                {
                    var fieldName =(withCamma_ ==true) ? $"'{fieldName_}_{column.Name}'" : $"{fieldName_}_{column.Name}";
                    rtn.Add($"{fieldName} TEXT");
                }
                return rtn;
            }
        },
        {FieldToDatabaseTypeEnum.LIST,(field_,fieldName_,withCamma_)=>
            {
                var fieldName =(withCamma_ ==true) ? $"'{fieldName_}'" : $"{fieldName_}";
                return new List<string>(){$"{fieldName} TEXT" };
            }
        },
        {FieldToDatabaseTypeEnum.SUBTABLE,(field_,fieldName_,withCamma_)=>
            {
                var fieldName =(withCamma_ ==true) ? $"'{fieldName_}'" : $"{fieldName_}";
                return new List<string>(){$"{fieldName} TEXT" };
            }
        }
    };

    public static Dictionary<FieldToDatabaseTypeEnum, FieldToStringCallBack> ListToInsertHeader = new()
    {
        {FieldToDatabaseTypeEnum.TEXT,(field__,fieldName_,withCamma_)=>
            {
                var fieldName =(withCamma_ ==true) ? $"'{fieldName_}'" : $"{fieldName_}";
                return  new List<string>(){fieldName };
            }
        },
        {FieldToDatabaseTypeEnum.EXTRACT,(field_,fieldName_,withCamma_)=>
            {
                var rtn = new List<string>();
                foreach (var column in  field_.ListColumn())
                {
                    var fieldName =(withCamma_ ==true) ? $"'{fieldName_}_{column.Name}'" : $"{fieldName_}_{column.Name}";
                    rtn.Add(fieldName);
                }
                return rtn;
            }
        },
        {FieldToDatabaseTypeEnum.LIST,(field_,fieldName_,withCamma_)=>
            {
                var fieldName =(withCamma_ ==true) ? $"'{fieldName_}'" : $"{fieldName_}";
                return new List<string>(){fieldName};
            }
        },
        {FieldToDatabaseTypeEnum.SUBTABLE,(field_,fieldName_,withCamma_)=>
            {
                var fieldName =(withCamma_ ==true) ? $"'{fieldName_}'" : $"{fieldName_}";
                return new List<string>(){fieldName};
            }
        }
    };

    public static Dictionary<FieldToDatabaseTypeEnum, FieldToStringCallBack> ListToValue = new()
    {
        {FieldToDatabaseTypeEnum.TEXT,(field_,fieldName_,withCamma_)=>
            {
                var fieldName =(withCamma_ ==true) ? $"'{field_.ToString()}'" : $"{field_.ToString()}";
                return new List<string>(){fieldName};
            }
        },
        {FieldToDatabaseTypeEnum.EXTRACT,(field_,fieldName_,withCamma_)=>field_.ListValue(withCamma_) },
        {FieldToDatabaseTypeEnum.LIST,(field_,fieldName_,withCamma_)=>
            {
                var fieldName =(withCamma_ ==true) ? $"'{field_.ToString()}'" : $"{field_.ToString()}";
                return new List<string>(){fieldName};
            }
        },
        {FieldToDatabaseTypeEnum.SUBTABLE,(field_,fieldName_,withCamma_)=>
            {
                var fieldName =(withCamma_ ==true) ? $"'{field_.ToString()}'" : $"{field_.ToString()}";
                return new List<string>(){fieldName};
            }
        }
    };

    #endregion

    #region SubTable
    public static IList<string> ListSubDefaultCreateHeader( bool withCamma_)
    {
        var rtn = new List<string>();
        //
        var list = typeof(BaseFieldValue).ListColumnProperty();
        if (list == null)
        {
            return rtn;
        }

        foreach (var prop in list)
        {
            var column = prop.GetAttribute<ColumnAttribute>();
            if (column == null)
            {
                continue;
            }

            var name = (withCamma_ == true) ? $"'{column.Name}'" : column.Name;
            rtn.Add($"{name} {column.TypeName}");
        }

        return rtn;
    }
    public static IList<string> ListSubDefaultInsertHeader(bool withCamma_)
    {
        var rtn = new List<string>();
        var list = typeof(BaseFieldValue).ListColumnProperty();
        if (list == null)
        {
            return rtn;
        }
        foreach (var prop in list)
        {
            var column = prop.GetAttribute<ColumnAttribute>();
            if (column == null)
            {
                continue;
            }

            var name = (withCamma_ == true) ? $"'{column.Name}'" : column.Name;
            rtn.Add($"{name}");
        }

        return rtn;
    }
    public IList<string> ListSubDefaultValue(bool withCamma_)
    {
        var rtn = new List<string>();
        var list = typeof(BaseFieldValue).ListColumnProperty();
        if (list == null)
        {
            return rtn;
        }
        foreach (var prop in list)
        {
            var column = prop.GetAttribute<ColumnAttribute>();
            if (column == null)
            {
                continue;
            }

            var val = prop.GetValue(this)?.ToString()??string.Empty;

            var add = (withCamma_ == true) ? $"'{val}'" : val;
            rtn.Add($"{add}");
        }

        return rtn;
    }
    public virtual string SubTableName(bool withCamma_) => string.Empty;
    public virtual IList<string> ListSubCreateHeader(bool withCamma_) => new List<string>();
    public virtual IList<string> ListSubInsertHeader(bool withCamma_) => new List<string>();
    public virtual IEnumerable<IEnumerable<string>> ListSubValue(bool withCamma_) => new List<IEnumerable<string>>();
    #endregion
    public IList<ColumnData> ListColumn() => this.GetType().ListColumn();
}
