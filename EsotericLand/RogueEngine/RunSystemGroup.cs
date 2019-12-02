using System;
using System.Collections.Generic;
using System.Text;

namespace EsotericLand.RogueEngine {
    public class RunSystemGroup : RunSystem {
        public IEnumerable<RunSystem> RunSystems;

        public override void Run() {
            foreach (RunSystem runSystem in RunSystems)
                runSystem.Run();
        }
    }
}
