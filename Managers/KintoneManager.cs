/*
 * Kintone De Sql
 * Copyright (c) 2025 noz-23
 *  https://github.com/noz-23/
 * 
 * Licensed under the MIT License 
 * 
 */
using KintoneDeSql.Data;
using KintoneDeSql.Files;
using KintoneDeSql.Interface;
using KintoneDeSql.Responses.Apps;
using KintoneDeSql.Responses.Records;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace KintoneDeSql.Managers;

/// <summary>
/// kintone Rest APIを操作
/// https://github.com/icoxfog417/kintoneDotNET 参考
/// </summary>
internal class KintoneManager : BaseSingleton<KintoneManager>
{
    /// <summary>
    /// 初期化
    /// </summary>
    public void Create()
    {
        LogFile.Instance.WriteLine(@"START");
        //
        _domain = string.Empty;
        _accessId = string.Empty;
        _accessPassword = string.Empty;
        _loginId = string.Empty;
        _loginPassword = string.Empty;
    }

    /// <summary>
    /// 各種の取得制限値
    /// </summary>
    public const int RECORD_LIMIT = 500;
    public const int COMMENT_LIMIT = 10;
    public const int CYBOZU_LIMIT = 100;
    public const int API_LIMIT = 300;

    /// <summary>
    /// ドメイン設定
    /// </summary>
    /// <param name="domain_"></param>
    public void Domain(string domain_)
    {
        _domain = domain_;
    }   

    /// <summary>
    /// アクセスアカウント設定
    /// </summary>
    /// <param name="accessId"></param>
    /// <param name="accessPassword_"></param>
    public void AccessAccount(string accessId,string accessPassword_)
    { 

        _accessId = accessPassword_;
        _accessPassword = accessPassword_;
    }

    /// <summary>
    /// ログインアカウント設定
    /// </summary>
    /// <param name="loginId"></param>
    /// <param name="loginPassword_"></param>
    public void LoginAccount( string loginId, string loginPassword_)
    {
        _loginId = loginId;
        _loginPassword = loginPassword_;
    }

    /// <summary>
    /// 各種設定
    /// </summary>
    private string _domain = string.Empty;
    private string _accessId = string.Empty;
    private string _accessPassword = string.Empty;
    private string _loginId = string.Empty;
    private string _loginPassword { get; set; } = string.Empty;


    /// <summary>
    /// kintoneDotNET 参考
    /// </summary>
    protected const string KINTONE_HOST = "{0}.cybozu.com:443";

    /// <summary>
    /// protected const string KINTONE_PORT = "443";
    /// TODO:APIのバージョンが上がれば変更する必要あり
    /// .jsonはApi Schema取得時ないほうが良いためつけない
    /// </summary>

    protected const string KINTONE_API_FORMAT = "https://{0}/k/v1/{1}"; // .json

    protected const string CYBOZE_API_FORMAT = "https://{0}/v1/{1}";   // .json

    /// <summary>
    /// kintoneのアクセス先。"xxx.cybozu.com"というようなアドレスで表現される(xxxはDomain)
    /// </summary>
    /// <value></value>
    public string Host { get => string.Format(KINTONE_HOST, _domain); }

    /// <summary>
    /// kintoneのログインを行うためのキーを作成する
    /// </summary>
    /// <value></value>
    /// <returns></returns>
    /// <remarks></remarks>
    private string LoginKey
    {
        get
        {
            if (!string.IsNullOrEmpty(_loginId) && !string.IsNullOrEmpty(_loginPassword))
            {
                return Convert.ToBase64String(Encoding.UTF8.GetBytes(_loginId + ":" + _loginPassword));
            }
            return string.Empty;
        }
    }

    /// <summary>
    /// Basic認証のためのキーを作成する。形式については<a href="http://developers.cybozu.com/ja/kintone-api/common-appapi.html">公式ドキュメント</a>を参照
    /// </summary>
    /// <value></value>
    /// <returns></returns>
    /// <remarks></remarks>
    private string AccessKey
    {
        get
        {
            if (!string.IsNullOrEmpty(_accessId) && !string.IsNullOrEmpty(_accessPassword))
            {
                return Convert.ToBase64String(Encoding.UTF8.GetBytes(_accessId + ":" + _accessPassword));
            }
            return string.Empty;
        }
    }

    /// <summary>
    /// Cybozu API 利用時のヘッダ作成
    /// </summary>
    /// <param name="method_"></param>
    /// <param name="command_"></param>
    /// <param name="query_"></param>
    /// <param name="parameter_"></param>
    /// <returns></returns>
    private HttpRequestMessage cybozuHeader(HttpMethod method_, string command_, string query_ = "", string parameter_ = "")
    {
        LogFile.Instance.WriteLine($"Command[{command_.ToString()}] Method[{method_}] Query[{query_}] Parameter[{parameter_}]");
        //
        var uri = string.Format(CYBOZE_API_FORMAT, Host, command_);
        if (!string.IsNullOrEmpty(query_))
        {
            uri += "?" + query_;
        }
        LogFile.Instance.WriteLine($"{uri.ToString()}");

        return makeHeader(method_, new Uri(uri), parameter_);
    }

