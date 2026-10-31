using TrueDotBin.CosmosUtils.Logging;
using Sys = Cosmos.Kernel.System;

namespace TrueDotBin.CosmosUtils.TestKernel
{
    public class Kernel : Sys.Kernel
    {
        protected override void BeforeRun()
        {
            Console.WriteLine("CosmosUtils test kernel booted successfully!");

            Logger.Global.Info("Global logger");

            var testLogger = new Logger("LoggerInstance");
            testLogger.Info("Instanced logger");
        }

        protected override void Run()
        {
            
        }
    }
}
