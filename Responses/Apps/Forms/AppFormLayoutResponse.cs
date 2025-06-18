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

namespace KintoneDeSql.Responses.Apps.Forms;

/// <summary>
/// https://cybozu.dev/ja/kintone/docs/rest-api/apps/form/get-form-layout/
/// </summary>

[Table($"{SQLiteManager.SUB_DATABASE}.appFromLayouts")]
internal class AppFormLayoutResponse: BaseToData, ISqlTable,IAppTableId
{
    /// <summary>
    /// appFromLayouts 保存
    /// </summary>
    // layout	配列	フォームの行ごとのレイアウトの一覧
    [JsonPropertyName("layout")]
    public List<AppFormLayoutValue> ListLayout { get; set; } = new();

    // revision	文字列	アプリの設定のリビジョン番号
    [JsonPropertyName("revision")]
    [ColumnEx("revision", Order = 2, TypeName = "TEXT")]
    public string Revision { get; set; } = string.Empty;

    #region NoJson
    /// <summary>
    /// 上位のアプリID
    /// </summary>
    [ColumnEx("appId", Order = 1, TypeName = "TEXT", IsUnique =true)]
    public string AppId { get; set; } = string.Empty;
    #endregion

    #region ISqlTable
    public static string TableName(bool withCamma_) => typeof(AppFormLayoutResponse).TableName(withCamma_);
    public static IEnumerable<string> ListCreateHeader(bool withCamma_) => MemberInfoExtension.ListCreateHeader(_listColumn(), withCamma_);
    public static IEnumerable<string> ListInsertHeader(bool withCamma_) => MemberInfoExtension.ListInsertHeader(_listColumn(), withCamma_);

    private static IEnumerable<ColumnData> _listColumn()
    {
        const int _SORT = 100;
        const int _SORT_FIELD = 10;
        var rtn = typeof(AppFormLayoutResponse).ListColumn();
        rtn.AddRange(typeof(AppFormLayoutValue).ListColumn("layout_", _SORT));
        rtn.AddRange(typeof(LineValue).ListColumn("layout_fields_", _SORT+ _SORT_FIELD));
        return rtn;

    }
    public IEnumerable<IEnumerable<string>> ListInsertValue(bool withCamma_) => _listInsertValue(ListLayout, withCamma_);
    private IEnumerable<IEnumerable<string>> _listInsertValue(IEnumerable<AppFormLayoutValue> listLayout_, bool withCamma_, int? count_ =null)
    {
        var rtn =new List<IEnumerable<string>>();

        var layoutCount =0;
        foreach (var layout in listLayout_)
        {
            layout.MainRow = count_ == null ? $"{layoutCount}" : $"{count_}";
            layout.SubRow = count_ == null ? string.Empty : $"{layoutCount}";
            //
            if (layout.ListField.Any() == false)
            {
                // レイアウト保存できないので空データ挿入
                layout.ListField.Add(new());
            }

            var fieldCount = 0;
            foreach (var field in layout.ListField)
            {
                field.Column = $"{fieldCount}";
                //
                var list = ListValue(withCamma_);
                list.AddRange(layout.ListValue(withCamma_));
                list.AddRange(field.ListValue(withCamma_));
                //
                rtn.Add(list);
                fieldCount++;
            }
            //

            rtn.AddRange(_listInsertValue(layout.ListLayout, withCamma_, layoutCount));
            layoutCount++;
        }
        return rtn;
    }
    #endregion
}
