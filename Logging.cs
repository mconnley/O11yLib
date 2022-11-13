using System.Text;
using System.Diagnostics;
using System.Reflection;
using NLog;

namespace O11yLib;
public class MyLogger : ILogger
{
    private readonly Logger _logger;
    private readonly NLog.Layouts.JsonLayout _jsonLayout;

    bool ILogger.IsTraceEnabled => throw new NotImplementedException();

    bool ILogger.IsDebugEnabled => throw new NotImplementedException();

    bool ILogger.IsInfoEnabled => throw new NotImplementedException();

    bool ILogger.IsWarnEnabled => throw new NotImplementedException();

    bool ILogger.IsErrorEnabled => throw new NotImplementedException();

    bool ILogger.IsFatalEnabled => throw new NotImplementedException();

    string ILoggerBase.Name => throw new NotImplementedException();

    LogFactory ILoggerBase.Factory => throw new NotImplementedException();

    public MyLogger()
    {
        var timestampLayout = NLog.Layouts.Layout.FromString("${date:format=o}");
        var levelLayout = NLog.Layouts.Layout.FromString("${level}");
        var loggerLayout = NLog.Layouts.Layout.FromString("${logger}");
        var callsiteLayout = NLog.Layouts.Layout.FromString("${callsite:className=True:fileName=True:includeSourcePath=True:methodName=True}");
        var bodyLayout = NLog.Layouts.Layout.FromString("${message:raw=true");
        var attributesLayout = NLog.Layouts.Layout.FromString("${event-properties:item=attributes:format=@}");
        var attributesExceptionLayout = NLog.Layouts.Layout.FromString("${event-properties:item=attributes.exception:format=@}");

        _jsonLayout = new NLog.Layouts.JsonLayout()
        {
            Attributes = {
                new NLog.Layouts.JsonAttribute("timestamp", timestampLayout),
                new NLog.Layouts.JsonAttribute("level", levelLayout),
                new NLog.Layouts.JsonAttribute("logger", loggerLayout),
                new NLog.Layouts.JsonAttribute("instrumentationscope", callsiteLayout),
                new NLog.Layouts.JsonAttribute("body", bodyLayout),
                new NLog.Layouts.JsonAttribute("attributes", attributesLayout, false),
                new NLog.Layouts.JsonAttribute("attributes.exception", attributesExceptionLayout, false)
            }
        };

        var config = new NLog.Config.LoggingConfiguration();
        var logconsole = new NLog.Targets.ConsoleTarget("logconsole")
        {
            Layout = _jsonLayout
        };
        config.AddRule(LogLevel.Info, LogLevel.Fatal, logconsole, "Microsoft.Hosting.Lifetime", true);
        config.AddRule(LogLevel.Warn, LogLevel.Fatal, logconsole, "Microsoft.*", true);
        config.AddRule(LogLevel.Debug, LogLevel.Fatal, logconsole, "*", true);
        NLog.LogManager.Configuration = config;

        _logger = NLog.LogManager.GetLogger(NameOfCallingClass());
    }

    event EventHandler<EventArgs> ILoggerBase.LoggerReconfigured
    {
        add
        {
            throw new NotImplementedException();
        }

        remove
        {
            throw new NotImplementedException();
        }
    }

    public void Trace(string Message, object Attributes)
    {
        LogEventInfo ei = new (LogLevel.Trace, _logger.Name, Message);
        ei.Properties.Add("attributes", Attributes);
        _logger.Log(typeof(MyLogger), ei);
    }

    public void Debug(string Message, object Attributes)
    {
        LogEventInfo ei = new (LogLevel.Debug, _logger.Name, Message);
        ei.Properties.Add("attributes", Attributes);
        _logger.Log(typeof(MyLogger), ei);
    }

    public void Info(string Message, object Attributes)
    {
        LogEventInfo ei = new (LogLevel.Info, _logger.Name, Message);
        ei.Properties.Add("attributes", Attributes);
        _logger.Log(typeof(MyLogger), ei);
    }

    public void Warn(string Message, object Attributes, System.Exception? Exception)
    {
        LogEventInfo ei = new (LogLevel.Info, _logger.Name, Message);
        ei.Properties.Add("attributes", Attributes);
        if (Exception != null) ei.Properties.Add("exception", Exception);
        _logger.Log(typeof(MyLogger), ei);
    }

