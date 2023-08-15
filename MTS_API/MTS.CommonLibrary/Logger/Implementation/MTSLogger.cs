using MTS.CommonLibrary.Logger.Abstraction;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Text;
//using Serilog.Sinks.MSSqlServer.Sinks.MSSqlServer.Options;

namespace MTS.CommonLibrary.Logger.Implementation
{
    public class MTSLogger : IMTSLogger
    {
        public IConfiguration Configuration { get; }
        public MTSLogger(IConfiguration configuration)
        {
            Configuration = configuration; ;
            LoadConfigurtion();
        }

        private void LoadConfigurtion()
        {

#pragma warning disable CS0618 // Type or member is obsolete
            Log.Logger = new LoggerConfiguration().WriteTo
                .MSSqlServer(
                    connectionString: Configuration.GetConnectionString("DefaultConnection"),
                    schemaName: "dbo",
                    tableName: "Logs",
                    autoCreateSqlTable: true,
                    //restrictedToMinimumLevel: LogEventLevel.Debug,
                    formatProvider: null,
                    columnOptions: null,
                    logEventFormatter: null)
                .CreateLogger();
#pragma warning restore CS0618 // Type or member is obsolete
        }

        // Warning - trace information within the application 
        public void Information(string message)
        {
            //Trace.TraceInformation(message);
            Serilog.Log.Information(message);
        }
        public void Information(string fmt, params object[] vars)
        {
            Serilog.Log.Information(fmt, vars);
            //Trace.TraceInformation(fmt, vars);
        }

        public void Information(Exception exception, string fmt, params object[] vars)
        {
            Serilog.Log.Information(string.Format(fmt, vars) + ";Exception Details={0}", exception.ToString());
        }

        // Warning - trace warnings within the application 
        public void Warning(string message)
        {
            Serilog.Log.Warning(message);
        }
        public void Warning(string fmt, params object[] vars)
        {
            Serilog.Log.Warning(fmt, vars);
        }
        public void Warning(Exception exception, string fmt, params object[] vars)
        {
            Serilog.Log.Warning(string.Format(fmt, vars) + ";Exception Details={0}", exception.ToString());
        }
        //
        // Error - trace fatal errors within the application 
        public void Error(string message)
        {
            Serilog.Log.Error(message);
        }
        public void Error(string fmt, params object[] vars)
        {
            Serilog.Log.Error(fmt, vars);
        }
        public void Error(Exception exception, string fmt, params object[] vars)
        {
            Serilog.Log.Error(string.Format(fmt, vars) + ";Exception Details={0}", exception.ToString());
        }
        //
        // TraceAPI - trace inter-service calls (including latency)
        public void TraceApi(string componentName, string method, TimeSpan timespan)
        {
            TraceApi(componentName, method, timespan, "");
        }
        public void TraceApi(string componentName, string method, TimeSpan timespan, string fmt, params object[] vars)
        {
            TraceApi(componentName, method, timespan, string.Format(fmt, vars));
        }
        public void TraceApi(string componentName, string method, TimeSpan timespan, string properties)
        {
            string message = String.Concat("component:", componentName, ";method:", method, ";timespan:", timespan.ToString(), ";properties:", properties);
            Serilog.Log.Information(message);
        }
    }
}
