﻿using System.Collections;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using UnityEngine;
using ZLogger;

public static class LogManager
{
    static Microsoft.Extensions.Logging.ILogger globalLogger;
    static ILoggerFactory loggerFactory;

    // Setup on first called GetLogger<T>.
    static LogManager()
    {
        // Standard LoggerFactory does not work on IL2CPP,
        // But you can use ZLogger's UnityLoggerFactory instead,
        // it works on IL2CPP, all platforms(includes mobile).
        loggerFactory = UnityLoggerFactory.Create(builder =>
        {
            // or more configuration, you can use builder.AddFilter
            builder.SetMinimumLevel(LogLevel.Trace);

            // AddZLoggerUnityDebug is only available for Unity, it send log to UnityEngine.Debug.Log.
            // LogLevels are translate to
            // * Trace/Debug/Information -> LogType.Log
            // * Warning/Critical -> LogType.Warning
            // * Error without Exception -> LogType.Error
            // * Error with Exception -> LogException
            builder.AddZLoggerUnityDebug();

            // and other configuration(AddFileLog, etc...)
            builder.AddZLoggerFile("applog.log");
        });

        globalLogger = loggerFactory.CreateLogger("Global");

        Application.quitting += () =>
        {
            // when quit, flush unfinished log entries.
            loggerFactory.Dispose();
        };
    }

    public static Microsoft.Extensions.Logging.ILogger Logger => globalLogger;

    public static ILogger<T> GetLogger<T>() where T : class => loggerFactory.CreateLogger<T>();
    public static Microsoft.Extensions.Logging.ILogger GetLogger(string categoryName) => loggerFactory.CreateLogger(categoryName);
}