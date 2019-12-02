using System;
using System.Collections.Generic;
using System.Text;

namespace EsotericLand.RogueEngine.Game.Units {
    public class LivingChestUnit : UnitAI {

        private class LivingChestBattleBrain : BattleBrain {
            public int MinDamage = 2, MaxDamage = 6;
            public int Damage = 0;

            public override void Action(Character source, Character enemy) {
                Damage = Program.RNG.Next(MinDamage, MaxDamage);
                enemy.Damage(Damage);
            }

            public override IEnumerable<Sprite> GetActionDescription() {
                return new Sprite[] {
                    new Sprite($"Bite dealt {Damage} damage.")
                };
            }
        }

        public LivingChestUnit(Scene scene) : base(ChestUnit.ChestSprite, new Character("Living Chest", Program.RNG.Next(5, 11)), new UnitIdleControls(), scene) {
            BattleBrain = new LivingChestBattleBrain();

            Character.GoldDropMin = 0;
            Character.GoldDropMax = 0;
        }
    }
}
