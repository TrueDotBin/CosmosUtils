using Cosmos.Kernel.System.Graphics;
using Cosmos.Kernel.System.Graphics.Fonts;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace TrueDotBin.CosmosUtils.Graphics
{
    /// <summary>
    /// A utility that allows for aligning content to a rectangle.
    /// </summary>
    public static class AlignedDrawing
    {
        private static Point GetAlignmentPosition(Rectangle rect, int contentWidth, int contentHeight,
            HorizontalAlignment horizontal, VerticalAlignment vertical)
        {
            var x = horizontal switch
            {
                HorizontalAlignment.Left => rect.X,
                HorizontalAlignment.Center => rect.X + (rect.Width - contentWidth) / 2,
                HorizontalAlignment.Right => rect.X + rect.Width - contentWidth,
                _ => throw new InvalidOperationException("Horizontal alignment must be Left, Center or Right")
            };

            var y = vertical switch
            {
                VerticalAlignment.Top => rect.Y,
                VerticalAlignment.Center => rect.Y + (rect.Height - contentHeight) / 2,
                VerticalAlignment.Bottom => rect.Y + rect.Height - contentHeight,
                _ => throw new InvalidOperationException("Vertical alignment must be Top, Center or Bottom")
            };

            return new Point(x, y);
        }

        /// <summary>
        /// Draws a string aligned to the horizontal and vertical alignment.
        /// </summary>
        /// <param name="canvas">The canvas to draw the string on.</param>
        /// <param name="str">The string to draw.</param>
        /// <param name="font">The TrueType font to draw the text with.</param>
        /// <param name="sizePx">The size of the font, in pixels.</param>
        /// <param name="color">The color of the text.</param>
        /// <param name="rect">The source rectangle.</param>
        /// <param name="horizontal">Where the string should be horizontally placed.</param>
        /// <param name="vertical">Where the string should be vertically placed.</param>
        public static void DrawStringAligned(this Canvas canvas, string str, TrueTypeFont font, int sizePx, Color color, Rectangle rect,
            HorizontalAlignment horizontal, VerticalAlignment vertical)
        {
            var aligned = GetAlignmentPosition(rect, font.MeasureString(str), font.GetLineHeight(), horizontal, vertical);
            canvas.DrawString(str, font, sizePx, color, aligned.X, aligned.Y);
        }


        /// <summary>
        /// Draws a string aligned to the horizontal and vertical alignment.
        /// </summary>
        /// <param name="canvas">The canvas to draw the string on.</param>
        /// <param name="str">The string to draw.</param>
        /// <param name="font">The font to draw the text with.</param>
        /// <param name="color">The color of the text.</param>
        /// <param name="rect">The source rectangle.</param>
        /// <param name="horizontal">Where the string should be horizontally placed.</param>
        /// <param name="vertical">Where the string should be vertically placed.</param>
        public static void DrawStringAligned(this Canvas canvas, string str, Font font, Color color, Rectangle rect,
            HorizontalAlignment horizontal, VerticalAlignment vertical)
        {
            var aligned = GetAlignmentPosition(rect, font.MeasureString(str), font.GetLineHeight(), horizontal, vertical);
            canvas.DrawString(str, font, color, aligned.X, aligned.Y);
        }
    }
}
