using Cosmos.Kernel.System.Graphics;
using Cosmos.Kernel.System.Timers;
using TrueDotBin.CosmosUtils.Graphics;
using TrueDotBin.CosmosUtils.Logging;
using TrueDotBin.CosmosUtils.Resources;
using Sys = Cosmos.Kernel.System;

namespace TrueDotBin.CosmosUtils.TestKernel
{
    public class Kernel : Sys.Kernel
    {
        private const string NineSliceTestPath = "TrueDotBin.CosmosUtils.TestKernel.Resources.NineSlice.png";

        protected override void BeforeRun()
        { 
            Console.WriteLine("CosmosUtils test kernel booted successfully!");

            Logger.Global.Info("Global logger");

            var testLogger = new Logger("LoggerInstance")
            {
                LogFormat = "{SOURCE} | {LEVEL} {MESSAGE}"
            };

            testLogger.Info("Instanced logger with custom log format");

            Logger.Global.Info("Preparing for nine-slice test");
            TimerManager.Wait(2000);

            if (!EmbeddedResourceLoader.TryLoadResource(NineSliceTestPath, typeof(Kernel).Assembly, out var nineSliceTestImageBytes))
            {
                Logger.Global.Error($"Could not load \"{NineSliceTestPath}\"");
                Stop();
                return;
            }

            var nineSliceTestImg = new Png(nineSliceTestImageBytes);
            var canvas = Canvas.GetFullScreen();

            canvas.Clear();

            for (int i = 0; i < 4; i++)
            {
                var size = 16 * (i + 1);
                var y = 20 * (i + 1) + (size + 5);
                canvas.DrawNineSliced(nineSliceTestImg, 20, y, size, size, 2);
            }

            canvas.Display();
        }

        protected override void Run()
        {
            
        }
    }
}
