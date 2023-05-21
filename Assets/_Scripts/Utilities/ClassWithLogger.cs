using System;

using UnityEngine;

namespace NoSuchStudio.Common {
    /// <summary>
    /// Base class for MonoBehaviours that want to use the extended logging capabilities of <see cref="UnityObjectLoggerExt"/>.
    /// By subclassing, you can use the methods without qualifying with 'this.', saving yourself from typing 5 characters!
    /// <code>LogWarn("My warning: {0}", warningMsg);</code>
    /// instead of
    /// <code>this.LogWarn("My warning: {0}", warningMsg);</code>
    /// </summary>
    public abstract class ClassWithLogger
    {
        public const string name = "ClassWithLogger";
        
        public Logger logger {
            get {
                Type thisType = GetType();
                return UnityObjectLoggerExt.GetLoggerByType(thisType).logger;
            }
        }
        public LoggerConfig loggerConfig {
            get {
                Type thisType = GetType();
                return UnityObjectLoggerExt.GetLoggerByType(thisType).loggerConfig;
            }
        }

        [HideInCallstack]
        protected static void Log(string format, params object[] args) {
            UnityObjectLoggerExt.Log<ClassWithLogger>(format, args);
        }

        [HideInCallstack]
        protected static void LogWarn(string format, params object[] args) {
            UnityObjectLoggerExt.LogWarn<ClassWithLogger>(format, args);
        }

        [HideInCallstack]
        protected static void LogError(string format, params object[] args) {
            UnityObjectLoggerExt.LogError<ClassWithLogger>(format, args);
        }
    }
}