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
    /// 
    /// <para>
    /// Logs are not written to a file, set <see cref="WriteToFile"/> to true and set a <see cref="LogFilePath"/> to enable file logging.
    /// </para>
    /// 
    /// <para>
    /// The minimum log level is <see cref="LogLevel.Info"/>, this can be changed by
    /// setting <see cref="MinimumLevel"/> (output) or <see cref="MinimumFileLevel"/> (file logging)
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
        /// The string representation of each log level.
        /// </summary>
        public Dictionary<LogLevel, string> LogLevelStrings { get; set; } = new()
        {
            { LogLevel.Fatal, "FTL" },
            { LogLevel.Error, "ERR" },
            { LogLevel.Warning, "WRN" },
            { LogLevel.Info, "INF" },
            { LogLevel.Debug, "DEB" },
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

        /// <summary>
        /// The format options for logging to standard output.
        /// </summary>
        public LoggerFormatOptions OutputFormatOptions { get; } = new();

        /// <summary>
        /// The format options for logging to a file.
        /// </summary>
        public LoggerFormatOptions FileFormatOptions { get; } = new()
        {
            ShowDate = true,
            ShowTime = true
        };

        private List<LogFragment> BuildFragments(LoggerFormatOptions options, LogLevel level, string message)
        {
            var fragments = new List<LogFragment>();
            var dt = DateTime.Now;
            var formattedDate = dt.ToString(DateFormat);
            var formattedTime = dt.ToString(TimeFormat);

            if (options.ShowDate)
            {
                var frag = new LogFragment($"{formattedDate} ", null);
                fragments.Add(frag);
            }

            if (options.ShowTime)
            {
                var frag = new LogFragment($"{formattedTime} ", null);
                fragments.Add(frag);
            }

            if (options.ShowLevel)
            {
                var color = LogLevelColors[level];
                var logLevelString = LogLevelStrings[level];

                var frag = new LogFragment($"[ {logLevelString} ] ", color);
                fragments.Add(frag);
            }

            if (options.ShowSource && !string.IsNullOrEmpty(source))
            {
                var frag = new LogFragment($"({source}) ", null);
                fragments.Add(frag);
            }

            fragments.Add(new(message, null));
            return fragments;
        }

        /// <summary>
        /// Logs a message to the logger.
        /// </summary>
        /// <param name="level">The severity of the log..</param>
        /// <param name="message">The message that should be logged.</param>
        public void Log(LogLevel level, string message)
        {
            var outputFragments = BuildFragments(OutputFormatOptions, level, message);
            var fileFragments = BuildFragments(FileFormatOptions, level, message);
            var rawFileFragments = fileFragments.Select(f => f.Text);
            var logFileLine = string.Concat(rawFileFragments);

            if (WriteToFile && File.Exists(LogFilePath))
            {
                if (level < MinimumFileLevel)
                    return;

                File.AppendAllText(LogFilePath, logFileLine + "\n");
            }

            if (level < MinimumLevel)
                return;

            if (!ColoredOutput || Console.IsOutputRedirected)
            {
                var rawOutputFragments = outputFragments.Select(f => f.Text);
                var logOutputLine = string.Concat(rawOutputFragments);

                Console.WriteLine(logOutputLine);
                return;
            }

            var startFg = Console.ForegroundColor;

            try
            {
                foreach (var fragment in outputFragments)
                {
                    if (fragment.Color is { } color)
                        Console.ForegroundColor = color;
                    else
                        Console.ForegroundColor = startFg;

                    Console.Write(fragment.Text);
                }

                Console.WriteLine();
            }
            finally
            {
                Console.ForegroundColor = startFg;
            }
        }
    }
}
