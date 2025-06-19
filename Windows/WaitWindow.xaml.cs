/*
 * Kintone De Sql
 * Copyright (c) 2025 noz-23
 *  https://github.com/noz-23/
 * 
 * Licensed under the MIT License 
 * 
 */
using System.Windows;

namespace KintoneDeSql.Windows;

/// <summary>
/// WaitWindow.xaml の相互作用ロジック
/// 待ち表示ウィンドウ
/// </summary>
public partial class WaitWindow : Window
{
    /// <summary>
    /// コンストラクタ
    /// </summary>
    public WaitWindow()
    {
        InitializeComponent();
    }

    /// <summary>
    /// 実処理
    /// </summary>
    /// <returns></returns>
    public delegate Task RunCallBack();

    /// <summary>
    /// プログレスバー処理
    /// </summary>
    /// <param name="count_"></param>
    /// <param name="max_"></param>
    /// <param name="str_"></param>
    public delegate void ProgressCountCallBack(int count_, int max_=1, string str_ = "");

    /// <summary>
    /// 処理数
    /// </summary>
    //public int Count { get; set; } = 0;

    /// <summary>
    /// 処理内容
    /// </summary>
    public RunCallBack? Run { get; set; } = null;

    /// <summary>
    /// ロード処理
    /// </summary>
    /// <param name="sender_"></param>
    /// <param name="e_"></param>
    private async void _loaded(object sender_, RoutedEventArgs e_)
    {
        // 実行して
        if (Run != null)
        {
            await Run.Invoke();
        }
        // 閉じる
        Close();
    }

    /// <summary>
    /// プログレスバー更新
    /// </summary>
    /// <param name="count_"></param>
    /// <param name="max_"></param>
    /// <param name="str_"></param>
    public void ProgressCount(int count_, int max_=1, string str_ = "")
    {
        App.Current.Dispatcher.Invoke(() =>
        {
            if (count_ == 0)
            {
                // カウントが0の場合に最大数と文字列を更新
                _textBlock.Text = str_;
                _progressBar.Maximum = max_;
            }
            _progressBar.Value = count_;
            _textProgress.Text = $"{_progressBar.Value} / {_progressBar.Maximum}";

        });
    }
}
