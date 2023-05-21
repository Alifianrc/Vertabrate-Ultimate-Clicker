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
    public abstract class MonoBehaviourWithLogger : MonoBehaviour {
        
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
        
        protected void Log(string format, params object[] args) {
            UnityObjectLoggerExt.Log(this, format, args);
        }
        
        protected void LogWarn(string format, params object[] args) {
            UnityObjectLoggerExt.LogWarn(this, format, args);
        }
        
        protected void LogError(string format, params object[] args) {
            UnityObjectLoggerExt.LogError(this, format, args);
        }

        protected static void Log<T>(string format, params object[] args) {
            UnityObjectLoggerExt.Log<T>(format, args);
        }

        protected static void LogWarn<T>(string format, params object[] args) {
            UnityObjectLoggerExt.LogWarn<T>(format, args);
        }

        protected static void LogError<T>(string format, params object[] args) {
            UnityObjectLoggerExt.LogError<T>(format, args);
        }

        protected void Log(object message)
        {
            UnityObjectLoggerExt.Log(this, message.ToString());
        }

        protected void LogWarn(object message)
        {
            UnityObjectLoggerExt.LogWarn(this, message.ToString());
        }

        protected void LogError(object message)
        {
            UnityObjectLoggerExt.LogError(this, message.ToString());
        }

        protected static void Log<T>(object message)
        {
            UnityObjectLoggerExt.Log<T>(message.ToString());
        }

        protected static void LogWarn<T>(object message)
        {
            UnityObjectLoggerExt.LogWarn<T>(message.ToString());
        }

        protected static void LogError<T>(object message)
        {
            UnityObjectLoggerExt.LogError<T>(message.ToString());
        }
    }
}