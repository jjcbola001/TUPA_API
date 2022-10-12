using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using NLog;
using SharedModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedInterfaces.Ilogging
{
    public class NLogging : ILogging
    {

        public string GetConnString()
        {
            string jsonFile = $"appsettings.json";

            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile(jsonFile, optional: false, reloadOnChange: true)
            .AddEnvironmentVariables();

            IConfigurationRoot configuration = builder.Build();


            //var ConString = configuration.GetConnectionString("PrimaryDatabaseConnectionString");

            ConnectionStrings connectionStrings = new ConnectionStrings();
            configuration.GetSection("ConnectionStrings").Bind(connectionStrings);
            return connectionStrings.PrimaryDatabaseConnectionString;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="Username"></param>
        public void LogAction(string str, string Username = null)
        {
            GlobalDiagnosticsContext.Set("Username", Username);
            NLog.Logger Logger = NLog.LogManager.GetLogger("ActionLog");
            Logger.Info(str);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="Object"></param>
        /// <param name="OldValue"></param>
        /// <param name="Username"></param>
        public void LogAuditTrail(LogAuditType type, object Object, object OldValue = null, string Username = null)
        {
            GlobalDiagnosticsContext.Set("Username", Username);
            NLog.Logger Logger = NLog.LogManager.GetLogger("LogAudit");
            var ObjetoJson = JsonConvert.SerializeObject(Object);
            StringBuilder s = new StringBuilder();
            if (type == LogAuditType.Insert)
            {
                s.AppendLine("Insert  : ");
                s.AppendLine(ObjetoJson);
                s.AppendLine();
            }
            else if (type == LogAuditType.Delete)
            {
                s.AppendLine("Delete  : ");
                s.AppendLine(ObjetoJson);
                s.AppendLine();
            }
            else if (type == LogAuditType.Update)
            {
                var OldVal = JsonConvert.SerializeObject(OldValue);
                s.AppendLine("Update  : ");
                s.AppendLine("New Value: " + ObjetoJson);
                s.AppendLine("Old Value: " + OldVal);
                s.AppendLine();
            }
            Logger.Info(s);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="functionName"></param>
        /// <param name="ex"></param>
        /// <param name="Username"></param>
        public void LogError(string functionName, Exception ex, string Username = null, string param = null)
        {
            // var ParamLog = new ParamLogUtility();
            string Parameter = string.Empty;
            GlobalDiagnosticsContext.Set("Username", Username);
            NLog.Logger Logger = NLog.LogManager.GetLogger("Error");
            StringBuilder s = new StringBuilder();
            if (param != null)
            {
                s.AppendLine();
                s.AppendLine($"PARAMETER: {param}");
            }


            Exception e = ex;
            while (e != null)
            {
                s.AppendLine($"{functionName}");
                s.AppendLine("Exception type : " + e.GetType().FullName);
                s.AppendLine("Message        : " + e.Message);
                s.AppendLine("Stacktrace     : " + CustomStackTraceMsg(e.StackTrace));
                s.AppendLine("Inner Exception: " + e.InnerException);
                s.AppendLine();
                e = e.InnerException;
            }
            Logger.Error(s);

            //var log = new ErrorLog();
            //log.MethodName = functionName;
            //log.Message = ex.Message;
            //log.ExceptionType = ex.GetType().FullName;
            //log.StackTrace = ex.StackTrace;
            //log.InnerException = ex.InnerException == null ? "" : ex.InnerException.ToString();
            ////            Action
            ////UserID
            ////Parameter
            ////SessionID
            ////MethodName
            ////ExceptionType
            ////StackTrace
            ////InnerException
            ////CreatedDate
            //var cons = new SqlConnection(GetConnString());
            //try
            //{
            //    s.Clear();
            //    s.Append("INSERT INTO [dbo].[ErrorLog] ([Action] ,[UserID] ,[Parameter] ,[SessionID] ,[Message] ,[MethodName] ,[ExceptionType] ,[StackTrace],[InnerException] )");
            //    s.Append(" VALUES(@Action,@UserID,@Parameter,@SessionID,@Message,@MethodName,@ExceptionType,@StackTrace,@InnerException)");

            //    var _param = new DynamicParameters();
            //    _param.Add("@Action", log.MethodName);
            //    _param.Add("@UserID", 0);
            //    _param.Add("@Parameter", param);
            //    _param.Add("@SessionID", "");
            //    _param.Add("@Message", log.Message);
            //    _param.Add("@MethodName", log.MethodName);
            //    _param.Add("@ExceptionType", log.ExceptionType);
            //    _param.Add("@StackTrace", log.StackTrace);
            //    _param.Add("@InnerException", log.InnerException);
            //    cons.Open();
            //    cons.Execute(s.ToString(), _param, commandType: CommandType.Text);
            //}
            //catch (Exception essss)
            //{

            //}
            //finally
            //{
            //    cons.Close();
            //}


        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="Username"></param>
        public void LogError(string str, string Username = null)
        {
            GlobalDiagnosticsContext.Set("Username", Username);
            NLog.Logger Logger = NLog.LogManager.GetLogger("Error");
            Logger.Error(str);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="Username"></param>
        public void LogInformation(string str, string Username = null)
        {
            GlobalDiagnosticsContext.Set("Username", Username);
            NLog.Logger Logger = NLog.LogManager.GetLogger("Info");
            Logger.Info(str);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="functionName"></param>
        /// <param name="ex"></param>
        /// <param name="Username"></param>
        /// <param name="Param"></param>
        public void LogError(string functionName, Exception ex, string Username = null, params object[] Param)
        {
            string Parameter = string.Empty;
            GlobalDiagnosticsContext.Set("Username", Username);
            NLog.Logger Logger = NLog.LogManager.GetLogger("Error");
            StringBuilder s = new StringBuilder();
            //var paramLog = new ParamLogUtility(() => aPersonEntity, () => age, () => id, () => name).GetLog();

            if (Param != null)
            {
                if (Param.Count() > 0)
                {
                    List<object> ParamPass = new List<object>();
                    foreach (var item in Param)
                    {
                        ParamPass.Add(item);
                    }
                    s.AppendLine();
                    s.AppendLine("PARAMETER: " + JsonConvert.SerializeObject(ParamPass));
                    s.AppendLine($"FUNCTION: {functionName}({ JsonConvert.SerializeObject(ParamPass)}) ");
                    s.AppendLine();
                }
                else
                {
                    s.AppendLine(functionName);
                }
            }
            else
            {
                s.AppendLine(functionName);
            }


            Exception e = ex;
            while (e != null)
            {
                s.AppendLine("Exception type : " + e.GetType().FullName);
                s.AppendLine("Message        : " + e.Message);
                s.AppendLine("Stacktrace     : " + CustomStackTraceMsg(e.StackTrace));
                s.AppendLine("Inner Exception: " + e.InnerException);
                s.AppendLine();
                e = e.InnerException;
            }


            Logger.Error(s);
        }

        public enum LogAuditType
        {
            Insert,
            Update,
            Delete
        }

        private string CustomStackTraceMsg(string msg)
        {
            String[] spearator = { " at " };
            Int32 count = 1;

            // using the method 
            String[] splitSTack = msg.Split(spearator, count,
                   StringSplitOptions.RemoveEmptyEntries);

            // var splitSTack = msg.Split(" at ");

            var sb = new StringBuilder();
            try
            {

                foreach (var item in splitSTack)
                {
                    if (!string.IsNullOrEmpty(item.Trim()))
                    {
                        var FstIndex = item.IndexOf(" in ") + " in ".Length;
                        var LastIndex = item.LastIndexOf(@"\") + @"\".Length;
                        if (LastIndex > 0)
                        {
                            var tobeDeleted = item.Substring(FstIndex, LastIndex - FstIndex);
                            sb.Append(item.Replace(tobeDeleted, ""));
                        }
                        else
                        {
                            sb.Append(item);
                        }
                    }
                }
                return sb.ToString();
            }
            catch (Exception ex)
            {
                LogError(nameof(CustomStackTraceMsg), ex);
                return string.Empty;
            }
        }
    }



}
