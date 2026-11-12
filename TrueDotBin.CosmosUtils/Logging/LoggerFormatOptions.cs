using System;
using System.Collections.Generic;
using System.Text;

namespace TrueDotBin.CosmosUtils.Logging
{
    /// <summary>
    /// How a log entry should look like.
    /// </summary>
    public sealed class LoggerFormatOptions
    {
        /// <summary>
        /// Whether to show the log's level.
        /// </summary>
        public bool ShowLevel { get; set; } = true;

        /// <summary>
        /// Whether to show the log's date.
        /// </summary>
        public bool ShowDate { get; set; } = false;

        /// <summary>
        /// Whether to show the log's time.
        /// </summary>
        public bool ShowTime { get; set; } = false;

        /// <summary>
        /// Whether to show where the log came from.
        /// </summary>
        public bool ShowSource { get; set; } = false;
    }
}