    public void Error(string Message, object Attributes, System.Exception? Exception)
    {
        LogEventInfo ei = new (LogLevel.Info, _logger.Name, Message);
        ei.Properties.Add("attributes", Attributes);
        if (Exception != null) ei.Properties.Add("exception", Exception);
        _logger.Log(typeof(MyLogger), ei);
    }

    public void Fatal(string Message, object Attributes, System.Exception? Exception)
    {
        LogEventInfo ei = new (LogLevel.Info, _logger.Name, Message);
        ei.Properties.Add("attributes", Attributes);
        if (Exception != null) ei.Properties.Add("exception", Exception);
        _logger.Log(typeof(MyLogger), ei);
    }

    private static string NameOfCallingClass()
    {
        string? fullName;
        Type? declaringType;
        int skipFrames = 2;
        do
        {
            MethodBase? method = new StackFrame(skipFrames, false).GetMethod();
            declaringType = method.DeclaringType;
            if (declaringType == null)
            {
                return method.Name;
            }
            skipFrames++;
            fullName = declaringType.FullName;
        }
        while (declaringType.Module.Name.Equals("mscorlib.dll", StringComparison.OrdinalIgnoreCase));

        return fullName;
    }

    void ILogger.Trace(object value)
    {
        throw new NotImplementedException();
    }

    void ILogger.Trace(IFormatProvider formatProvider, object value)
    {
        throw new NotImplementedException();
    }

    void ILogger.Trace(string message, object arg1, object arg2)
    {
        throw new NotImplementedException();
    }

    void ILogger.Trace(string message, object arg1, object arg2, object arg3)
    {
        throw new NotImplementedException();
    }

