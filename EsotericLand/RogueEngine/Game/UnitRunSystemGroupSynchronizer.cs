using System;
using System.Collections.Generic;
using System.Text;

namespace EsotericLand.RogueEngine.Game {
    public class UnitRunSystemGroupSynchronizer : RunSystemGroupSynchronizer {

        public UnitRunSystemGroupSynchronizer() {
            RunSystems = new RunSystem[] {
                new UnitControlsRunSystem(),
                new UnitRunSystem()
            };
        }
    }
}
