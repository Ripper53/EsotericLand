using EsotericLand.RogueEngine.Game.Items.Weapons;
using System;
using System.Collections.Generic;
using System.Text;

namespace EsotericLand.RogueEngine.Game.Units {
    public class BoppinUnit : UnitAI {

        private class BoppinBattleBrain : BattleBrain {
            private enum AttackType {
                KnifeSwing,
                Roll
            };
            private AttackType atk;

            public int KnifeSwingMinDamage = 10, KnifeSwingMaxDamage = 100;
            public int Damage = 0, RollDamage = 40;
            public int RollStaminaCost = 110;

            public override void Action(Character source, Character enemy) {
                if (source.Stamina.Use(RollStaminaCost)) {
                    atk = AttackType.Roll;
                    enemy.Damage(RollDamage);
                } else {
                    atk = AttackType.KnifeSwing;
                    Damage = Program.RNG.Next(KnifeSwingMinDamage, KnifeSwingMaxDamage + 1);
                    enemy.Damage(Damage);
                }
            }

            public override IEnumerable<Sprite> GetActionDescription() {
                switch (atk) {
                    case AttackType.Roll:
                        return new Sprite[] {
                            new Sprite($"Roll dealt {RollDamage} damage.")
                        };
                    default:
                        return new Sprite[] {
                            new Sprite($"Knife swing dealt {Damage} damage.")
                        };
                }
            }
        }

        public static bool BoppinDefeated = false;
        public BoppinUnit(Scene scene) : base(new Sprite("Ƀ", ConsoleColor.Red), new Character("Boppin, the Child Devourer", 1000, 500, 500, 500), new UnitRandomControls(), scene) {
            ((UnitRandomControls)Controls).FollowDistance = 5;
            BattleBrain = new BoppinBattleBrain();

            Character.Stamina.RegenValue = 50;
            Character.Mana.RegenValue = 20;
            Character.Destroyed += source => BoppinDefeated = true;

            Character.GoldDropMin = 1000;
            Character.GoldDropMax = 2000;

            if (SetDropIfPlayerDoesNotHave(this, new CursedKnife())) {
                Character.GoldDropThreshold = 0;
            }
        }
    }
}
