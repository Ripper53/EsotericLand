using System;
using System.Collections.Generic;
using System.Text;
using EsotericLand.RogueEngine;
using EsotericLand.RogueEngine.Game;

namespace EsotericLand {
    public class GameSystemManager : SystemManager {
        public readonly UnitRunSystemSynchronizer UnitRunSystemSynchronizer;

        public GameSystemManager() {
            UnitRunSystemSynchronizer = new UnitRunSystemSynchronizer();
            UpdateRunSystem = new RunSystem[] {
                UnitRunSystemSynchronizer
            };
        }

        public void Reset() {
            UnitRunSystemSynchronizer.Reset();
        }
    }
}
