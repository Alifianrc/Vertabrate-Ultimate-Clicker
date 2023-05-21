using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

using UnityEngine;

namespace NoSuchStudio.Common {
    public class LoggerConfig {
        public string className;
        public bool logClassName;
        public bool logGameObjectName;
        public bool logThreadId;
        public bool logGameTime;

        public LoggerConfig(string className, bool logClassName = true, bool logGameObjectName = true, bool logThreadId = true, bool logGameTime = true) {
            this.className = className;
            this.logClassName = logClassName;
            this.logGameObjectName = logGameObjectName;
            this.logThreadId = logThreadId;
            this.logGameTime = logGameTime;
        }
    }

    /// <summary>
    /// Utility class for UnityEngine.Object subclasses (MonoBehaviour, Component, Editor, etc.) that want to use the extended logging capabilities below:
    /// <ul>
    /// <li>Option to log ThreadId, class name, object name, game time or other common info to log messages.</li>
    /// <li>Configure the info PER CLASS. Useful for debugging specific classes.</li>
    /// </ul>
    /// </summary>
    /// <remarks>
    /// This class keeps track of all types that use it and creates a <see cref="UnityEngine.Logger"/> for each. 
    /// Any messages logged through the extension methods will have the info based on the LoggerConfig for that type prepended to the message.
    /// <code>
    /// MyClass myObj = new MyClass(); // MyClass extends UnityEngine.Object (i.e. MonoBehaviour, Editor, Component, ...)
    /// myObj.LogLog("Hello World!"); 
    /// // will print "[1][4.56](MyClass)(myObjName) Hello World!"
    /// </code>
    /// Using sample code like below, you can filter your logs by class.
    /// <code>
    /// UnityObjectLoggerExt.GetLoggerByType&lt;MyClass&gt;().logger.filterLogType = LogType.Error;
    /// </code>
    /// Using sample code like below, you can change the logging config for each class.
    /// <code>
    /// UnityObjectLoggerExt.GetLoggerByType&lt;MyClass&gt;().loggerConfig.logGameTime = false;
    /// </code>
    /// </remarks>
    public static class UnityObjectLoggerExt {
        
        public static readonly Dictionary<Type, (Logger, LoggerConfig)> loggers = new();
        
        [HideInCallstack]
        private static void Log(UnityEngine.Object unityObj, LogType logType, string format, params object[] args) {
            (Logger logger, LoggerConfig lc) = GetLoggerByType(unityObj.GetType());
            logger.LogFormat(logType, unityObj,
                $"{(lc.logThreadId ? "[" + Thread.CurrentThread.ManagedThreadId + "] " : "")}" +
                $"{(lc.logGameTime ? "[" + Time.time + "]" : "")}" +
                $"{(lc.logClassName ? "(" + lc.className + ")" : "")}" +
                $"{(lc.logGameObjectName ? "(" + unityObj.name + ") " : "")}" +
                $"{format}", args);
        }
        
        [HideInCallstack]
        public static void Log(this UnityEngine.Object mono, string format, params object[] args) {
            Log(mono, LogType.Log, format, args);
        }
        
        [HideInCallstack]
        public static void LogWarn(this UnityEngine.Object mono, string format, params object[] args) {
            Log(mono, LogType.Warning, format, args);
        }

        [HideInCallstack]
        public static void LogError(this UnityEngine.Object mono, string format, params object[] args) {
            Log(mono, LogType.Error, format, args);
        }
      
        [HideInCallstack]
        private static void AddType(Type type) {
            if (!loggers.ContainsKey(type)) {
                LoggerConfig lc = new LoggerConfig($"{type.Name}");
                loggers.Add(type, (new Logger(Debug.unityLogger.logHandler), lc));
            }
        }

        public static (Logger logger, LoggerConfig loggerConfig) GetLoggerByType(Type type) {
            AddType(type);
            return loggers[type];
        }

        public static (Logger logger, LoggerConfig loggerConfig) GetLoggerByType<T>() {
            return GetLoggerByType(typeof(T));
        }

        [HideInCallstack]
        private static void LogStatic<T>(LogType logType, string format, params object[] args) {
            (Logger logger, LoggerConfig lc) = GetLoggerByType<T>();
            logger.LogFormat(logType,
                $"{(lc.logThreadId ? "[" + Thread.CurrentThread.ManagedThreadId + "] " : "")}" +
                $"{(lc.logGameTime ? "[" + $"{Time.time:0.00}" + "]" : "")}" +
                $"{(lc.logClassName ? "(" + lc.className + ")" : "")}" +
                $"{(lc.logGameObjectName ? "(<static>) " : "")}" +
                $"{format}", args);
        }

        [HideInCallstack]
        public static void Log<T>(string format, params object[] args) {
            LogStatic<T>(LogType.Log, format, args);
        }

        [HideInCallstack]
        public static void LogWarn<T>(string format, params object[] args) {
            LogStatic<T>(LogType.Warning, format, args);
        }

        [HideInCallstack]
        public static void LogError<T>(string format, params object[] args) {
            LogStatic<T>(LogType.Error, format, args);
        }
    }
}