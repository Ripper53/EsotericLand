using System;
using System.Collections.Generic;
using System.Text;
using EsotericLand.RogueEngine.Game.Items.Potions;
using EsotericLand.RogueEngine.Game.Items.Weapons;

namespace EsotericLand.RogueEngine.Game.Units {
    public class BanditUnit : UnitAI {

        private class BanditBattleBrain : BattleBrain {
            public int Damage = 1;
            private bool missed = false;

            public override void Action(Character source, Character enemy) {
                if (Program.RNG.Next(0, 2) == 0) {
                    missed = true;
                } else {
                    missed = false;
                    enemy.Damage(Damage);
                }
            }

            public override IEnumerable<Sprite> GetActionDescription() {
                if (missed) {
                    return new Sprite[] {
                        new Sprite("Missed!")
                    };
                } else {
                    return new Sprite[] {
                        new Sprite($"Punched for {Damage} damage.")
                    };
                }
            }
        }

        public BanditUnit(Scene scene) : base(new Sprite("ƃ"), new Character("Bandit", 2), new UnitRandomControls(), scene) {
            BattleBrain = new BanditBattleBrain();
            Character.GoldDropMin = 2;
            Character.GoldDropMax = 5;

            ((UnitRandomControls)Controls).FollowDistance = 10;
            if (!SetDropIfPlayerDoesNotHave(this, new HeavyStoneSword()) && CurrentScene.PlayerUnit.Character.Health < 15) {
                Character.Drops = new Item[] {
                    new HeartPotion()
                };
            }
        }
    }
}
