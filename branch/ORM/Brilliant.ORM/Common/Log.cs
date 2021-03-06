﻿using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace Brilliant.ORM
{
    /// <summary>
    /// 日志
    /// </summary>
    public class Log
    {
        private string _logFilePath;
        private static readonly Log _instance = new Log();

        private static bool enable = false;

        /// <summary>
        /// 是否启用
        /// </summary>
        public static bool Enable
        {
            get { return enable; }
            set { enable = value; }
        }

        /// <summary>
        /// 单例
        /// </summary>
        public static Log Instance
        {
            get { return Log._instance; }
        }

        /// <summary>
        /// 构造器
        /// </summary>
        private Log()
        {
            this._logFilePath = AppDomain.CurrentDomain.BaseDirectory + "\\Log.json";
        }

        /// <summary>
        /// 添加日志信息
        /// </summary>
        /// <param name="message">日志信息</param>
        public void Add(string message)
        {
            Add(message, null);
        }

        /// <summary>
        /// 添加日志信息
        /// </summary>
        /// <param name="message">日志信息</param>
        /// <param name="sql">SQL对象</param>
        public void Add(string message, SQL sql)
        {
            Add(LogLevel.Exception, LogType.Common, message, sql);
        }

        /// <summary>
        /// 添加日志信息
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="message">信息</param>
        public void Add(LogType type, string message)
        {
            Add(LogLevel.Exception, type, message, null);
        }

        /// <summary>
        /// 添加日志信息
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="message">日志信息</param>
        /// <param name="sql">SQL对象</param>
        public void Add(LogType type, string message, SQL sql)
        {
            Add(LogLevel.Exception, type, message, sql);
        }

        /// <summary>
        /// 添加日志信息
        /// </summary>
        /// <param name="level">级别</param>
        /// <param name="type">类型</param>
        /// <param name="message">信息</param>
        public void Add(LogLevel level, LogType type, string message)
        {
            Add(level, type, message, null);
        }

        /// <summary>
        /// 添加日志信息
        /// </summary>
        /// <param name="level">级别</param>
        /// <param name="type">类型</param>
        /// <param name="message">信息</param>
        /// <param name="sql">SQL对象</param>
        public void Add(LogLevel level, LogType type, string message, SQL sql)
        {
            if (Enable)
            {
                LogInfo log = new LogInfo();
                log.Date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                log.LogLevel = Enum<LogLevel>.GetDesc(level);
                log.LogType = Enum<LogType>.GetDesc(type);
                log.Message = message;
                if (sql != null)
                {
                    log.CmdText = sql.CmdText;
                    if (sql.Parameters != null)
                    {
                        foreach (IDbDataParameter param in sql.Parameters)
                        {
                            LogCmdParam logParam = new LogCmdParam();
                            logParam.DbType = param.DbType.ToString();
                            logParam.ParameterName = param.ParameterName;
                            logParam.Size = param.Size.ToString();
                            logParam.Value = param.Value.ToString();
                            log.Parameters.Add(logParam);
                        }
                    }
                }
                WriteLogAsync(log);
            }
        }

        /// <summary>
        /// 异步写入日志文件
        /// </summary>
        /// <param name="log">Log对象</param>
        private void WriteLogAsync(LogInfo log)
        {
            string logContent = JsonSerializer.JSSerialize(log);
            using (FileStream fs = new FileStream(_logFilePath, FileMode.Append, FileAccess.Write, FileShare.Write, 1024, FileOptions.Asynchronous))
            {
                logContent = fs.Length == 0 ? logContent : "," + logContent;
                byte[] buffer = Encoding.UTF8.GetBytes(logContent);
                IAsyncResult writeResult = fs.BeginWrite(buffer, 0, buffer.Length, (asyncResult) =>
                {
                    FileStream stream = (FileStream)asyncResult.AsyncState;
                    stream.EndWrite(asyncResult);
                },
                    fs);
                fs.Flush();
            }
        }

        /// <summary>
        /// 获取日志类型列表
        /// </summary>
        /// <returns>日志类型列表</returns>
        public static List<EnumItem> GetLogTypes()
        {
            return Enum<LogType>.ToList();
        }

        /// <summary>
        /// 获取日志级别列表
        /// </summary>
        /// <returns>日志级别列表</returns>
        public static List<EnumItem> GetLogLevels()
        {
            return Enum<LogLevel>.ToList();
        }
    }

    /// <summary>
    /// 日志信息
    /// </summary>
    public class LogInfo
    {
        /// <summary>
        /// 构造器
        /// </summary>
        public LogInfo()
        {
            this.Parameters = new List<LogCmdParam>();
        }

        /// <summary>
        /// 日期
        /// </summary>
        public string Date { get; set; }

        /// <summary>
        /// 级别
        /// </summary>
        public string LogLevel { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public string LogType { get; set; }

        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 查询指令
        /// </summary>
        public string CmdText { get; set; }

        /// <summary>
        /// 查询参数列表
        /// </summary>
        public List<LogCmdParam> Parameters { get; set; }

    }

    /// <summary>
    /// SQL日志参数
    /// </summary>
    public class LogCmdParam
    {
        /// <summary>
        /// 参数名称
        /// </summary>
        public string ParameterName { get; set; }

        /// <summary>
        /// 大小
        /// </summary>
        public string Size { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public string DbType { get; set; }
    }

    /// <summary>
    /// 日志信息等级
    /// </summary>
    public enum LogLevel
    {
        /// <summary>
        /// 全部信息（用于筛选）
        /// </summary>
        [Description("全部")]
        All,
        /// <summary>
        /// 正常信息
        /// </summary>
        [Description("正常")]
        Normal,
        /// <summary>
        /// 异常信息
        /// </summary>
        [Description("异常")]
        Exception,
        /// <summary>
        /// 警告信息
        /// </summary>
        [Description("警告")]
        Warning,
        /// <summary>
        /// 错误信息
        /// </summary>
        [Description("错误")]
        Error
    }

    /// <summary>
    /// 日志类型
    /// </summary>
    public enum LogType
    {
        /// <summary>
        /// 显示全部（用于筛选）
        /// </summary>
        [Description("显示全部")]
        All,
        /// <summary>
        /// 一般类型
        /// </summary>
        [Description("一般类型")]
        Common,
        /// <summary>
        /// SQL解析
        /// </summary>
        [Description("SQL解析")]
        Parse,
        /// <summary>
        /// SQL执行
        /// </summary>
        [Description("SQL执行")]
        Execute,
        /// <summary>
        /// 对象映射
        /// </summary>
        [Description("对象映射")]
        Map
    }
}
