using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;

namespace TrueDotBin.CosmosUtils.Logging
{
    /// <summary>
    /// Writes leveled log levels to the console.
    /// </summary>
    /// <remarks>
    /// <para>
    /// By default, the log format is "[{LEVEL}] {MESSAGE}"; this can be changed with <see cref="LogFormat"/>
    /// </para>
    /// </remarks>
    public class Logger(string source = "Logger")
    {
        /// <summary>
        /// The process-wide logger instance. Use this instead of creating a
        /// <see cref="Logger"/> unless you need independent configuration.
        /// </summary>
        public static readonly Logger Global = new();

        /// <summary>
        /// Where each log comes from.
        /// </summary>
        public string Source { get; } = source;

        /// <summary>
        /// The format string to format the time portion of a log entry.
        /// </summary>
        /// <remarks>
        /// The format string follows the same format as <see cref="DateTime.ToString(string?)"/>
        /// </remarks>
        public string TimeFormat { get; set; } = "HH:mm:ss";

        /// <summary>
        /// The format string to format the date portion of a log entry.
        /// </summary>
        /// <remarks>
        /// The format string follows the same format as <see cref="DateTime.ToString(string?)"/>
        /// </remarks>
        public string DateFormat { get; set; } = "dd/MM/yyyy";

        /// <summary>
        /// The format of each log entry.
        /// </summary>
        /// <remarks>
        /// <para>
        /// The following tokens are replaced in each entry:
        /// <list type="bullet">
        ///     <item><c>{SOURCE}</c> - where this log entry came from, e.g., "Shell"</item>
        ///     <item><c>{LEVEL}</c> - the log level</item>
        ///     <item><c>{MESSAGE}</c> - the message you want to log</item>
        ///     <item><c>{DATE}</c> - the current date, formatted with <see cref="DateFormat"/></item>
        ///     <item><c>{TIME}</c> - the current time, formatted with <see cref="TimeFormat"/></item>
        /// </list></para>
        /// 
        /// <para>
        /// The log level is three characters; there is no way to change that.
        /// </para>
        /// </remarks>
        public string LogFormat { get; set; } = "[{LEVEL}] {MESSAGE}";

        private string CreateLogMessage(string level, string message)
        {
            var now = DateTime.Now;
            var time = now.ToString(TimeFormat);
            var date = now.ToString(DateFormat);

            return LogFormat
                .Replace("{SOURCE}", Source)
                .Replace("{LEVEL}", level)
                .Replace("{MESSAGE}", message)
                .Replace("{DATE}", date)
                .Replace("{TIME}", time);
        }

        /// <summary>
        /// Logs an informational message.
        /// </summary>
        /// <param name="message">The message to log.</param>
        public void Info(string message)
        {
            var msg = CreateLogMessage("INF", message);
            Console.WriteLine(msg);
        }

        /// <summary>
        /// Logs a message that indicates an error with an operation.
        /// </summary>
        /// <param name="message">The message to log.</param>
        public void Error(string message)
        {
            var msg = CreateLogMessage("ERR", message);
            Console.WriteLine(msg);
        }

        /// <summary>
        /// Logs a message that indicates a fatal failure.
        /// </summary>
        /// <param name="message">The message to log.</param>
        public void Fatal(string message)
        {
            var msg = CreateLogMessage("FTL", message);
            Console.WriteLine(msg);
        }

        /// <summary>
        /// Logs a message that indicates a warning.
        /// </summary>
        /// <param name="message">The message to log.</param>
        public void Warn(string message)
        {
            var msg = CreateLogMessage("WRN", message);
            Console.WriteLine(msg);
        }
    }
}
