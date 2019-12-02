using System;
using System.Collections.Generic;
using System.Text;

namespace EsotericLand.RogueEngine.Game {
    public class BattleRunSystemSynchronizer : RunSystemSynchronizer {

        public BattleRunSystemSynchronizer(BattleMenu battleMenu) {
            RunSystems = new RunSystem[] {
                new MenuRunSystem(),
                new RendererRunSystem(),

                new MenuPlayerControlsRunSystem(),
                new BattleMenuRunSystem(battleMenu)
            };
        }
    }
}
