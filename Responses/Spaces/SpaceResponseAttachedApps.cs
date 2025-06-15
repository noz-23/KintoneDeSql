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
using KintoneDeSql.Responses.Records;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace KintoneDeSql.Responses.Spaces;

/// <summary>
/// https://cybozu.dev/ja/kintone/docs/rest-api/spaces/get-space/
/// </summary>
[Table($"{SQLiteManager.SPACE_DATABASE}.spaces_AttachedApps")]
internal class SpaceResponseAttachedApps: SpaceResponseBase, ISqlTable
{
    //name	文字列	スペース名
    //apps[].name	文字列	アプリ名
    [JsonPropertyName("name")]
    [ColumnEx("name", Order = 10, TypeName = "TEXT")]
    public string Name { get; set; } = string.Empty;

    //isPrivate	真偽値	公開／非公開の区分
    [JsonPropertyName("isPrivate")]
    [ColumnEx("isPrivate", Order = 11, TypeName = "NUMERIC")]
    public bool IsPrivate { get; set; } = false;

    //memberCount	文字列	スペースのメンバー数
    [JsonPropertyName("memberCount")]
    [ColumnEx("memberCount", Order = 12, TypeName = "TEXT")]
    public string MemberCount { get; set; } = string.Empty;

    //coverType	文字列	スペースのカバー画像の種類
    [JsonPropertyName("coverType")]
    [ColumnEx("coverType", Order = 13, TypeName = "TEXT")]
    public string CoverType { get; set; } = string.Empty;

    //coverKey	文字列	スペースのカバー画像のキー文字列
    [JsonPropertyName("coverKey")]
    [ColumnEx("coverKey", Order = 14, TypeName = "TEXT")]
    public string CoverKey { get; set; } = string.Empty;

    //coverUrl	文字列	スペースのカバー画像のURL
    [JsonPropertyName("coverUrl")]
    [ColumnEx("coverUrl", Order = 15, TypeName = "TEXT")]
    public string CoverUrl { get; set; } = string.Empty;

    //body	文字列	スペースの本文（HTML）
    [JsonPropertyName("body")]
    [ColumnEx("body", Order = 16, TypeName = "TEXT")]
    public string Body { get; set; } = string.Empty;

    //useMultiThread	真偽値	「スペースのポータルと複数のスレッドを使用する」が有効のスペースかどうか
    [JsonPropertyName("useMultiThread")]
    [ColumnEx("useMultiThread", Order = 15, TypeName = "NUMERIC")]
    public bool UseMultiThread { get; set; } = false;

    //isGuest	真偽値	ゲストスペースかどうか
    [JsonPropertyName("isGuest")]
    [ColumnEx("isGuest", Order = 17, TypeName = "NUMERIC")]
    public bool IsGuest { get; set; } = false;

    //fixedMember	真偽値	各ユーザーがスペースの退会／アンフォローすることを禁止するかどうか
    [JsonPropertyName("fixedMember")]
    [ColumnEx("fixedMember", Order = 18, TypeName = "NUMERIC")]
    public bool FixedMember { get; set; } = false;

    //showAnnouncement	真偽値	「スペースのポータルと複数のスレッドを使用する」が有効のスペースのポータルの「お知らせ」の表示状態
    [JsonPropertyName("showAnnouncement")]
    [ColumnEx("showAnnouncement", Order = 19, TypeName = "NUMERIC")]
    public bool ShowAnnouncement { get; set; } = false;

    //showThreadList	真偽値	「スペースのポータルと複数のスレッドを使用する」が有効のスペースのポータルの「スレッド一覧」の表示状態
    [JsonPropertyName("showThreadList")]
    [ColumnEx("showThreadList", Order = 20, TypeName = "NUMERIC")]
    public bool ShowThreadList { get; set; } = false;

    //showAppList	真偽値	「スペースのポータルと複数のスレッドを使用する」が有効のスペースのポータルの「アプリ一覧」の表示状態
    [JsonPropertyName("showAppList")]
    [ColumnEx("showAppList", Order = 21, TypeName = "NUMERIC")]
    public bool ShowAppList { get; set; } = false;

    //showMemberList	真偽値	「スペースのポータルと複数のスレッドを使用する」が有効のスペースのポータルの「ピープル一覧」の表示状態
    [JsonPropertyName("showMemberList")]
    [ColumnEx("showMemberList", Order = 22, TypeName = "NUMERIC")]
    public bool ShowMemberList { get; set; } = false;

    //showRelatedLinkList	真偽値	「スペースのポータルと複数のスレッドを使用する」が有効のスペースのポータルの「関連リンク一覧」の表示状態
    [JsonPropertyName("showRelatedLinkList")]
    [ColumnEx("showRelatedLinkList", Order = 23, TypeName = "NUMERIC")]
    public bool ShowRelatedLinkList { get; set; } = false;

    //permissions	オブジェクト	スペースに関する権限
    [JsonPropertyName("permissions")]
    [ColumnEx("permissions", Order = 100, TypeName = "TEXT", IsExtract = true)]
    public PermissionsValue Permissions { get; set; } = new();

    //creator	オブジェクト	スペースの作成者情報
    //creator.code	文字列	作成者のコード
    //creator.name	文字列	作成者の名前
    [JsonPropertyName("creator")]
    [ColumnEx("creator", Order = 200, TypeName = "TEXT", IsExtract = true)]
    public CreatorValue Creator { get; set; } = new();

    //modifier	オブジェクト	スペースの更新者情報
    //modifier.code	文字列	更新者のコード
    //modifier.name	文字列	更新者の名前
    [JsonPropertyName("modifier")]
    [ColumnEx("modifier", Order = 300, TypeName = "TEXT", IsExtract = true)]
    public ModifierValue Modifier { get; set; } = new();

    //attachedApps	配列	スレッド内アプリのリスト
    [JsonPropertyName("attachedApps")]
    [ColumnEx("attachedApps", Order = 400, TypeName = "TEXT")]
    public AttachedAppValueList ListAttachedApp { get; set; } = new();

    #region ISqlTable
    public static string TableName(bool withCamma_) => typeof(SpaceResponseAttachedApps).TableName(withCamma_);
    public static IEnumerable<string> ListCreateHeader(bool withCamma_) => MemberInfoExtension.ListCreateHeader(_listColumn(), withCamma_);
    public static IEnumerable<string> ListInsertHeader(bool withCamma_) => MemberInfoExtension.ListInsertHeader(_listColumn(), withCamma_);
    private static IEnumerable<ColumnData> _listColumn()
    {
        const int _SORT = 100;
        var rtn = typeof(SpaceResponseBase).ListColumn();
        rtn.AddRange(typeof(AttachedAppValue).ListColumn(string.Empty, _SORT));
        return rtn;
    }
    public virtual IEnumerable<IEnumerable<string>> ListInsertValue(bool withCamma_) => ListInsertValueAttachedApp(withCamma_);
    public IEnumerable<IEnumerable<string>> ListInsertValueAttachedApp(bool withCamma_)
    {
        var rtn = new List<IList<string>>();
        foreach (var attach in ListAttachedApp)
        {
            var list = this.ListValue(withCamma_, typeof(SpaceResponseBase));
            list.AddRange(attach.ListValue(withCamma_));
            rtn.Add(list);
        }
        return rtn;
    }
    #endregion
}
