using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;

namespace TrueDotBin.CosmosUtils.Logging
{
    /// <summary>
    /// Writes leveled log messages to the console.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Output is colored by default, set <see cref="ColoredOutput"/> to false to disable output coloring.
    /// </para>
    /// </remarks>
    public sealed class Logger(string? source = null)
    {
        /// <summary>
        /// The process-wide logger instance. Use this instead of creating a
        /// <see cref="Logger"/> unless you need independent configuration.
        /// </summary>
        public static readonly Logger Global = new();

        /// <summary>
        /// Where each log comes from.
        /// </summary>
        public string Source { get; } = source ?? Defaults.DefaultLoggerSource;

        /// <summary>
        /// Whether to color the output based on the log level.
        /// </summary>
        /// <remarks>
        /// Each log level has its own color:
        /// <list type="bullet">
        ///     <item><see cref="LogLevel.Fatal"/> - red</item>
        ///     <item><see cref="LogLevel.Error"/> - red</item>
        ///     <item><see cref="LogLevel.Warning"/> - yellow</item>
        ///     <item><see cref="LogLevel.Info"/> - cyan</item>
        ///     <item><see cref="LogLevel.Debug"/> - white</item>
        /// </list>
        /// 
        /// These can be changed using <see cref="LogLevelColors"/>
        /// </remarks>
        public bool ColoredOutput { get; set; } = true;

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
        /// The color of each log level.
        /// </summary>
        public Dictionary<LogLevel, ConsoleColor> LogLevelColors { get; set; } = new()
        {
            { LogLevel.Fatal, ConsoleColor.Red },
            { LogLevel.Error, ConsoleColor.Red },
            { LogLevel.Warning, ConsoleColor.Yellow },
            { LogLevel.Info, ConsoleColor.Cyan },
            { LogLevel.Debug, ConsoleColor.White },
        };

        /// <summary>
        /// The minimum log level that will be shown on the console.
        /// </summary>
        public LogLevel MinimumLevel { get; set; } = LogLevel.Info;

        /// <summary>
        /// The minimum log level that will be written to <see cref="LogFilePath"/> if <see cref="WriteToFile"/> is true.
        /// </summary>
        public LogLevel MinimumFileLevel { get; set; } = LogLevel.Info;

        /// <summary>
        /// Whether to also write logs to a file.
        /// </summary>
        public bool WriteToFile { get; set; }

        /// <summary>
        /// The path of the log file.
        /// </summary>
        public string? LogFilePath { get; set; }
    }
}
