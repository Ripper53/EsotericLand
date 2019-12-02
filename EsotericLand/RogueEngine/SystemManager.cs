using System;
using System.Collections.Generic;
using System.Text;

namespace EsotericLand.RogueEngine {
    public class SystemManager {
        public IEnumerable<RunSystem> UpdateRunSystem { get; protected set; }

        public void Update() {
            foreach (RunSystem runSystem in UpdateRunSystem) {
                runSystem.Run();
            }
        }
    }
}
