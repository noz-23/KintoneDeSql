/*
 * Kintone De Sql
 * Copyright (c) 2025 noz-23
 *  https://github.com/noz-23/
 * 
 * Licensed under the MIT License 
 * 
 */
using KintoneDeSql.Requests.Apps.Forms;
using KintoneDeSql.Responses.Apps.Forms;

namespace KintoneDeSql.Controls.Apps.Forms;

/// <summary>
/// https://cybozu.dev/ja/kintone/docs/rest-api/apps/form/get-form-layout/
/// </summary>
internal class AppFormLayoutControl : BaseAppControl
{
    /// <summary>
    /// コンストラクタ
    /// </summary>
    public AppFormLayoutControl() : base()
    {
        ControlMainTableName= AppFormLayoutResponse.TableName(false);
    }

    /// <summary>
    /// Get釦押下処理
    /// </summary>
    /// <param name="appId_">AppId</param>
    /// <returns>挿入数</returns>
    public override async Task<int> ControlInsert(string appId_)
    {
        const int _max = 1;
        var count = 0;
        //
        _ProgressCount?.Invoke(count, _max, ControlMainTableName);
        var response = await AppFormLayoutsRequest.Instance.Insert(appId_);
        _ProgressCount?.Invoke(++count);
        //
        return count;
    }
}
