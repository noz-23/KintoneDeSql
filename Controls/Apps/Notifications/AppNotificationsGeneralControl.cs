/*
 * Kintone De Sql
 * Copyright (c) 2025 noz-23
 *  https://github.com/noz-23/
 * 
 * Licensed under the MIT License 
 * 
 */
using KintoneDeSql.Requests.Apps.Notifications;
using KintoneDeSql.Responses.Apps.Notifications;

namespace KintoneDeSql.Controls.Apps.Notifications;

/// <summary>
/// https://cybozu.dev/ja/kintone/docs/rest-api/apps/settings/get-app-permissions/
/// </summary>
internal class AppNotificationsGeneralControl : BaseAppControl
{
    /// <summary>
    /// コンストラクタ
    /// </summary>
    public AppNotificationsGeneralControl() : base()
    {
        ControlMainTableName = AppNotificationsGeneralResponse.TableName(false);
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
        var response = await AppNotificationsGeneralRequest.Instance.Insert(appId_);
        _ProgressCount?.Invoke(++count);
        //
        return count;
    }
}