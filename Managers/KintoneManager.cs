/*
 * Reprise Report Log Analyzer
 * Copyright (c) 2025 noz-23
 *  https://github.com/noz-23/
 * 
 * Licensed under the MIT License 
 * 
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KintoneDeSql;
using KintoneDeSql.Files;
using kintoneDotNET.API;

namespace KintoneDeSql.Managers;

internal class KintoneManager
{
    public static KintoneManager Instance=new ();
    private KintoneManager()
    {
    }

    private kintoneAccount _kintoneAccount;
    public void Create()
    {
        LogFile.Instance.WriteLine(@"START");
        //
        _kintoneAccount = new()
        {
            Domain = "kintone.com", // https://xxx.cybozu.com
            AccessId = "xxxxxx",    // Basic Authentication id
            AccessPassword = "xxxxxx",  // Basic Authentication password
            LoginId = "xxxxxx",     // kintone id
            LoginPassword = "xxxxxx", // kintone password
            ApiToken = "xxxxxx", // kintone API token

        };
    }

}
