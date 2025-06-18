/*
 * Kintone De Sql
 * Copyright (c) 2025 noz-23
 *  https://github.com/noz-23/
 * 
 * Licensed under the MIT License 
 * 
 */
using Dapper;
using KintoneDeSql.Data;
using KintoneDeSql.Extensions;
using KintoneDeSql.Files;
using KintoneDeSql.Views;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Xml.Linq;

namespace KintoneDeSql.Managers;

/// <summary>
/// SQL 操作処理
/// </summary>
internal sealed class SQLiteManager : BaseSingleton<SQLiteManager>, IDisposable
{
    public void Create(string path_ = _FILE_NAME)
    {
        _path = path_;
        //
        DeleteFile();
        //
        LogFile.Instance.WriteLine("START");
        SqlMapper.AddTypeHandler(DateTimeHandler.Default);
        Open(_path);
        //
    }

    /// <summary>
    /// アタッチするデータベース名
    /// </summary>
    public const string MAIN_DATABASE = "(main)";
    public const string APP_DATABASE = "app";
    public const string SUB_DATABASE = "sub";
    public const string FIELD_DATABASE = "field";
    public const string RECORD_DATABASE = "record";
    public const string SPACE_DATABASE = "space";
    public const string CYBOZU_DATABASE = "cybozu";

    public const string MAIN_MASTER = "sqlite_master";
    public const string APP_MASTER = $"{APP_DATABASE}.{MAIN_MASTER}";
    public const string SUB_MASTER = $"{SUB_DATABASE}.{MAIN_MASTER}";
    public const string FIELD_MASTER = $"{FIELD_DATABASE}.{MAIN_MASTER}";
    public const string RECORD_MASTER = $"{RECORD_DATABASE}.{MAIN_MASTER}";
    public const string SPACE_MASTER = $"{SPACE_DATABASE}.{MAIN_MASTER}";
    public const string CYBOZU_MASTER = $"{CYBOZU_DATABASE}.{MAIN_MASTER}";

    /// <summary>
    /// アタッチするデータベースファイル名
    /// </summary>
    private const string _FILE_NAME = @".\KintoneDeSql.db";
    private const string _APP_FILE_NAME = $".\\KintoneDeSql_{APP_DATABASE}.db";
    private const string _SUB_FILE_NAME = $".\\KintoneDeSql_{SUB_DATABASE}.db";
    private const string _FIELD_FILE_NAME = $".\\KintoneDeSql_{FIELD_DATABASE}.db";
    private const string _RECORD_FILE_NAME = $".\\KintoneDeSql_{RECORD_DATABASE}.db";
    private const string _SPACE_FILE_NAME = $".\\KintoneDeSql_{SPACE_DATABASE}.db";
    private const string _CYBOZU_FILE_NAME = $".\\KintoneDeSql_{CYBOZU_DATABASE}.db";


    /// <summary>
    /// メインのファイル名
    /// </summary>
    private string _path = _FILE_NAME;

    /// <summary>
    /// コネクション
    /// </summary>

    private SQLiteConnection? _connection=null;

    /// <summary>
    /// オープン処理
    /// </summary>
    /// <param name="path_"></param>
    public void Open(string path_)
    {
        LogFile.Instance.WriteLine($"Open[{path_}]");

        var config = new SQLiteConnectionStringBuilder()
        {
            DataSource = path_
        };

        _connection = new SQLiteConnection(config.ToString());
        if (_connection != null)
        {
            _connection.Open();
            //
            // アタッチ処理
            _connection.Execute($"ATTACH '{_APP_FILE_NAME}' AS {APP_DATABASE}");
            _connection.Execute($"ATTACH '{_SUB_FILE_NAME}' AS {SUB_DATABASE}");
            _connection.Execute($"ATTACH '{_FIELD_FILE_NAME}' AS {FIELD_DATABASE}");
            _connection.Execute($"ATTACH '{_RECORD_FILE_NAME}' AS {RECORD_DATABASE}");
            _connection.Execute($"ATTACH '{_CYBOZU_FILE_NAME}' AS {CYBOZU_DATABASE}");
            _connection.Execute($"ATTACH '{_SPACE_FILE_NAME}' AS {SPACE_DATABASE}");
        }
    }

