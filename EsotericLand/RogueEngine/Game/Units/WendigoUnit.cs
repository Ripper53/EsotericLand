using System;
using System.Collections.Generic;
using System.Text;

namespace EsotericLand.RogueEngine.Game.Units {
    public class WendigoUnit : UnitAI {

        private class WendigoBattleBrain : BattleBrain {
            private enum AttackType {
                Rest, Charge, Claw
            };
            private AttackType atk;

            public int ChargeDamage = 10, ClawDamage = 5;
            public int ChargeStaminaCost = 10, ClawStaminaCost = 6;

            public override void Action(Character source, Character enemy) {
                if (source.Stamina.Use(ChargeStaminaCost)) {
                    atk = AttackType.Charge;
                    enemy.Damage(ChargeDamage);
                } else if (source.Stamina.Use(ClawStaminaCost)) {
                    atk = AttackType.Claw;
                    enemy.Damage(ClawDamage);
                } else {
                    atk = AttackType.Rest;
                }
            }

            public override IEnumerable<Sprite> GetActionDescription() {
                switch (atk) {
                    case AttackType.Charge:
                        return new Sprite[] {
                            new Sprite($"Charge dealt {ChargeDamage} damage.")
                        };
                    case AttackType.Claw:
                        return new Sprite[] {
                            new Sprite($"Claws dealt {ClawDamage} damage.")
                        };
                    default:
                        return new Sprite[] {
                            new Sprite("Resting...")
                        };
                }
            }
        }

        public WendigoUnit(Scene scene) : base(new Sprite("ɰ", ConsoleColor.Magenta), new Character("Wendigo", 40, 20, 5), new UnitRandomControls(), scene) {
            ((UnitRandomControls)Controls).FollowDistance = 4;
            BattleBrain = new WendigoBattleBrain();
            Character.Stamina.RegenValue = 2;

            Character.GoldDropMin = 20;
            Character.GoldDropMax = 30;
        }
    }
}
