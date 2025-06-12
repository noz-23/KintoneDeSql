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
using KintoneDeSql.Extensions;
using KintoneDeSql.Interface;
using KintoneDeSql.Managers;
using KintoneDeSql.Responses.Commons;
using KintoneDeSql.Responses.Records;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace KintoneDeSql.Responses.Spaces;

/// <summary>
/// https://cybozu.dev/ja/kintone/docs/rest-api/spaces/get-space/
/// </summary>
internal class PermissionsResponse : BaseToData
{
    //permissions.createApp	文字列	アプリを作成できるユーザー
    [JsonPropertyName("createApp")]
    [ColumnEx("createApp", Order = 1, TypeName = "TEXT")]
    public string CreateApp { get; set; } = string.Empty;
}

[Table($"{SQLiteManager.SPACE_DATABASE}.spaces")]
internal class SpaceResponse : SpaceValue, ISqlTable
{
    //defaultThread	文字列	スペースが作成されたときに初期作成されたスレッドのスレッドID
    [JsonPropertyName("defaultThread")]
    [ColumnEx("defaultThread", Order = 3, TypeName = "TEXT")]
    public string DefaultThread { get; set; } = string.Empty;

    //isPrivate	真偽値	公開／非公開の区分
    [JsonPropertyName("isPrivate")]
    [ColumnEx("isPrivate", Order = 4, TypeName = "NUMERIC")]
    public bool IsPrivate { get; set; } = false;

    //creator	オブジェクト	スペースの作成者情報
    //creator.code	文字列	作成者のコード
    //creator.name	文字列	作成者の名前
    [JsonPropertyName("creator")]
    [ColumnEx("creator", Order = 101, IsExtract = true)]
    public CreatorValue Creator { get; set; } = new();

    //modifier	オブジェクト	スペースの更新者情報
    //modifier.code	文字列	更新者のコード
    //modifier.name	文字列	更新者の名前
    [JsonPropertyName("modifier")]
    [ColumnEx("modifier", Order = 201, IsExtract = true)]
    public ModifierValue Modifier { get; set; } = new();

    //memberCount	文字列	スペースのメンバー数
    [JsonPropertyName("memberCount")]
    [ColumnEx("memberCount", Order = 10, TypeName = "TEXT")]
    public string MemberCount { get; set; } = string.Empty;

    //coverType	文字列	スペースのカバー画像の種類
    [JsonPropertyName("coverType")]
    [ColumnEx("coverType", Order = 11, TypeName = "TEXT")]
    public string CoverType { get; set; } = string.Empty;

    //coverKey	文字列	スペースのカバー画像のキー文字列
    [JsonPropertyName("coverKey")]
    [ColumnEx("coverKey", Order = 12, TypeName = "TEXT")]
    public string CoverKey { get; set; } = string.Empty;

    //coverUrl	文字列	スペースのカバー画像のURL
    [JsonPropertyName("coverUrl")]
    [ColumnEx("coverUrl", Order = 13, TypeName = "TEXT")]
    public string CoverUrl { get; set; } = string.Empty;

    //body	文字列	スペースの本文（HTML）
    [JsonPropertyName("body")]
    [ColumnEx("body", Order = 14, TypeName = "TEXT")]
    public string Body { get; set; } = string.Empty;

    //useMultiThread	真偽値	「スペースのポータルと複数のスレッドを使用する」が有効のスペースかどうか
    [JsonPropertyName("useMultiThread")]
    [ColumnEx("useMultiThread", Order = 15, TypeName = "NUMERIC")]
    public bool UseMultiThread { get; set; } = false;

    //isGuest	真偽値	ゲストスペースかどうか
    [JsonPropertyName("isGuest")]
    [ColumnEx("isGuest", Order = 16, TypeName = "NUMERIC")]
    public bool IsGuest { get; set; } = false;

    //attachedApps	配列	スレッド内アプリのリスト
    [JsonPropertyName("attachedApps")]
    [ColumnEx("attachedApps", Order = 17, TypeName = "TEXT")]
    public ListAttachedAppResponse ListAttachedApp { get; set; } = new();

    //fixedMember	真偽値	各ユーザーがスペースの退会／アンフォローすることを禁止するかどうか
    [JsonPropertyName("fixedMember")]
    [ColumnEx("fixedMember", Order = 20, TypeName = "NUMERIC")]
    public bool FixedMember { get; set; } = false;

    //showAnnouncement	真偽値	「スペースのポータルと複数のスレッドを使用する」が有効のスペースのポータルの「お知らせ」の表示状態
    [JsonPropertyName("showAnnouncement")]
    [ColumnEx("showAnnouncement", Order = 21, TypeName = "NUMERIC")]
    public bool ShowAnnouncement { get; set; } = false;

    //showThreadList	真偽値	「スペースのポータルと複数のスレッドを使用する」が有効のスペースのポータルの「スレッド一覧」の表示状態
    [JsonPropertyName("showThreadList")]
    [ColumnEx("showThreadList", Order = 22, TypeName = "NUMERIC")]
    public bool ShowThreadList { get; set; } = false;

    //showAppList	真偽値	「スペースのポータルと複数のスレッドを使用する」が有効のスペースのポータルの「アプリ一覧」の表示状態
    [JsonPropertyName("showAppList")]
    [ColumnEx("showAppList", Order = 23, TypeName = "NUMERIC")]
    public bool ShowAppList { get; set; } = false;

    //showMemberList	真偽値	「スペースのポータルと複数のスレッドを使用する」が有効のスペースのポータルの「ピープル一覧」の表示状態
    [JsonPropertyName("showMemberList")]
    [ColumnEx("showMemberList", Order = 24, TypeName = "NUMERIC")]
    public bool ShowMemberList { get; set; } = false;

    //showRelatedLinkList	真偽値	「スペースのポータルと複数のスレッドを使用する」が有効のスペースのポータルの「関連リンク一覧」の表示状態
    [JsonPropertyName("showRelatedLinkList")]
    [ColumnEx("showRelatedLinkList", Order = 25, TypeName = "NUMERIC")]
    public bool ShowRelatedLinkList { get; set; } = false;

    //permissions	オブジェクト	スペースに関する権限

    [JsonPropertyName("permissions")]
    [ColumnEx("permissions", Order = 100, IsExtract =true)]
    public PermissionsResponse Permissions { get; set; } = new();

    #region ISqlTable
    public static string TableName(bool withCamma_) => typeof(SpaceResponse).TableName(withCamma_);
    public static List<string> ListCreateHeader(bool withCamma_)
    {
        var list =typeof(SpaceResponse).ListColumn();
        //list.AddRange(typeof(AttachedAppResponse).ListColumn("attachedApps_"));
        return MemberInfoExtension.ListCreateHeader(list, withCamma_);
    }
    public static List<string> ListInsertHeader(bool withCamma_)
    {
        var list = typeof(SpaceResponse).ListColumn();
        //list.AddRange(typeof(AttachedAppResponse).ListColumn("attachedApps_"));
        return MemberInfoExtension.ListInsertHeader(list, withCamma_);
    }
    public List<List<string>> ListInsertValue(bool withCamma_)
    {
        //var rtn = new List<List<string>>();

        //if (ListAttachedApp.Any() == false)
        //{
        //    ListAttachedApp.Add(new ());
        //}
        //foreach (var app in ListAttachedApp)
        //{
        //    var list = base.ListValue(withCamma_);
        //    list.AddRange(app.ListValue(withCamma_));
        //    rtn.Add(list);
        //}
        var rtn = new List<List<string>>() { base.ListValue(withCamma_)};

        return rtn;
    }
    #endregion

}
