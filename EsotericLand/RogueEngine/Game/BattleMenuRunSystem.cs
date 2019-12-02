using System;
using System.Collections.Generic;
using System.Text;

namespace EsotericLand.RogueEngine.Game {
    public class BattleMenuRunSystem : RunSystem {
        public readonly BattleMenu BattleMenu;

        public BattleMenuRunSystem(BattleMenu battleMenu) => BattleMenu = battleMenu;

        public override void Run() {
            BattleMenu.Battle();
        }
    }
}