    /// <summary>
    /// Kintone API 利用時のヘッダ作成
    /// </summary>
    /// <param name="method_"></param>
    /// <param name="command_"></param>
    /// <param name="query_"></param>
    /// <param name="parameter_"></param>
    /// <param name="apiToken_"></param>
    /// <returns></returns>
    private HttpRequestMessage kintoneHeader(HttpMethod method_, string command_, string query_ = "", string parameter_ = "", string apiToken_ = "")
    {
        LogFile.Instance.WriteLine($"Command[{command_.ToString()}] Method[{method_}] Query[{query_}] Parameter[{parameter_}] Token[{apiToken_}]");
        //
        var uri = string.Format(KINTONE_API_FORMAT, Host, command_);
        if (!string.IsNullOrEmpty(query_))
        {
            uri += "?" + query_;
        }
        LogFile.Instance.WriteLine($"{uri.ToString()}");

        return makeHeader(method_, new Uri(uri), parameter_, apiToken_);
    }


    /// <summary>
    /// kintone/CybozuにアクセスするためのHTTPヘッダ作成
    /// </summary>
    /// <param name="method_"></param>
    /// <param name="uri_"></param>
    /// <param name="parameter_"></param>
    /// <param name="apiToken_"></param>
    /// <returns></returns>
    private HttpRequestMessage makeHeader(HttpMethod method_, Uri uri_, string parameter_ = "", string apiToken_ ="")
    {
        var request = new HttpRequestMessage(method_, uri_);

        if ( string.IsNullOrEmpty(apiToken_) ==false)
        {
            // APIトークン
            // https://cybozu.dev/ja/kintone/tips/development/customize/development-know-how/api-tokens/
            request.Headers.Add("X-Cybozu-API-Token", apiToken_);
            //LogFile.Instance.WriteLine($"API-Token {apiToken_.ToString()}");
        }
        else
        {
            // 
            // https://cybozu.dev/ja/kintone/docs/rest-api/overview/authentication/
            request.Headers.Add("X-Cybozu-Authorization", LoginKey);
            //LogFile.Instance.WriteLine($"Authorization {LoginKey.ToString()}");
        }

        if (string.IsNullOrEmpty(AccessKey) == false)
        {
            // https://jp.cybozu.help/general/ja/admin/list_security/list_access/basic_auth.html
            request.Headers.Add("Authorization", "Basic " + AccessKey);
            //LogFile.Instance.WriteLine($"Basic {AccessKey.ToString()}");
        }

        if (string.IsNullOrEmpty(parameter_) == false)
        {
            var json = new StringContent(parameter_, Encoding.UTF8, "application/json");
            request.Content = json;
            //LogFile.Instance.WriteLine($"{json.ToString()} {parameter_}");
        }

        //ContentTypeの設定。デフォルト設定という感じにし、複雑な分岐・設定が必要な場合個別のメソッド側で実装する
        //GET(読込)の場合、ContentTypeを指定しない(指定すると400 BadRequestエラー)
        //if (!(method_ == HttpMethod.Get))
        //{
        //    request.Content = new StringContent("application/json");
        //}
        return request;

    }

    /// <summary>
    /// Kintone レスポンス取得
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="method_"></param>
    /// <param name="command_"></param>
    /// <param name="query_"></param>
    /// <param name="parameter_"></param>
    /// <param name="apiToken_"></param>
    /// <returns></returns>
    public async Task<T?> KintoneGet<T>(HttpMethod method_, string command_, string query_ = "", string parameter_ = "", string apiToken_ = "") where T : class?, new()
        => await get<T>(kintoneHeader(HttpMethod.Get, command_, query_, parameter_, apiToken_));

    /// <summary>
    /// Cybozu レスポンス取得
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="method_"></param>
    /// <param name="command_"></param>
    /// <param name="query_"></param>
    /// <param name="parameter_"></param>
    /// <returns></returns>
    public async Task<T?> CybozuGet<T>(HttpMethod method_, string command_, string query_ = "", string parameter_ = "") where T : class?, new()
        => await get<T>(cybozuHeader(HttpMethod.Get, command_, query_, parameter_));

    /// <summary>
    /// Json(class)取得処理
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="request_"></param>
    /// <returns></returns>
    private async Task<T?> get<T>(HttpRequestMessage request_) where T :class?,new()
    {
        LogFile.Instance.WriteLine($"Get[{typeof(T).ToString()}]");

        using (var client = new HttpClient())
        {
            var response = await client.SendAsync(request_);
            if( response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                LogFile.Instance.WriteLine($"Error Http Request\n{response.StatusCode}");
                return null;
            }
            var readJson = await response.Content.ReadAsStringAsync();

            try
            {
                var rtn = JsonSerializer.Deserialize<T>(readJson, BaseFieldValue.Ooptions) ?? new T();
                return rtn;
            }
            catch (Exception ex_)
            {
                LogFile.Instance.WriteLine($"Error\n{ex_.Message}");
            }
            return null;
        }
    }

    ///
    //public async Task<T?> Get<T>(string command_,string appId_) where T: class?,ISqlTable, IAppId, new()
    //{
    //    var query = string.Empty;
    //    var paramater = JsonSerializer.Serialize(new { app = appId_ });
    //    var rtn = await KintoneGet<T?>(HttpMethod.Get, command_, query, paramater);

    //    if (rtn != null)
    //    {
    //        rtn.AppId = appId_;
    //    }
    //    return rtn;
    //}

    //public async Task<T?> Insert<T>(string command_, string appId_) where T : class?, ISqlTable, IAppId, new()
    //{
    //    var rtn = await Get<T>(command_,appId_);
    //    if (rtn != null)
    //    {
    //        SQLiteManager.Instance.InsertTable(T.TableName(false), T.ListInsertHeader(true), rtn.ListInsertValue(true));
    //    }

    //    return rtn;
    //}
}
