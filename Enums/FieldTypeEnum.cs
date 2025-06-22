/*
 * Kintone De Sql
 * Copyright (c) 2025 noz-23
 *  https://github.com/noz-23/
 * 
 * Licensed under the MIT License 
 * 
 */
using System.ComponentModel;

namespace KintoneDeSql.Enums;

/// <summary>
/// 処理内容の振分け
/// </summary>
internal enum FieldToDatabaseTypeEnum
{
    [Description("文字列")]
    TEXT,
    [Description("配列")] // クラスによる(基本別テーブル)
    LIST,
    [Description("展開")] // クラスをばらす
    EXTRACT,
    [Description("テーブル")]   // 別テーブル保存
    SUBTABLE,
}


/// <summary>
/// https://cybozu.dev/ja/kintone/docs/overview/field-types/
/// 文字列に置き換え予定
/// </summary>
internal enum FieldTypeEnum
{
    // レコード番号
    RECORD_NUMBER,

    // レコードID
    __ID__,

    // 作成者
    CREATOR,

    // 作成日時
    CREATED_TIME,

    // 更新者
    MODIFIER,

    // 更新日時
    UPDATED_TIME,

    // 文字列（1行）
    SINGLE_LINE_TEXT,

    // 文字列（複数行）
    MULTI_LINE_TEXT,

    // リッチエディター
    RICH_TEXT,

    // 数値
    NUMBER,

    // 計算
    CALC,

    // チェックボックス
    CHECK_BOX,

    // ラジオボタン
    RADIO_BUTTON,

    // 複数選択
    MULTI_SELECT,

    // ドロップダウン
    DROP_DOWN,

    // ユーザー選択
    USER_SELECT,

    // 組織選択
    ORGANIZATION_SELECT,

    // グループ選択
    GROUP_SELECT,

    // 日付
    DATE,

    // 時刻
    TIME,

    // 日時
    DATETIME,

    // リンク
    LINK,

    // 添付ファイル
    FILE,

    // ルックアップ
    //SINGLE_LINE_TEXT,

    // テーブル
    SUBTABLE,

    // 関連レコード一覧
    REFERENCE_TABLE,

    // カテゴリー
    CATEGORY,

    // ステータス
    STATUS,

    // 作業者
    STATUS_ASSIGNEE,

    // ラベル
    LABEL,
    // スペース
    SPACER,

    // グループ
    GROUP,
}