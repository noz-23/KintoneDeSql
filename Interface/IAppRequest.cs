using KintoneDeSql.Managers;
using KintoneDeSql.Responses.Apps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace KintoneDeSql.Interface;

internal interface IAppRequest
{
    string _COMMAND{get;}

    Task<T?> Get<T>(string appId_);
    Task<T?> Insert<T>(string appId_);
}