    /// <summary>
    /// クローズ処理
    /// </summary>
    public void Close()
    {
        LogFile.Instance.WriteLine($"Close");
        //
        _connection?.Execute($"DETACH {APP_DATABASE}");
        _connection?.Execute($"DETACH {SUB_DATABASE}");
        _connection?.Execute($"DETACH {FIELD_DATABASE}");
        _connection?.Execute($"DETACH {RECORD_DATABASE}");
        _connection?.Execute($"DETACH {CYBOZU_DATABASE}");
        _connection?.Execute($"DETACH {SPACE_DATABASE}");

        _connection?.Close();
        _connection?.Dispose();
        _connection = null;
    }

    /// <summary>
    /// ファイルを削除
    /// </summary>
    public void DeleteFile()
    {
        LogFile.Instance.WriteLine($"Delete");
        //
        Close();
        //
        File.Delete(_FILE_NAME);
        File.Delete(_APP_FILE_NAME);
        File.Delete(_SUB_FILE_NAME);
        File.Delete(_FIELD_FILE_NAME);
        File.Delete(_CYBOZU_FILE_NAME);
        File.Delete(_SPACE_FILE_NAME);
    }


    /// <summary>
    /// Using を利用するため
    /// </summary>
    public void Dispose()
    {
        Close();
    }

    /// <summary>
    /// テーブル作成
    /// </summary>
    /// <param name="tableName_"></param>
    /// <param name="listColumn_"></param>
    public void CreateTable(string tableName_, IEnumerable<string> listColumn_)
    {
        if (string.IsNullOrEmpty(tableName_) == true)
        {
            return;
        }

        var query = $"CREATE TABLE IF NOT EXISTS {tableName_} ({string.Join(",", listColumn_)});";
        LogFile.Instance.WriteLine($"[{query}]");
        try
        {
            _connection?.Execute(query);
        }
        catch (Exception ex_)
        {
            LogFile.Instance.WriteLine($"{ex_.Message}\n{query}");
        }
    }

    /// <summary>
    /// テーブル削除
    /// </summary>
    /// <param name="tableName_"></param>
    public void DropTable(string tableName_)
    {
        if (string.IsNullOrEmpty(tableName_) == true)
        {
            return;
        }

        var query = $"DROP TABLE IF EXISTS {tableName_};";
        LogFile.Instance.WriteLine($"[{query}]");
        try
        {
            _connection?.Execute(query);
        }
        catch (Exception ex_)
        {
            LogFile.Instance.WriteLine($"{ex_.Message}\n{query}");
        }
    }

    /// <summary>
    /// テーブル選択
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="withCammna_"></param>
    /// <returns></returns>
    public IEnumerable<T> SelectTable<T>(bool withCammna_, string where_ = "")
    {
        var query = $"SELECT * FROM {typeof(T).TableName(withCammna_)} {where_}";
        try
        {
            var list = _connection?.Query<T>(query);
            return list ?? new List<T>();
        }
        catch (Exception ex_)
        {
            LogFile.Instance.WriteLine($"{ex_.Message}\n{query}");
        }

        return new List<T>();
    }

    public DataTable SelectTable(string tableName_,string where_="")
    {
        var rtn =new DataTable();

        var query = $"SELECT * FROM {tableName_} {where_}";
        try
        {
            var data = _connection?.ExecuteReader(query);
            if (data != null)
            {
                rtn.Load(data);
            }
        }
        catch (Exception ex_)
        {
            LogFile.Instance.WriteLine($"{ex_.Message}\n{query}");
        }

        return rtn;
    }

