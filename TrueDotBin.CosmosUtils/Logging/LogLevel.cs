using System;
using System.Collections.Generic;
using System.Text;

namespace TrueDotBin.CosmosUtils.Logging
{
    /// <summary>
    /// Log levels for filtering.
    /// </summary>
    public enum LogLevel
    {
        /// <summary>
        /// Diagnostic information, useful during development and troubleshooting.
        /// </summary>
        Debug,

        /// <summary>
        /// Messages that inform the user about something
        /// </summary>
        Info,

        /// <summary>
        /// Indicates a problem that may potentially cause errors.
        /// </summary>
        Warning,

        /// <summary>
        /// Indicates a failure that causes the current operation to stop.
        /// </summary>
        Error,

        /// <summary>
        /// Indicates a failure that causes the system to stop.
        /// </summary>
        Fatal,
    }
}