    void ILogger.Trace(IFormatProvider formatProvider, string message, bool argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Trace(string message, bool argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Trace(IFormatProvider formatProvider, string message, char argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Trace(string message, char argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Trace(IFormatProvider formatProvider, string message, byte argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Trace(string message, byte argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Trace(IFormatProvider formatProvider, string message, string argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Trace(string message, string argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Trace(IFormatProvider formatProvider, string message, int argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Trace(string message, int argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Trace(IFormatProvider formatProvider, string message, long argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Trace(string message, long argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Trace(IFormatProvider formatProvider, string message, float argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Trace(string message, float argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Trace(IFormatProvider formatProvider, string message, double argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Trace(string message, double argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Trace(IFormatProvider formatProvider, string message, decimal argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Trace(string message, decimal argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Trace(IFormatProvider formatProvider, string message, object argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Trace(string message, object argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Trace(IFormatProvider formatProvider, string message, sbyte argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Trace(string message, sbyte argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Trace(IFormatProvider formatProvider, string message, uint argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Trace(string message, uint argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Trace(IFormatProvider formatProvider, string message, ulong argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Trace(string message, ulong argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Debug(object value)
    {
        throw new NotImplementedException();
    }

    void ILogger.Debug(IFormatProvider formatProvider, object value)
    {
        throw new NotImplementedException();
    }

    void ILogger.Debug(string message, object arg1, object arg2)
    {
        throw new NotImplementedException();
    }

    void ILogger.Debug(string message, object arg1, object arg2, object arg3)
    {
        throw new NotImplementedException();
    }

    void ILogger.Debug(IFormatProvider formatProvider, string message, bool argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Debug(string message, bool argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Debug(IFormatProvider formatProvider, string message, char argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Debug(string message, char argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Debug(IFormatProvider formatProvider, string message, byte argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Debug(string message, byte argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Debug(IFormatProvider formatProvider, string message, string argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Debug(string message, string argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Debug(IFormatProvider formatProvider, string message, int argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Debug(string message, int argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Debug(IFormatProvider formatProvider, string message, long argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Debug(string message, long argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Debug(IFormatProvider formatProvider, string message, float argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Debug(string message, float argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Debug(IFormatProvider formatProvider, string message, double argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Debug(string message, double argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Debug(IFormatProvider formatProvider, string message, decimal argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Debug(string message, decimal argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Debug(IFormatProvider formatProvider, string message, object argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Debug(string message, object argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Debug(IFormatProvider formatProvider, string message, sbyte argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Debug(string message, sbyte argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Debug(IFormatProvider formatProvider, string message, uint argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Debug(string message, uint argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Debug(IFormatProvider formatProvider, string message, ulong argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Debug(string message, ulong argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Info(object value)
    {
        throw new NotImplementedException();
    }

    void ILogger.Info(IFormatProvider formatProvider, object value)
    {
        throw new NotImplementedException();
    }

    void ILogger.Info(string message, object arg1, object arg2)
    {
        throw new NotImplementedException();
    }

    void ILogger.Info(string message, object arg1, object arg2, object arg3)
    {
        throw new NotImplementedException();
    }

    void ILogger.Info(IFormatProvider formatProvider, string message, bool argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Info(string message, bool argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Info(IFormatProvider formatProvider, string message, char argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Info(string message, char argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Info(IFormatProvider formatProvider, string message, byte argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Info(string message, byte argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Info(IFormatProvider formatProvider, string message, string argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Info(string message, string argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Info(IFormatProvider formatProvider, string message, int argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Info(string message, int argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Info(IFormatProvider formatProvider, string message, long argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Info(string message, long argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Info(IFormatProvider formatProvider, string message, float argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Info(string message, float argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Info(IFormatProvider formatProvider, string message, double argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Info(string message, double argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Info(IFormatProvider formatProvider, string message, decimal argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Info(string message, decimal argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Info(IFormatProvider formatProvider, string message, object argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Info(string message, object argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Info(IFormatProvider formatProvider, string message, sbyte argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Info(string message, sbyte argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Info(IFormatProvider formatProvider, string message, uint argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Info(string message, uint argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Info(IFormatProvider formatProvider, string message, ulong argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Info(string message, ulong argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Warn(object value)
    {
        throw new NotImplementedException();
    }

    void ILogger.Warn(IFormatProvider formatProvider, object value)
    {
        throw new NotImplementedException();
    }

    void ILogger.Warn(string message, object arg1, object arg2)
    {
        throw new NotImplementedException();
    }

    void ILogger.Warn(string message, object arg1, object arg2, object arg3)
    {
        throw new NotImplementedException();
    }

    void ILogger.Warn(IFormatProvider formatProvider, string message, bool argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Warn(string message, bool argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Warn(IFormatProvider formatProvider, string message, char argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Warn(string message, char argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Warn(IFormatProvider formatProvider, string message, byte argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Warn(string message, byte argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Warn(IFormatProvider formatProvider, string message, string argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Warn(string message, string argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Warn(IFormatProvider formatProvider, string message, int argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Warn(string message, int argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Warn(IFormatProvider formatProvider, string message, long argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Warn(string message, long argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Warn(IFormatProvider formatProvider, string message, float argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Warn(string message, float argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Warn(IFormatProvider formatProvider, string message, double argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Warn(string message, double argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Warn(IFormatProvider formatProvider, string message, decimal argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Warn(string message, decimal argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Warn(IFormatProvider formatProvider, string message, object argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Warn(string message, object argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Warn(IFormatProvider formatProvider, string message, sbyte argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Warn(string message, sbyte argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Warn(IFormatProvider formatProvider, string message, uint argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Warn(string message, uint argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Warn(IFormatProvider formatProvider, string message, ulong argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Warn(string message, ulong argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Error(object value)
    {
        throw new NotImplementedException();
    }

    void ILogger.Error(IFormatProvider formatProvider, object value)
    {
        throw new NotImplementedException();
    }

    void ILogger.Error(string message, object arg1, object arg2)
    {
        throw new NotImplementedException();
    }

    void ILogger.Error(string message, object arg1, object arg2, object arg3)
    {
        throw new NotImplementedException();
    }

    void ILogger.Error(IFormatProvider formatProvider, string message, bool argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Error(string message, bool argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Error(IFormatProvider formatProvider, string message, char argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Error(string message, char argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Error(IFormatProvider formatProvider, string message, byte argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Error(string message, byte argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Error(IFormatProvider formatProvider, string message, string argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Error(string message, string argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Error(IFormatProvider formatProvider, string message, int argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Error(string message, int argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Error(IFormatProvider formatProvider, string message, long argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Error(string message, long argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Error(IFormatProvider formatProvider, string message, float argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Error(string message, float argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Error(IFormatProvider formatProvider, string message, double argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Error(string message, double argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Error(IFormatProvider formatProvider, string message, decimal argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Error(string message, decimal argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Error(IFormatProvider formatProvider, string message, object argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Error(string message, object argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Error(IFormatProvider formatProvider, string message, sbyte argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Error(string message, sbyte argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Error(IFormatProvider formatProvider, string message, uint argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Error(string message, uint argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Error(IFormatProvider formatProvider, string message, ulong argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Error(string message, ulong argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Fatal(object value)
    {
        throw new NotImplementedException();
    }

    void ILogger.Fatal(IFormatProvider formatProvider, object value)
    {
        throw new NotImplementedException();
    }

    void ILogger.Fatal(string message, object arg1, object arg2)
    {
        throw new NotImplementedException();
    }

    void ILogger.Fatal(string message, object arg1, object arg2, object arg3)
    {
        throw new NotImplementedException();
    }

    void ILogger.Fatal(IFormatProvider formatProvider, string message, bool argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Fatal(string message, bool argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Fatal(IFormatProvider formatProvider, string message, char argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Fatal(string message, char argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Fatal(IFormatProvider formatProvider, string message, byte argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Fatal(string message, byte argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Fatal(IFormatProvider formatProvider, string message, string argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Fatal(string message, string argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Fatal(IFormatProvider formatProvider, string message, int argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Fatal(string message, int argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Fatal(IFormatProvider formatProvider, string message, long argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Fatal(string message, long argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Fatal(IFormatProvider formatProvider, string message, float argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Fatal(string message, float argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Fatal(IFormatProvider formatProvider, string message, double argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Fatal(string message, double argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Fatal(IFormatProvider formatProvider, string message, decimal argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Fatal(string message, decimal argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Fatal(IFormatProvider formatProvider, string message, object argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Fatal(string message, object argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Fatal(IFormatProvider formatProvider, string message, sbyte argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Fatal(string message, sbyte argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Fatal(IFormatProvider formatProvider, string message, uint argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Fatal(string message, uint argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Fatal(IFormatProvider formatProvider, string message, ulong argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Fatal(string message, ulong argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Trace<T>(T value)
    {
        throw new NotImplementedException();
    }

    void ILogger.Trace<T>(IFormatProvider formatProvider, T value)
    {
        throw new NotImplementedException();
    }

    void ILogger.Trace(LogMessageGenerator messageFunc)
    {
        throw new NotImplementedException();
    }

    void ILogger.TraceException(string message, Exception exception)
    {
        throw new NotImplementedException();
    }

    void ILogger.Trace(Exception exception, string message)
    {
        throw new NotImplementedException();
    }

    void ILogger.Trace(Exception exception, string message, params object[] args)
    {
        throw new NotImplementedException();
    }

    void ILogger.Trace(Exception exception, IFormatProvider formatProvider, string message, params object[] args)
    {
        throw new NotImplementedException();
    }

    void ILogger.Trace(IFormatProvider formatProvider, string message, params object[] args)
    {
        throw new NotImplementedException();
    }

    void ILogger.Trace(string message)
    {
        throw new NotImplementedException();
    }

    void ILogger.Trace(string message, params object[] args)
    {
        throw new NotImplementedException();
    }

    void ILogger.Trace(string message, Exception exception)
    {
        throw new NotImplementedException();
    }

    void ILogger.Trace<TArgument>(IFormatProvider formatProvider, string message, TArgument argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Trace<TArgument>(string message, TArgument argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Trace<TArgument1, TArgument2>(IFormatProvider formatProvider, string message, TArgument1 argument1, TArgument2 argument2)
    {
        throw new NotImplementedException();
    }

    void ILogger.Trace<TArgument1, TArgument2>(string message, TArgument1 argument1, TArgument2 argument2)
    {
        throw new NotImplementedException();
    }

    void ILogger.Trace<TArgument1, TArgument2, TArgument3>(IFormatProvider formatProvider, string message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3)
    {
        throw new NotImplementedException();
    }

    void ILogger.Trace<TArgument1, TArgument2, TArgument3>(string message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3)
    {
        throw new NotImplementedException();
    }

    void ILogger.Debug<T>(T value)
    {
        throw new NotImplementedException();
    }

    void ILogger.Debug<T>(IFormatProvider formatProvider, T value)
    {
        throw new NotImplementedException();
    }

    void ILogger.Debug(LogMessageGenerator messageFunc)
    {
        throw new NotImplementedException();
    }

    void ILogger.DebugException(string message, Exception exception)
    {
        throw new NotImplementedException();
    }

    void ILogger.Debug(Exception exception, string message)
    {
        throw new NotImplementedException();
    }

    void ILogger.Debug(Exception exception, string message, params object[] args)
    {
        throw new NotImplementedException();
    }

    void ILogger.Debug(Exception exception, IFormatProvider formatProvider, string message, params object[] args)
    {
        throw new NotImplementedException();
    }

    void ILogger.Debug(IFormatProvider formatProvider, string message, params object[] args)
    {
        throw new NotImplementedException();
    }

    void ILogger.Debug(string message)
    {
        throw new NotImplementedException();
    }

    void ILogger.Debug(string message, params object[] args)
    {
        throw new NotImplementedException();
    }

    void ILogger.Debug(string message, Exception exception)
    {
        throw new NotImplementedException();
    }

    void ILogger.Debug<TArgument>(IFormatProvider formatProvider, string message, TArgument argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Debug<TArgument>(string message, TArgument argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Debug<TArgument1, TArgument2>(IFormatProvider formatProvider, string message, TArgument1 argument1, TArgument2 argument2)
    {
        throw new NotImplementedException();
    }

    void ILogger.Debug<TArgument1, TArgument2>(string message, TArgument1 argument1, TArgument2 argument2)
    {
        throw new NotImplementedException();
    }

    void ILogger.Debug<TArgument1, TArgument2, TArgument3>(IFormatProvider formatProvider, string message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3)
    {
        throw new NotImplementedException();
    }

    void ILogger.Debug<TArgument1, TArgument2, TArgument3>(string message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3)
    {
        throw new NotImplementedException();
    }

    void ILogger.Info<T>(T value)
    {
        throw new NotImplementedException();
    }

    void ILogger.Info<T>(IFormatProvider formatProvider, T value)
    {
        throw new NotImplementedException();
    }

    void ILogger.Info(LogMessageGenerator messageFunc)
    {
        throw new NotImplementedException();
    }

    void ILogger.InfoException(string message, Exception exception)
    {
        throw new NotImplementedException();
    }

    void ILogger.Info(Exception exception, string message)
    {
        throw new NotImplementedException();
    }

    void ILogger.Info(Exception exception, string message, params object[] args)
    {
        throw new NotImplementedException();
    }

    void ILogger.Info(Exception exception, IFormatProvider formatProvider, string message, params object[] args)
    {
        throw new NotImplementedException();
    }

    void ILogger.Info(IFormatProvider formatProvider, string message, params object[] args)
    {
        throw new NotImplementedException();
    }

    void ILogger.Info(string message)
    {
        throw new NotImplementedException();
    }

    void ILogger.Info(string message, params object[] args)
    {
        throw new NotImplementedException();
    }

    void ILogger.Info(string message, Exception exception)
    {
        throw new NotImplementedException();
    }

    void ILogger.Info<TArgument>(IFormatProvider formatProvider, string message, TArgument argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Info<TArgument>(string message, TArgument argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Info<TArgument1, TArgument2>(IFormatProvider formatProvider, string message, TArgument1 argument1, TArgument2 argument2)
    {
        throw new NotImplementedException();
    }

    void ILogger.Info<TArgument1, TArgument2>(string message, TArgument1 argument1, TArgument2 argument2)
    {
        throw new NotImplementedException();
    }

    void ILogger.Info<TArgument1, TArgument2, TArgument3>(IFormatProvider formatProvider, string message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3)
    {
        throw new NotImplementedException();
    }

    void ILogger.Info<TArgument1, TArgument2, TArgument3>(string message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3)
    {
        throw new NotImplementedException();
    }

    void ILogger.Warn<T>(T value)
    {
        throw new NotImplementedException();
    }

    void ILogger.Warn<T>(IFormatProvider formatProvider, T value)
    {
        throw new NotImplementedException();
    }

    void ILogger.Warn(LogMessageGenerator messageFunc)
    {
        throw new NotImplementedException();
    }

    void ILogger.WarnException(string message, Exception exception)
    {
        throw new NotImplementedException();
    }

    void ILogger.Warn(Exception exception, string message)
    {
        throw new NotImplementedException();
    }

    void ILogger.Warn(Exception exception, string message, params object[] args)
    {
        throw new NotImplementedException();
    }

    void ILogger.Warn(Exception exception, IFormatProvider formatProvider, string message, params object[] args)
    {
        throw new NotImplementedException();
    }

    void ILogger.Warn(IFormatProvider formatProvider, string message, params object[] args)
    {
        throw new NotImplementedException();
    }

    void ILogger.Warn(string message)
    {
        throw new NotImplementedException();
    }

    void ILogger.Warn(string message, params object[] args)
    {
        throw new NotImplementedException();
    }

    void ILogger.Warn(string message, Exception exception)
    {
        throw new NotImplementedException();
    }

    void ILogger.Warn<TArgument>(IFormatProvider formatProvider, string message, TArgument argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Warn<TArgument>(string message, TArgument argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Warn<TArgument1, TArgument2>(IFormatProvider formatProvider, string message, TArgument1 argument1, TArgument2 argument2)
    {
        throw new NotImplementedException();
    }

    void ILogger.Warn<TArgument1, TArgument2>(string message, TArgument1 argument1, TArgument2 argument2)
    {
        throw new NotImplementedException();
    }

    void ILogger.Warn<TArgument1, TArgument2, TArgument3>(IFormatProvider formatProvider, string message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3)
    {
        throw new NotImplementedException();
    }

    void ILogger.Warn<TArgument1, TArgument2, TArgument3>(string message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3)
    {
        throw new NotImplementedException();
    }

    void ILogger.Error<T>(T value)
    {
        throw new NotImplementedException();
    }

    void ILogger.Error<T>(IFormatProvider formatProvider, T value)
    {
        throw new NotImplementedException();
    }

    void ILogger.Error(LogMessageGenerator messageFunc)
    {
        throw new NotImplementedException();
    }

    void ILogger.ErrorException(string message, Exception exception)
    {
        throw new NotImplementedException();
    }

    void ILogger.Error(Exception exception, string message)
    {
        throw new NotImplementedException();
    }

    void ILogger.Error(Exception exception, string message, params object[] args)
    {
        throw new NotImplementedException();
    }

    void ILogger.Error(Exception exception, IFormatProvider formatProvider, string message, params object[] args)
    {
        throw new NotImplementedException();
    }

    void ILogger.Error(IFormatProvider formatProvider, string message, params object[] args)
    {
        throw new NotImplementedException();
    }

    void ILogger.Error(string message)
    {
        throw new NotImplementedException();
    }

    void ILogger.Error(string message, params object[] args)
    {
        throw new NotImplementedException();
    }

    void ILogger.Error(string message, Exception exception)
    {
        throw new NotImplementedException();
    }

    void ILogger.Error<TArgument>(IFormatProvider formatProvider, string message, TArgument argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Error<TArgument>(string message, TArgument argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Error<TArgument1, TArgument2>(IFormatProvider formatProvider, string message, TArgument1 argument1, TArgument2 argument2)
    {
        throw new NotImplementedException();
    }

    void ILogger.Error<TArgument1, TArgument2>(string message, TArgument1 argument1, TArgument2 argument2)
    {
        throw new NotImplementedException();
    }

    void ILogger.Error<TArgument1, TArgument2, TArgument3>(IFormatProvider formatProvider, string message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3)
    {
        throw new NotImplementedException();
    }

    void ILogger.Error<TArgument1, TArgument2, TArgument3>(string message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3)
    {
        throw new NotImplementedException();
    }

    void ILogger.Fatal<T>(T value)
    {
        throw new NotImplementedException();
    }

    void ILogger.Fatal<T>(IFormatProvider formatProvider, T value)
    {
        throw new NotImplementedException();
    }

    void ILogger.Fatal(LogMessageGenerator messageFunc)
    {
        throw new NotImplementedException();
    }

    void ILogger.FatalException(string message, Exception exception)
    {
        throw new NotImplementedException();
    }

    void ILogger.Fatal(Exception exception, string message)
    {
        throw new NotImplementedException();
    }

    void ILogger.Fatal(Exception exception, string message, params object[] args)
    {
        throw new NotImplementedException();
    }

    void ILogger.Fatal(Exception exception, IFormatProvider formatProvider, string message, params object[] args)
    {
        throw new NotImplementedException();
    }

    void ILogger.Fatal(IFormatProvider formatProvider, string message, params object[] args)
    {
        throw new NotImplementedException();
    }

    void ILogger.Fatal(string message)
    {
        throw new NotImplementedException();
    }

    void ILogger.Fatal(string message, params object[] args)
    {
        throw new NotImplementedException();
    }

    void ILogger.Fatal(string message, Exception exception)
    {
        throw new NotImplementedException();
    }

    void ILogger.Fatal<TArgument>(IFormatProvider formatProvider, string message, TArgument argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Fatal<TArgument>(string message, TArgument argument)
    {
        throw new NotImplementedException();
    }

    void ILogger.Fatal<TArgument1, TArgument2>(IFormatProvider formatProvider, string message, TArgument1 argument1, TArgument2 argument2)
    {
        throw new NotImplementedException();
    }

    void ILogger.Fatal<TArgument1, TArgument2>(string message, TArgument1 argument1, TArgument2 argument2)
    {
        throw new NotImplementedException();
    }

    void ILogger.Fatal<TArgument1, TArgument2, TArgument3>(IFormatProvider formatProvider, string message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3)
    {
        throw new NotImplementedException();
    }

    void ILogger.Fatal<TArgument1, TArgument2, TArgument3>(string message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3)
    {
        throw new NotImplementedException();
    }

    void ILoggerBase.Log(LogLevel level, object value)
    {
        throw new NotImplementedException();
    }

    void ILoggerBase.Log(LogLevel level, IFormatProvider formatProvider, object value)
    {
        throw new NotImplementedException();
    }

    void ILoggerBase.Log(LogLevel level, string message, object arg1, object arg2)
    {
        throw new NotImplementedException();
    }

    void ILoggerBase.Log(LogLevel level, string message, object arg1, object arg2, object arg3)
    {
        throw new NotImplementedException();
    }

    void ILoggerBase.Log(LogLevel level, IFormatProvider formatProvider, string message, bool argument)
    {
        throw new NotImplementedException();
    }

    void ILoggerBase.Log(LogLevel level, string message, bool argument)
    {
        throw new NotImplementedException();
    }

    void ILoggerBase.Log(LogLevel level, IFormatProvider formatProvider, string message, char argument)
    {
        throw new NotImplementedException();
    }

    void ILoggerBase.Log(LogLevel level, string message, char argument)
    {
        throw new NotImplementedException();
    }

    void ILoggerBase.Log(LogLevel level, IFormatProvider formatProvider, string message, byte argument)
    {
        throw new NotImplementedException();
    }

    void ILoggerBase.Log(LogLevel level, string message, byte argument)
    {
        throw new NotImplementedException();
    }

    void ILoggerBase.Log(LogLevel level, IFormatProvider formatProvider, string message, string argument)
    {
        throw new NotImplementedException();
    }

    void ILoggerBase.Log(LogLevel level, string message, string argument)
    {
        throw new NotImplementedException();
    }

    void ILoggerBase.Log(LogLevel level, IFormatProvider formatProvider, string message, int argument)
    {
        throw new NotImplementedException();
    }

    void ILoggerBase.Log(LogLevel level, string message, int argument)
    {
        throw new NotImplementedException();
    }

    void ILoggerBase.Log(LogLevel level, IFormatProvider formatProvider, string message, long argument)
    {
        throw new NotImplementedException();
    }

    void ILoggerBase.Log(LogLevel level, string message, long argument)
    {
        throw new NotImplementedException();
    }

    void ILoggerBase.Log(LogLevel level, IFormatProvider formatProvider, string message, float argument)
    {
        throw new NotImplementedException();
    }

    void ILoggerBase.Log(LogLevel level, string message, float argument)
    {
        throw new NotImplementedException();
    }

    void ILoggerBase.Log(LogLevel level, IFormatProvider formatProvider, string message, double argument)
    {
        throw new NotImplementedException();
    }

    void ILoggerBase.Log(LogLevel level, string message, double argument)
    {
        throw new NotImplementedException();
    }

    void ILoggerBase.Log(LogLevel level, IFormatProvider formatProvider, string message, decimal argument)
    {
        throw new NotImplementedException();
    }

    void ILoggerBase.Log(LogLevel level, string message, decimal argument)
    {
        throw new NotImplementedException();
    }

    void ILoggerBase.Log(LogLevel level, IFormatProvider formatProvider, string message, object argument)
    {
        throw new NotImplementedException();
    }

    void ILoggerBase.Log(LogLevel level, string message, object argument)
    {
        throw new NotImplementedException();
    }

    void ILoggerBase.Log(LogLevel level, IFormatProvider formatProvider, string message, sbyte argument)
    {
        throw new NotImplementedException();
    }

    void ILoggerBase.Log(LogLevel level, string message, sbyte argument)
    {
        throw new NotImplementedException();
    }

    void ILoggerBase.Log(LogLevel level, IFormatProvider formatProvider, string message, uint argument)
    {
        throw new NotImplementedException();
    }

    void ILoggerBase.Log(LogLevel level, string message, uint argument)
    {
        throw new NotImplementedException();
    }

    void ILoggerBase.Log(LogLevel level, IFormatProvider formatProvider, string message, ulong argument)
    {
        throw new NotImplementedException();
    }

    void ILoggerBase.Log(LogLevel level, string message, ulong argument)
    {
        throw new NotImplementedException();
    }

    bool ILoggerBase.IsEnabled(LogLevel level)
    {
        throw new NotImplementedException();
    }

    void ILoggerBase.Log(LogEventInfo logEvent)
    {
        throw new NotImplementedException();
    }

    void ILoggerBase.Log(Type wrapperType, LogEventInfo logEvent)
    {
        throw new NotImplementedException();
    }

    void ILoggerBase.Log<T>(LogLevel level, T value)
    {
        throw new NotImplementedException();
    }

    void ILoggerBase.Log<T>(LogLevel level, IFormatProvider formatProvider, T value)
    {
        throw new NotImplementedException();
    }

    void ILoggerBase.Log(LogLevel level, LogMessageGenerator messageFunc)
    {
        throw new NotImplementedException();
    }

    void ILoggerBase.LogException(LogLevel level, string message, Exception exception)
    {
        throw new NotImplementedException();
    }

    void ILoggerBase.Log(LogLevel level, Exception exception, string message, params object[] args)
    {
        throw new NotImplementedException();
    }

    void ILoggerBase.Log(LogLevel level, Exception exception, IFormatProvider formatProvider, string message, params object[] args)
    {
        throw new NotImplementedException();
    }

    void ILoggerBase.Log(LogLevel level, IFormatProvider formatProvider, string message, params object[] args)
    {
        throw new NotImplementedException();
    }

    void ILoggerBase.Log(LogLevel level, string message)
    {
        throw new NotImplementedException();
    }

    void ILoggerBase.Log(LogLevel level, string message, params object[] args)
    {
        throw new NotImplementedException();
    }

    void ILoggerBase.Log(LogLevel level, string message, Exception exception)
    {
        throw new NotImplementedException();
    }

    void ILoggerBase.Log<TArgument>(LogLevel level, IFormatProvider formatProvider, string message, TArgument argument)
    {
        throw new NotImplementedException();
    }

    void ILoggerBase.Log<TArgument>(LogLevel level, string message, TArgument argument)
    {
        throw new NotImplementedException();
    }

    void ILoggerBase.Log<TArgument1, TArgument2>(LogLevel level, IFormatProvider formatProvider, string message, TArgument1 argument1, TArgument2 argument2)
    {
        throw new NotImplementedException();
    }

    void ILoggerBase.Log<TArgument1, TArgument2>(LogLevel level, string message, TArgument1 argument1, TArgument2 argument2)
    {
        throw new NotImplementedException();
    }

    void ILoggerBase.Log<TArgument1, TArgument2, TArgument3>(LogLevel level, IFormatProvider formatProvider, string message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3)
    {
        throw new NotImplementedException();
    }

    void ILoggerBase.Log<TArgument1, TArgument2, TArgument3>(LogLevel level, string message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3)
    {
        throw new NotImplementedException();
    }

    void ISuppress.Swallow(Action action)
    {
        throw new NotImplementedException();
    }

    T ISuppress.Swallow<T>(Func<T> func)
    {
        throw new NotImplementedException();
    }

    T ISuppress.Swallow<T>(Func<T> func, T fallback)
    {
        throw new NotImplementedException();
    }

    void ISuppress.Swallow(Task task)
    {
        throw new NotImplementedException();
    }

    Task ISuppress.SwallowAsync(Task task)
    {
        throw new NotImplementedException();
    }

    Task ISuppress.SwallowAsync(Func<Task> asyncAction)
    {
        throw new NotImplementedException();
    }

    Task<TResult> ISuppress.SwallowAsync<TResult>(Func<Task<TResult>> asyncFunc)
    {
        throw new NotImplementedException();
    }

    Task<TResult> ISuppress.SwallowAsync<TResult>(Func<Task<TResult>> asyncFunc, TResult fallback)
    {
        throw new NotImplementedException();
    }
}