using System;
using System.Collections.Generic;
using System.Text;

namespace TrueDotBin.CosmosUtils.Logging
{
    /// <summary>
    /// A log's fragment, which contains a value and a color.
    /// </summary>
    /// <param name="text">The text of this fragment.</param>
    /// <param name="color">The color of this fragment.</param>
    public readonly struct LogFragment(string text, ConsoleColor? color = null)
    {
        /// <summary>
        /// The text of this fragment.
        /// </summary>
        public string Text { get; } = text;

        /// <summary>
        /// The optional color of this fragment.
        /// </summary>
        public ConsoleColor? Color { get; } = color;
    }
}
