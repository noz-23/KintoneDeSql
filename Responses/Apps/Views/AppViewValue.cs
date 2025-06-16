/*
 * Kintone De Sql
 * Copyright (c) 2025 noz-23
 *  https://github.com/noz-23/
 * 
 * Licensed under the MIT License 
 * 
 */
using KintoneDeSql.Attributes;
using KintoneDeSql.Interface;
using System.Text.Json.Serialization;

namespace KintoneDeSql.Responses.Apps.Views;

/// <summary>
/// https://cybozu.dev/ja/kintone/docs/rest-api/apps/view/get-views/
/// </summary>
internal class AppViewValue : AppViewValueBase
{
    // views.一覧名.type  文字列 一覧の表示形式
    [JsonPropertyName("type")]
    [ColumnEx("type", Order = 11, TypeName = "TEXT")]
    public string Type { get; set; } = string.Empty;

    // views.一覧名.builtinType 文字列 一覧名が「（作業者が自分）」の場合のみ出力されるプロパティ
    [JsonPropertyName("builtinType")]
    [ColumnEx("builtinType", Order = 12, TypeName = "TEXT")]
    public string BuiltInType { get; set; } = string.Empty;

    //views.一覧名.name 文字列 一覧名
    [JsonPropertyName("name")]
    [ColumnEx("name", Order = 13, TypeName = "TEXT")]
    public string Name { get; set; } = string.Empty;

    //views.一覧名.date 文字列 日付として使用するフィールドのフィールドコード
    [JsonPropertyName("date")]
    [ColumnEx("date", Order = 21, TypeName = "TEXT")]
    public string Date { get; set; } = string.Empty;

    //views.一覧名.title 文字列 タイトルとして使用するフィールドのフィールドコード
    [JsonPropertyName("title")]
    [ColumnEx("title", Order = 22, TypeName = "TEXT")]
    public string Title { get; set; } = string.Empty;

    //views.一覧名.html 文字列 カスタマイズに使用するHTMLの内容
    [JsonPropertyName("html")]
    [ColumnEx("html", Order = 23, TypeName = "TEXT")]
    public string Html { get; set; } = string.Empty;

    //views.一覧名.pager 真偽値 ページネーションを表示するかどうか
    [JsonPropertyName("pager")]
    [ColumnEx("pager", Order = 24, TypeName = "NUMERIC")]
    public bool Pager { get; set; } = false;

    //views.一覧名.device 文字列	一覧を表示する範囲
    [JsonPropertyName("device")]
    [ColumnEx("device", Order = 25, TypeName = "TEXT")]
    public string Device { get; set; } = string.Empty;

    //views.一覧名.filterCond 文字列 レコードの絞り込み条件
    [JsonPropertyName("filterCond")]
    [ColumnEx("filterCond", Order = 26, TypeName = "TEXT")]
    public string FilterCond { get; set; } = string.Empty;

    // views.一覧名.sort	文字列	レコードのソート条件
    [JsonPropertyName("sort")]
    [ColumnEx("sort", Order = 27, TypeName = "TEXT")]
    public string Sort { get; set; } = string.Empty;

    // views.一覧名.index 文字列 一覧の表示順（昇順）
    [JsonPropertyName("index")]
    [ColumnEx("index", Order = 28, TypeName = "TEXT")]
    public string Index { get; set; } = string.Empty;
    public override string ToString()
    {
        return Name.ToString();
    }
}