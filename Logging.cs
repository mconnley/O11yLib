using System.Text;
using NLog;

namespace O11yLib;
public class MyLogger
{
    private readonly Logger _logger;
    private readonly NLog.Layouts.JsonLayout _jsonLayout;
    public MyLogger()
    {
        var timestampLayout = NLog.Layouts.Layout.FromString("${date:format=o}");
        var levelLayout = NLog.Layouts.Layout.FromString("${level}");
        var loggerLayout = NLog.Layouts.Layout.FromString("${logger}");
        var callsiteLayout = NLog.Layouts.Layout.FromString("${callsite:className=True:fileName=True:includeSourcePath=True:methodName=True}");
        var bodyLayout = NLog.Layouts.Layout.FromString("${message:raw=true");
        var attributesLayout = NLog.Layouts.Layout.FromString("${event-properties:item=attributes:format=@}");

        _jsonLayout = new NLog.Layouts.JsonLayout()
        {
            Attributes = {
                new NLog.Layouts.JsonAttribute("timestamp", timestampLayout),
                new NLog.Layouts.JsonAttribute("level", levelLayout),
                new NLog.Layouts.JsonAttribute("logger", loggerLayout),
                new NLog.Layouts.JsonAttribute("instrumentationscope", callsiteLayout),
                new NLog.Layouts.JsonAttribute("body", bodyLayout),
                new NLog.Layouts.JsonAttribute("attributes", attributesLayout, false)
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

        _logger = NLog.LogManager.GetLogger("MyLogger");
    }

    public void Info(string Message, object attributes)
    {
        LogEventInfo ei = new (LogLevel.Info, _logger.Name, Message);
        ei.Properties.Add("attributes", attributes);
        _logger.Log(typeof(MyLogger), ei);
    }
}