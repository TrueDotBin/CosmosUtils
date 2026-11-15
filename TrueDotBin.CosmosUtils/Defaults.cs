using TrueDotBin.CosmosUtils.Logging;

namespace TrueDotBin.CosmosUtils
{
    /// <summary>
    /// The global defaults for every class in this utility package.
    /// </summary>
    public static class Defaults
    {
        /// <summary>
        /// The default value of a <see cref="Logger"/>'s source.
        /// </summary>
        public static string DefaultLoggerSource { get; set; } = "Logger";

        /// <summary>
        /// The default value of a <see cref="Logger"/>'s output format.
        /// </summary>
        public static LoggerFormatOptions DefaultOutputFormat { get; set; } = new();

        /// <summary>
        /// The default value of a <see cref="Logger"/>'s file format.
        /// </summary>
        public static LoggerFormatOptions DefaultFileFormat { get; set; } = new();
    }
}
