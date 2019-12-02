using System;
using System.Collections.Generic;
using System.Text;

namespace EsotericLand.RogueEngine {
    public class RunSystemSynchronizer : RunSystem {
        public IList<RunSystem> RunSystems { get; protected set; }
        protected int currentIndex = 0;

        public void Reset() {
            currentIndex = 0;
        }

        public override void Run() {
            while (currentIndex < RunSystems.Count) {
                RunSystem runSystem = RunSystems[currentIndex];
                runSystem.Run();
                if (runSystem is IRunSystemSynchronize runSync && !runSync.Finished)
                    return;
                currentIndex++;
            }
            Reset();
        }
    }
}
