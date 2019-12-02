using System;
using System.Collections.Generic;
using System.Text;

namespace EsotericLand.RogueEngine {
    public abstract class RunSystemSynchronize : RunSystem, IRunSystemSynchronize {
        public bool Finished { get; private set; }

        public override void Run() {
            Finished = true;
            if (!RunSync())
                Finished = false;
        }

        public abstract bool RunSync();
    }
}
