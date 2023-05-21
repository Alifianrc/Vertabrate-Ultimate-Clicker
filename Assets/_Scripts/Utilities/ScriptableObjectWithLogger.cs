using System;

using UnityEngine;

namespace NoSuchStudio.Common {
    /// <summary>
    /// Base class for ScriptableObjects that want to use the extended logging capabilities of <see cref="UnityObjectLoggerExt"/>.
    /// By subclassing, you can use the methods without qualifying with 'this.', saving yourself from typing 5 characters!
    /// <code>LogWarn("My warning: {0}", warningMsg);</code>
    /// instead of
    /// <code>this.LogWarn("My warning: {0}", warningMsg);</code>
    /// </summary>
    public abstract class ScriptableObjectWithLogger : ScriptableObject {
        
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
        protected void Log(string format, params object[] args) {
            UnityObjectLoggerExt.Log(this, format, args);
        }
        
        [HideInCallstack]
        protected void LogWarn(string format, params object[] args) {
            UnityObjectLoggerExt.LogWarn(this, format, args);
        }
        
        [HideInCallstack]
        protected void LogError(string format, params object[] args) {
            UnityObjectLoggerExt.LogError(this, format, args);
        }

        [HideInCallstack]
        protected static void Log<T>(string format, params object[] args) {
            UnityObjectLoggerExt.Log<T>(format, args);
        }

        [HideInCallstack]
        protected static void LogWarn<T>(string format, params object[] args) {
            UnityObjectLoggerExt.LogWarn<T>(format, args);
        }

        [HideInCallstack]
        protected static void LogError<T>(string format, params object[] args) {
            UnityObjectLoggerExt.LogError<T>(format, args);
        }

        [HideInCallstack]
        protected void Log(object message)
        {
            UnityObjectLoggerExt.Log(this, message.ToString());
        }

        [HideInCallstack]
        protected void LogWarn(object message)
        {
            UnityObjectLoggerExt.LogWarn(this, message.ToString());
        }

        [HideInCallstack]
        protected void LogError(object message)
        {
            UnityObjectLoggerExt.LogError(this, message.ToString());
        }

        [HideInCallstack]
        protected void Log(string message)
        {
            UnityObjectLoggerExt.Log(this, message);
        }

        [HideInCallstack]
        protected void LogWarn(string message)
        {
            UnityObjectLoggerExt.LogWarn(this, message);
        }

        [HideInCallstack]
        protected void LogError(string message)
        {
            UnityObjectLoggerExt.LogError(this, message);
        }
    }
}