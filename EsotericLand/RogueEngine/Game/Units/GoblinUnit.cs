using System;
using System.Collections.Generic;
using System.Text;

namespace EsotericLand.RogueEngine.Game.Units {
    public class GoblinUnit : UnitAI {

        private class GoblinBattleBrain : BattleBrain {

            public override void Action(Character source, Character enemy) {
                
            }

            public override IEnumerable<Sprite> GetActionDescription() {
                return new Sprite[] {
                    new Sprite("Frightened!")
                };
            }
        }

        public GoblinUnit(Scene scene) : base(new Sprite("g", ConsoleColor.Green), new Character("Goblin", 1, 10), new UnitRandomControls(), scene) {
            UnitRandomControls controls = (UnitRandomControls)Controls;
            controls.FollowDistance = 5;
            controls.DirectionMultiplier = new Vector2(-1, -1);

            BattleBrain = new GoblinBattleBrain();

            Character.GoldDropMin = 10;
            Character.GoldDropMax = 50;
        }
    }
}
