using System;
using System.Collections.Generic;
using System.Text;
using EsotericLand.RogueEngine;
using EsotericLand.RogueEngine.Game;

namespace EsotericLand {
    public class BattleSystemManager : SystemManager {
        public bool InBattle = false;
        public readonly BattleRunSystemSynchronizer BattleRunSystemSynchronizer;

        public BattleSystemManager(BattleMenu battleMenu) {
            BattleRunSystemSynchronizer = new BattleRunSystemSynchronizer(battleMenu);
            UpdateRunSystem = new RunSystem[] {
                new BattleRunSystemSynchronizer(battleMenu)
            };
        }
    }
}
