using System;
using System.Collections.Generic;
using System.Text;
using EsotericLand.RogueEngine.Game.Items.Potions;
using EsotericLand.RogueEngine.Game.Items.Weapons;

namespace EsotericLand.RogueEngine.Game.Units {
    public class ChestUnit : UnitAI {
        public static readonly Sprite ChestSprite = new Sprite("ň", ConsoleColor.Yellow);

        private class NothingBattleBrain : BattleBrain {
            public override void Action(Character source, Character enemy) { }

            public override IEnumerable<Sprite> GetActionDescription() {
                return new Sprite[] {
                    new Sprite("The Chest laid dormant...")
                };
            }
        }

        public ChestUnit(Scene scene) : base(ChestSprite, new Character("Chest", 5), new UnitIdleControls(), scene) {
            BattleBrain = new NothingBattleBrain();

            Character.GoldDropMin = 10;
            Character.GoldDropMax = 20;

            Character.Drops = new List<Item>() {
                new HealPotion(),
                new ManaPotion(),
                new IntensityPotion()
            };

            if (CurrentScene.PlayerUnit.Inventory.Gold > 20) {
                SetDropIfPlayerDoesNotHave(this, new TreeStaff());
            }

            if (CurrentScene.PlayerUnit.Inventory.Gold > 50) {
                Character.GoldDropMin = 20;
                Character.GoldDropMax = 40;
            }
        }
    }
}
