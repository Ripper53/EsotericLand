using System;
using System.Collections.Generic;
using EsotericLand.RogueEngine.Game.Items.Potions;
using System.Text;

namespace EsotericLand.RogueEngine.Game.Units {
    public class TrollUnit : UnitAI {

        private class TrollBattleBrain : BattleBrain {
            private enum AttackType { Nothing, FireBall, Punch };
            private AttackType atk;

            public int FireBallDamage = 4, PunchDamage = 1;
            public int FireBallManaCost = 4, PunchStaminaCost = 2;

            public override void Action(Character source, Character enemy) {
                if (source.Mana.Use(FireBallManaCost)) {
                    atk = AttackType.FireBall;
                    enemy.Damage(FireBallDamage);
                } else if (source.Stamina.Use(PunchStaminaCost)) {
                    atk = AttackType.Punch;
                    enemy.Damage(PunchDamage);
                } else {
                    atk = AttackType.Nothing;
                }
            }

            public override IEnumerable<Sprite> GetActionDescription() {
                switch (atk) {
                    case AttackType.FireBall:
                        return new Sprite[] {
                            new Sprite($"Dealt {FireBallDamage} damage with a Fire Ball!")
                        };
                    case AttackType.Punch:
                        return new Sprite[] {
                            new Sprite($"Punched for {PunchDamage} damage.")
                        };
                    default:
                        return new Sprite[] {
                            new Sprite("Resting!")
                        };
                }
            }
        }

        public TrollUnit(Scene scene) : base(new Sprite("ƾ", ConsoleColor.Green), new Character("Troll", 10, 5, 4), new UnitRandomControls(), scene) {
            ((UnitRandomControls)Controls).FollowDistance = 10;
            Character.Stamina.RegenValue = 1;
            Character.Mana.RegenValue = 1;
            Character.GoldDropMin = 5;
            Character.GoldDropMax = 10;
            BattleBrain = new TrollBattleBrain();

            Character.Drops = new List<Item>();
            
            if (scene.PlayerUnit.Character.Mana.Max < 2) {
                Character.Drops.Add(new AzurePotion());
            }
        }
    }
}
