/*
 * Kintone De Sql
 * Copyright (c) 2025 noz-23
 *  https://github.com/noz-23/
 * 
 * Licensed under the MIT License 
 * 
 */
using KintoneDeSql.Data;
using KintoneDeSql.Files;
using KintoneDeSql.Managers;
using KintoneDeSql.Properties;
using KintoneDeSql.Responses.Records;
using System.Net.Http;
using System.Text.Json;

namespace KintoneDeSql.Requests.Records;

/// <summary>
/// https://cybozu.dev/ja/kintone/docs/rest-api/records/get-records/
/// offsetの制限値を考慮したkintoneのレコード一括取得について
/// https://cybozu.dev/ja/kintone/tips/best-practices/data-acquisition-operation/kintone-get-records-with-offset-limitation/
/// </summary>
internal class RecordRequest : BaseRequest<RecordRequest, RecordResponse>
{
    protected override string _Command { get; } = "records.json";
    public override void Insert(RecordResponse? response_)
    {
        if (response_ != null)
        {
            SQLiteManager.Instance.InsertTable(response_.TableName(true), response_.ListInsertHeader(true), response_.ListInsertValue(true));
            foreach (var subTable in response_.ListSubTable())
            {
                //SQLiteManager.Instance.DeleteTable(subTable.SubTableName(false), $"'{Resource.COLUMN_APP_ID}'={appId_}");
                SQLiteManager.Instance.InsertTable(subTable.SubTableName(false), subTable.ListSubInsertHeader(true), subTable.ListSubValue(true));
            }
        }
    }

}
