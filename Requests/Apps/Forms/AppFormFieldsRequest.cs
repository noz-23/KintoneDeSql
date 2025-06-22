/*
 * Kintone De Sql
 * Copyright (c) 2025 noz-23
 *  https://github.com/noz-23/
 * 
 * Licensed under the MIT License 
 * 
 */
using KintoneDeSql.Managers;
using KintoneDeSql.Responses.Apps.Forms;

namespace KintoneDeSql.Requests.Apps.Forms;

/// <summary>
/// https://cybozu.dev/ja/kintone/docs/rest-api/apps/form/get-form-fields/
/// </summary>
internal class AppFormFieldsRequest : BaseRequest<AppFormFieldsRequest, AppFormFieldResponse>
{
    protected override string _Command { get; } = "app/form/fields.json";
    public override void Insert(AppFormFieldResponse? response_)
    {
        if (response_ != null)
        {
            SQLiteManager.Instance.InsertTable(AppFormFieldResponse.TableName(false), AppFormFieldResponse.ListInsertHeader(true), response_.ListInsertValue(true));
            SQLiteManager.Instance.InsertTable(AppFormFieldResponseEntity.TableName(false), AppFormFieldResponseEntity.ListInsertHeader(true), response_.ListInsertValueEntity(true));
            SQLiteManager.Instance.InsertTable(AppFormFieldResponseOption.TableName(false), AppFormFieldResponseOption.ListInsertHeader(true), response_.ListInsertValueOption(true));
            SQLiteManager.Instance.InsertTable(AppFormFieldResponseDisplayField.TableName(false), AppFormFieldResponseDisplayField.ListInsertHeader(true), response_.ListInsertValueDisplayField(true));
            SQLiteManager.Instance.InsertTable(AppFormFieldResponseFieldMapping.TableName(false), AppFormFieldResponseFieldMapping.ListInsertHeader(true), response_.ListInsertValueFieldMapping(true));
        }
    }
}
