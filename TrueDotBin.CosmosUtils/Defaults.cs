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
    }
}
