using Cosmos.Kernel.System.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace TrueDotBin.CosmosUtils.Graphics
{
    /// <summary>
    /// A utility that splits an image into nine parts to prevent scaling distortion.
    /// 
    /// <para>
    /// See the <see href="https://en.wikipedia.org/wiki/9-slice_scaling">Wikipedia page</see> about nine-slice scaling.
    /// </para>
    /// </summary>
    /// <remarks>
    /// <para>
    /// This is an extension to <see cref="Canvas"/>, meaning it can be called like this:
    /// </para>
    /// 
    /// <c>canvas.DrawNineSliced(image, ...)</c>
    /// 
    /// <para>
    /// rather than this:
    /// </para>
    /// 
    /// <c>NineSlicer.DrawNineSliced(canvas, image, ...)</c>
    /// </remarks>
    public static class NineSlicer
    {
        /// <summary>
        /// Draws a nine-sliced image.
        /// </summary>
        /// <param name="canvas">The canvas to draw the nine-sliced image on.</param>
        /// <param name="image">The image to split into nine slices.</param>
        /// <param name="x">The X position of the image.</param>
        /// <param name="y">The Y position of the image.</param>
        /// <param name="width">The destination width.</param>
        /// <param name="height">The destination height.</param>
        /// <param name="border">The size of the border.</param>
        /// <param name="opacity">How visible the image is, 255 being opaque and 0 being transparent.</param>
        public static void DrawNineSliced(this Canvas canvas, Image image, int x, int y, int width, int height, int border, byte opacity = 255)
        {
            var imgWidth = image.Width;
            var imgHeight = image.Height;

            var sourceBorder = Math.Min(border, Math.Min(imgWidth, imgHeight) / 2);
            var drawnBorder = Math.Min(sourceBorder, Math.Min(width, height) / 2);

            var sourceXs = new[] { 0, sourceBorder, imgWidth - sourceBorder };
            var sourceWidths = new[] { sourceBorder, imgWidth - 2 * sourceBorder, sourceBorder };

            var sourceYs = new[] { 0, sourceBorder, imgHeight - sourceBorder };
            var sourceHeights = new[] { sourceBorder, imgHeight - 2 * sourceBorder, sourceBorder };

            var destXs = new[] { x, x + drawnBorder, x + width - drawnBorder };
            var destWidths = new[] { drawnBorder, width - 2 * drawnBorder, drawnBorder };

            var destYs = new[] { y, y + drawnBorder, y + height - drawnBorder };
            var destHeights = new[] { drawnBorder, height - 2 * drawnBorder, drawnBorder };

            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    var src = new Rectangle(sourceXs[col], sourceYs[row], sourceWidths[col], sourceHeights[row]);
                    var dest = new Rectangle(destXs[col], destYs[row], destWidths[col], destHeights[row]);

                    canvas.DrawImage(image, dest, src, opacity);
                }
            }
        }
    }
}
