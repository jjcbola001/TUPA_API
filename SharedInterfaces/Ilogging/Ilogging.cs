using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SharedInterfaces.Ilogging.NLogging;

namespace SharedInterfaces.Ilogging
{
    public interface ILogging
    {
        void LogInformation(string str, string Username = null);
        void LogAction(string str, string Username = null);
        void LogError(string functionName, Exception ex, string Username = null, string param = null);
        void LogError(string str, string Username = null);
        void LogError(string functionName, Exception ex, string Username = null, params object[] Param);
        void LogAuditTrail(LogAuditType type, object Object, object OldValue = null, string Username = null);
    }
}
