using Cosmos.Kernel.System.Graphics;
using Cosmos.Kernel.System.Graphics.Fonts;
using Cosmos.Kernel.System.Timers;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using TrueDotBin.CosmosUtils.Graphics;
using TrueDotBin.CosmosUtils.Logging;
using TrueDotBin.CosmosUtils.Resources;
using Sys = Cosmos.Kernel.System;

namespace TrueDotBin.CosmosUtils.TestKernel
{
    public class Kernel : Sys.Kernel
    {
        private const string NineSliceTestPath = "TrueDotBin.CosmosUtils.TestKernel.Resources.NineSlice.png";
        private const string FontPath = "TrueDotBin.CosmosUtils.TestKernel.Resources.Inter.ttf";

        [AllowNull]
        private TrueTypeFont _inter;

        private void StringAlignmentTest(Canvas canvas)
        {
            canvas.Clear();

            var testString = "Test String";

            var rectWidth = 70;
            var rectHeight = 40;
            var currentY = 20;
            var currentX = 20;
            var spacing = 10;

            foreach (var horizontal in Enum.GetValues<HorizontalAlignment>())
            {
                currentX = 20;

                foreach (var vertical in Enum.GetValues<VerticalAlignment>())
                {
                    var rect = new Rectangle(currentX, currentY, rectWidth, rectHeight);
                    canvas.DrawFilledRectangle(Color.White, currentX, currentY, rectWidth, rectHeight);

                    canvas.DrawStringAligned(testString, _inter, 16, Color.Black, rect, horizontal, vertical);

                    currentX += rectWidth + spacing;
                }

                currentY += rectHeight + spacing;
            }

            canvas.Display();
        }

        private void NineSliceTest(Canvas canvas)
        {
            if (!EmbeddedResourceLoader.TryLoadResource(NineSliceTestPath, typeof(Kernel).Assembly, out var nineSliceTestImageBytes))
            {
                Logger.Global.Error($"Could not load \"{NineSliceTestPath}\"");
                return;
            }

            var nineSliceTestImg = new Png(nineSliceTestImageBytes);

            canvas.Clear();

            for (int i = 0; i < 4; i++)
            {
                var size = 16 * (i + 1);
                var y = 20 * (i + 1) + size + 5;

                canvas.DrawNineSliced(nineSliceTestImg, 20, y, size, size, 2);
            }

            canvas.Display();
        }

        private static void LoggerTest()
        {
            Logger.Global.Info("Global logger");

            var testLogger = new Logger("LoggerInstance")
            {
                LogFormat = "{SOURCE} | {LEVEL} {MESSAGE}"
            };

            testLogger.Info("Instanced logger with custom log format");
        }

        protected override void BeforeRun()
        { 
            Console.WriteLine("CosmosUtils test kernel booted successfully!");

            LoggerTest();

            Logger.Global.Info("Preparing for nine-slice test");
            TimerManager.Wait(2000);

            var canvas = Canvas.GetFullScreen();
            NineSliceTest(canvas);

            TimerManager.Wait(1000);

            if (!EmbeddedResourceLoader.TryLoadResource(FontPath, typeof(Kernel).Assembly, out var fontBytes))
            {
                Stop();
                return;
            }

            _inter = new TrueTypeFont(fontBytes);

            StringAlignmentTest(canvas);
        }

        protected override void Run()
        {
            
        }
    }
}
