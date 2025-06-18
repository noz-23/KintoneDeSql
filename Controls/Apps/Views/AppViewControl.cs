/*
 * Kintone De Sql
 * Copyright (c) 2025 noz-23
 *  https://github.com/noz-23/
 * 
 * Licensed under the MIT License 
 * 
 */
using KintoneDeSql.Requests.Apps.Views;
using KintoneDeSql.Responses.Apps;
using KintoneDeSql.Responses.Apps.Views;

namespace KintoneDeSql.Controls.Apps.Views;

/// <summary>
/// https://cybozu.dev/ja/kintone/docs/rest-api/apps/view/get-views/
/// </summary>
internal class AppViewControl : BaseAppControl
{
    /// <summary>
    /// コンストラクタ
    /// </summary>
    public AppViewControl() : base()
    {
        ControlMainTableName= AppViewResponse.TableName(false);
        // 下の表示
        ControlSubTableName = AppViewResponseField.TableName(false);
    }

    /// <summary>
    /// Get釦押下処理
    /// </summary>
    /// <param name="appId_">AppId</param>
    /// <returns>挿入数</returns>
    public override async Task<int> ControlInsert(string appId_, string apiKey_)
    {
        const int _max = 1;
        var count = 0;
        //
        _ProgressCount?.Invoke(count, _max, ControlMainTableName);
        var response = await AppViewsRequest.Instance.Insert(appId_, apiKey_);
        _ProgressCount?.Invoke(1);
        return count;
    }
}