    /// <summary>
    /// SQL 実行
    /// </summary>
    /// <param name="query_"></param>
    /// <returns></returns>
    public DataTable ExecuteSql(string query_)
    {
        var rtn = new DataTable();
        LogFile.Instance.WriteLine($"[{query_}]");
        try
        {
            var data = _connection?.ExecuteReader(query_);
            if (data != null)
            {
                rtn.Load(data);
            }
        }
        catch (Exception ex_)
        {
            LogFile.Instance.WriteLine($"{ex_.Message}\n{query_}");
        }

        return rtn;
    }

    /// <summary>
    /// テーブル存在確認
    /// </summary>
    /// <param name="tableName_"></param>
    /// <returns></returns>
    public bool ExsistsTable(string tableName_)
    {
        var query = $"SELECT name FROM {MAIN_MASTER} WHERE type='table' AND name='{tableName_}';";
        LogFile.Instance.WriteLine($"[{query}]");
        try
        {
            var data = _connection?.Query(query);
            if (data != null && data.Any() == true)
            {
                return true;
            }
        }
        catch (Exception ex_)
        {
            LogFile.Instance.WriteLine($"{ex_.Message}\n{query}");
        }
        return false;
    }

    /// <summary>
    /// データ挿入
    /// </summary>
    /// <param name="tableName_"></param>
    /// <param name="listColumn_"></param>
    /// <param name="listValue_"></param>
    public void InsertTable(string tableName_, IEnumerable<string> listColumn_, IEnumerable<IEnumerable<string>>listValue_)
    {
        if (listValue_.Any() == false)
        {
            return;
        }

        //
        using (var tran = _connection?.BeginTransaction())
        {
            // トランザクションしないと遅い
            var query = $"REPLACE INTO  {tableName_} ({string.Join(",", listColumn_)}) ";
            var queryValues = string.Empty;
            LogFile.Instance.WriteLine($"[{query}]({listValue_.Count()})");
            try
            {
                foreach (var item in listValue_)
                {
                    queryValues = query + $"VALUES({string.Join(",", item)});";
                    _connection?.Execute(queryValues);
                }
                tran?.Commit();
            }
            catch (Exception ex_)
            {
                tran?.Rollback();
                LogFile.Instance.WriteLine($"{ex_.Message}\n{queryValues}");
            }
        }
    }

    /// <summary>
    /// データ削除
    /// </summary>
    /// <param name="tableName_"></param>
    /// <param name="where_"></param>
    public void DeleteTable(string tableName_,string where_) 
    {
        var query = $"DELETE FROM {tableName_} WHERE {where_};";
        LogFile.Instance.WriteLine($"[{query}]");
        try
        {
            _connection?.Execute(query);
        }
        catch (Exception ex_)
        {
            LogFile.Instance.WriteLine($"{ex_.Message}\n{query}");
        }
    }


    public IList<string> ListTableName(string master_= MAIN_MASTER)
    {
        string dbName = master_.Substring(0, master_.IndexOf('.')+1);
        var query =$"SELECT '{dbName}'||name FROM {master_} WHERE type='table';";
        LogFile.Instance.WriteLine($"[{query}]");
        try
        {
            var data = _connection?.Query<string>(query);
            var rtn = data?.ToList();
            if (rtn == null)
            { 
                return new List<string>();
            }
            rtn.Sort();
            return rtn;
        }
        catch (Exception ex_)
        {
            LogFile.Instance.WriteLine($"{ex_.Message}\n{query}");
        }
        return new List<string>();
    }

    public IList<string> ListColumn(string tableName_)
    {
        string dbName = tableName_.Substring(0, tableName_.IndexOf('.') + 1);
        var query = $"PRAGMA {dbName}table_info ({tableName_.Substring(tableName_.IndexOf('.') + 1)});";
        LogFile.Instance.WriteLine($"[{query}]");
        try
        {
            var list = _connection?.Query<SqlTableView>(query);
            return list?.Select(view_=>view_.Name).ToList() ?? new List<string>();
        }
        catch (Exception ex_)
        {
            LogFile.Instance.WriteLine($"{ex_.Message}\n{query}");
        }
        return new List<string>();
    }
}
