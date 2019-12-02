using System;
using System.Collections.Generic;
using System.Text;

namespace EsotericLand.RogueEngine {
    public class RunSystemGroupSynchronizer : RunSystemGroup, IRunSystemSynchronize {
        public bool Finished { get; protected set; }

        public override void Run() {
            Finished = true;
            foreach (RunSystem runSystem in RunSystems) {
                runSystem.Run();
                if (runSystem is IRunSystemSynchronize runSync && !runSync.Finished) {
                    Finished = false;
                    return;
                }
            }
        }
    }
}
