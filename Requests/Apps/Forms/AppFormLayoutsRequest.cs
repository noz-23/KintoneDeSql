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
/// https://cybozu.dev/ja/kintone/docs/rest-api/apps/form/get-form-layout/
/// </summary>
internal class AppFormLayoutsRequest : BaseRequest<AppFormLayoutsRequest, AppFormLayoutResponse>
{
    protected override string _Command { get; } = "app/form/layout.json";
    public override void Insert(AppFormLayoutResponse? response_)
    {
        if (response_ != null)
        {
            SQLiteManager.Instance.InsertTable(AppFormLayoutResponse.TableName(false), AppFormLayoutResponse.ListInsertHeader(true), response_.ListInsertValue(true));
        }
    }
}
