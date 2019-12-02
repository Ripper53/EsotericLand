using System;
using System.Collections.Generic;
using System.Text;

namespace EsotericLand.RogueEngine.Game.Items.Weapons {
    public class ShockGun : Weapon {

        private class ShockAttack : Attack {
            public int MinDamage = 5, MaxDamage = 10;
            public int Damage = 0;
            public int EnergyCost = 2;

            public override IList<Sprite> GetDescription(Weapon source, Character friendly, Character target) {
                return new Sprite[] {
                    new Sprite($"Shock for {MinDamage}-{MaxDamage}, costs {EnergyCost} energy.")
                };
            }

            public override bool Use(Weapon source, Character friendly, Character target) {
                if (friendly.Energy.Use(EnergyCost)) {
                    Damage = Program.RNG.Next(MinDamage, MaxDamage + 1);
                    target.Damage(Damage);
                    return true;
                }
                return false;
            }
        }

        public ShockGun() {
            Name = "Shock Gun";

            Attacks = new Attack[] {
                new ShockAttack()
            };
        }

        public override Item Clone() {
            return new ShockGun();
        }
    }
}
