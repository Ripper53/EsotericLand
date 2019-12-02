using System;
using System.Collections.Generic;
using System.Text;

namespace EsotericLand.RogueEngine {
    public interface IRunSystemSynchronize {
        bool Finished { get; }

        void Run();
    }
}
