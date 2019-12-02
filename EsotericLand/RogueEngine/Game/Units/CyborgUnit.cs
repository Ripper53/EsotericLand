using EsotericLand.RogueEngine.Game.Items.Potions;
using EsotericLand.RogueEngine.Game.Items.Weapons;
using System;
using System.Collections.Generic;
using System.Text;

namespace EsotericLand.RogueEngine.Game.Units {
    public class CyborgUnit : UnitAI {

        private class CyborgBattleBrain : BattleBrain {
            private enum AttackType {
                Recharging, RayBlast
            };
            private AttackType atk;

            public int RayBlastDamage = 10;
            public int RayBlastEnergyCost = 10;

            public int RechargeValue = 5;

            public override void Action(Character source, Character enemy) {
                if (source.Energy.Use(RayBlastEnergyCost)) {
                    atk = AttackType.RayBlast;
                    enemy.Damage(RayBlastDamage);
                } else {
                    atk = AttackType.Recharging;
                    source.Energy.Add(RechargeValue);
                }
            }

            public override IEnumerable<Sprite> GetActionDescription() {
                switch (atk) {
                    case AttackType.RayBlast:
                        return new Sprite[] {
                            new Sprite($"Ray blast dealt {RayBlastDamage} damage.")
                        };
                    default:
                        return new Sprite[] {
                            new Sprite("Recharging...")
                        };
                }
            }
        }

        public CyborgUnit(Scene scene) : base(new Sprite("ƍ", ConsoleColor.Blue), new Character("Cyborg", 40, 0, 0, 20), new UnitRandomControls(), scene) {
            ((UnitRandomControls)Controls).FollowDistance = 20;
            BattleBrain = new CyborgBattleBrain();

            Character.GoldDropMin = 20;
            Character.GoldDropMax = 30;

            if (!SetDropIfPlayerDoesNotHave(this, new ShockGun())) {
                Character.Drops = new Item[] {
                    new EnergyPotion()
                };
            }
        }
    }
}
