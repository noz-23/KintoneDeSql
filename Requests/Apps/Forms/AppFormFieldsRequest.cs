﻿/*
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
//internal class AppFormFieldsRequest: BaseSingleton<AppFormFieldsRequest>
internal class AppFormFieldsRequest : BaseRequest<AppFormFieldsRequest, AppFormFieldResponse>
{
    //private const string _COMMAND = "app/form/fields.json";
    //public async Task<AppFormFieldResponse?> Get(string appId_, string appToken_="")
    //{
    //    var query = string.Empty;
    //    var paramater = JsonSerializer.Serialize(new { app = appId_ });
    //    var response = await KintoneManager.Instance.KintoneGet<AppFormFieldResponse?>(HttpMethod.Get, _COMMAND, query, paramater, appToken_);
    //    //
    //    if (response != null)
    //    {
    //        response.AppId = appId_;
    //    }
    //    return response;
    //}

    //public async Task<AppFormFieldResponse?> Insert(string appId_, string appToken_ = "")
    //{
    //    var response = await Get(appId_, appToken_);

    //    Insert(response);

    //    return response;
    //}
    //public async Task<AppFormFieldResponse?> Get(string appId_, string appToken_ = "") => await base._Get(_COMMAND, appId_, appToken_);
    //public async Task<AppFormFieldResponse?> Insert(string appId_, string appToken_ = "") => await base._Insert(_COMMAND, appId_, appToken_);
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
