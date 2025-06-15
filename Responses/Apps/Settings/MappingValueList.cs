using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KintoneDeSql.Responses.Apps.Settings;

/// <summary>
/// https://cybozu.dev/ja/kintone/docs/rest-api/apps/settings/get-action-settings/
/// </summary>

internal class MappingValueList : List<MappingValue>
{
    public override string ToString()
    {
        return string.Join("\n",this.Select(x_=>x_.ToString()));
    }
}
