﻿/*
 * Kintone De Sql
 * Copyright (c) 2025 noz-23
 *  https://github.com/noz-23/
 * 
 * Licensed under the MIT License 
 * 
 */
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;

namespace KintoneDeSql.Files;

/// <summary>
/// ログファイル
/// </summary>
internal sealed class LogFile
{
    /// <summary>
    /// シングルトン
    /// </summary>
    public static LogFile Instance { get; private set; } = new LogFile();

    /// <summary>
    /// コンストラクタ
    /// </summary>
    private LogFile()
    {
    }

    private const string _FILE_NAME = @".\log.txt";

    /// <summary>
    /// ログファイル名
    /// </summary>
    private string _logFileName = _FILE_NAME;

    /// <summary>
    /// 作成処理
    /// </summary>
    public void Create(string logFileName_= _FILE_NAME)
    {
        _logFileName = logFileName_;
        //
        if (File.Exists(_logFileName) == true)
        {
            File.Delete(_logFileName);
        }


        var stream = new StreamWriter(_logFileName);
        stream.AutoFlush = true;

        Trace.Listeners.Remove("Default");
        Trace.Listeners.Add(new TextWriterTraceListener(TextWriter.Synchronized(stream)));
    }
    /// <summary>
    /// ログの書き込み
    /// </summary>
    /// <param name="message_">実ログ</param>
    /// <param name="soruce_">ソース</param>
    /// <param name="line_">行</param>
    /// <param name="member_">関数</param>
    public void WriteLine(string message_, [CallerFilePath] string soruce_ = "", [CallerLineNumber] int line_ = -1, [CallerMemberName] string member_ = "")
    {
        Trace.WriteLine($"{DateTime.Now.ToString(@"yyyy/MM/dd HH:mm:ss", CultureInfo.InvariantCulture)} [{Path.GetFileName(soruce_)}({line_})][{member_}]\n{message_}\n");
        Trace.Flush();
    }
}
