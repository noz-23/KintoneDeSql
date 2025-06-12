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
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using System.Windows.Documents;

namespace KintoneDeSql.Responses.Apps;

/// <summary>
/// https://cybozu.dev/ja/kintone/docs/rest-api/apps/form/get-form-layout/
/// </summary>
internal class AppFromLayoutResponse:BaseToData
{
    // layout[].type	文字列	行の種類
    [JsonPropertyName("type")]
    [ColumnEx("type", Order = 101, TypeName = "TEXT")]
    public string Type { get; set; } =string.Empty;

    // layout[].code	文字列	テーブル、またはグループのコード
    [JsonPropertyName("code")]
    [ColumnEx("code", Order = 102, TypeName = "TEXT")]
    public string Code { get; set; } = string.Empty;

    /// <summary>
    /// リストごとに展開して保存
    /// </summary>
    // layout[].fields	配列	行に含まれるフィールドの一覧
    [JsonPropertyName("fields")]
    public ListLineResponse ListField { get; set; } =new();

    [ColumnEx("field_count", Order = 4, TypeName = "TEXT")]
    public string FieldsCount { get; set; } = string.Empty;

    /// <summary>
    /// 再帰処理して保存
    /// </summary>
    // layout[].layout	配列	グループ内のフィールドのレイアウトの一覧
    [JsonPropertyName("layout")]
    public List<AppFromLayoutResponse> ListLayout { get; set; } = new();

    [ColumnEx("layout_count", Order = 3, TypeName = "TEXT")]
    public string LayoutCount { get; set; } = string.Empty;
}

[Table($"{SQLiteManager.SUB_DATABASE}.appFromLayouts")]
internal class ListAppFormLayoutResponse: BaseToData,ISqlTable
{
    // layout	配列	フォームの行ごとのレイアウトの一覧
    [JsonPropertyName("layout")]
    public List<AppFromLayoutResponse> ListLayout { get; set; } = new();

    // revision	文字列	アプリの設定のリビジョン番号
    [JsonPropertyName("revision")]
    [ColumnEx("revision", Order = 2, TypeName = "TEXT")]
    public string Revision { get; set; } = string.Empty;

    #region NoJson
    /// <summary>
    /// AppFromLayoutResponse の番号
    /// </summary>
    [ColumnEx("layout_count", Order = 10, TypeName = "TEXT")]
    public string LayoutsCount { get; set; } = string.Empty;


    /// <summary>
    /// 上位のアプリID
    /// </summary>
    [ColumnEx("appId", Order = 1, TypeName = "TEXT")]
    public string AppId { get; set; } = string.Empty;
    #endregion

    #region ISqlTable
    public static string TableName(bool withCamma_) => typeof(ListAppFormLayoutResponse).TableName(withCamma_);

    public static List<string> ListCreateHeader(bool withCamma_)
    {
        var list = typeof(ListAppFormLayoutResponse).ListColumn();
        list.AddRange(typeof(AppFromLayoutResponse).ListColumn("layout_"));
        list.AddRange(typeof(LineResponse).ListColumn("layout_fields_"));
        return MemberInfoExtension.ListCreateHeader(list, withCamma_);

    }

    public static List<string> ListInsertHeader(bool withCamma_)
    {
        var list = typeof(ListAppFormLayoutResponse).ListColumn();
        list.AddRange(typeof(AppFromLayoutResponse).ListColumn("layout_"));
        list.AddRange(typeof(LineResponse).ListColumn("layout_fields_"));
        return MemberInfoExtension.ListInsertHeader(list, withCamma_);
    }

    public List<List<string>> ListInsertValue(bool withCamma_) => _listInsertValue(ListLayout, withCamma_, null);

    private List<List<string>> _listInsertValue( List<AppFromLayoutResponse> listLayout_, bool withCamma_, int? count_)
    {
        var rtn =new List<List<string>>();

        var layoutCount =0;
        foreach (var layout in listLayout_)
        {
            LayoutsCount = (count_ == null) ? $"{layoutCount}" : $"{count_}";
            layout.LayoutCount = (count_ == null) ? string.Empty : $"{layoutCount}";
            //
            //var list = base.ListValue(withCamma_);
            //list.AddRange(layout.ListValue(withCamma_));
            //
            if (layout.ListField.Any() == false)
            {
                // レイアウト保存できないので空データ挿入
                layout.ListField.Add(new());
            }

            var fieldCount = 0;
            foreach (var field in layout.ListField)
            {
                layout.FieldsCount = $"{fieldCount}";
                //
                var list = base.ListValue(withCamma_);
                list.AddRange(layout.ListValue(withCamma_));

                //var add = new List<string>(list);
                list.AddRange(field.ListValue(withCamma_));
                rtn.Add(list);
                fieldCount++;
            }
            //

            rtn.AddRange(_listInsertValue(layout.ListLayout, withCamma_, layoutCount));
            layoutCount++;
        }
        return rtn;
    }


    //public List<List<string>> ListInsertValue(bool withCamma_)
    //{ 
    //}


    #endregion
}
