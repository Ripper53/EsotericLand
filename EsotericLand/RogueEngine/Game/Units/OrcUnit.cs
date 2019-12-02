using System;
using System.Collections.Generic;
using System.Text;

namespace EsotericLand.RogueEngine.Game.Units {
    public class OrcUnit : UnitAI {

        private class OrcBattleBrain : BattleBrain {
            private enum AttackType { Punch, Smash };
            private AttackType atk;

            public int PunchDamage = 2, SmashDamage = 4;
            public int SmashStaminaUse = 2;

            public override void Action(Character source, Character enemy) {
                if (source.Stamina.Use(SmashStaminaUse)) {
                    // Smash
                    atk = AttackType.Smash;
                    enemy.Damage(SmashDamage);
                } else {
                    // Punch
                    atk = AttackType.Punch;
                    enemy.Damage(PunchDamage);
                }
            }

            public override IEnumerable<Sprite> GetActionDescription() {
                switch (atk) {
                    case AttackType.Punch:
                        return new Sprite[] {
                            new Sprite($"Punched for {PunchDamage} damage.")
                        };
                    default:
                        return new Sprite[] {
                            new Sprite($"Smashed for {SmashDamage} damage.")
                        };
                }

            }
        }

        public OrcUnit(Scene scene) : base(new Sprite("Ő", ConsoleColor.DarkGreen), new Character("Orc", 10, 2), new UnitRandomControls(), scene) {
            Character.Stamina.RegenValue = 1;
            BattleBrain = new OrcBattleBrain();

            Character.GoldDropMin = 5;
            Character.GoldDropMax = 10;

            ((UnitRandomControls)Controls).FollowDistance = 5;
        }
    }
}
