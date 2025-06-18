/*
 * Kintone De Sql
 * Copyright (c) 2025 noz-23
 *  https://github.com/noz-23/
 * 
 * Licensed under the MIT License 
 * 
 */
using KintoneDeSql.Requests.Apps.Settings;
using KintoneDeSql.Responses.Apps.Settings;

namespace KintoneDeSql.Controls.Apps.Settings;

/// <summary>
/// https://cybozu.dev/ja/kintone/docs/rest-api/apps/settings/get-general-settings/
/// </summary>
internal class AppSettingControl : BaseAppControl
{
    /// <summary>
    /// コンストラクタ
    /// </summary>
    public AppSettingControl() : base()
    {
        ControlMainTableName= AppSettingResponse.TableName(false);
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
        var response = await AppSettingRequest.Instance.Insert(appId_, apiKey_);
        _ProgressCount?.Invoke(count);

        return count;
    }
}