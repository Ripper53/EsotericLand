using System;
using System.Collections.Generic;
using System.Text;

namespace EsotericLand.RogueEngine.Game.Items.Weapons {
    public class ScarletWand : Weapon {

        private class HeatAttack : Attack {
            public int Damage = 5;
            public int HealthCost = 1;

            public override IList<Sprite> GetDescription(Weapon source, Character friendly, Character target) {
                return new Sprite[] {
                    new Sprite($"Heat wave for {Damage} Damage, costs {HealthCost} health.")
                };
            }

            public override bool Use(Weapon source, Character friendly, Character target) {
                if (friendly.UseHealth(HealthCost)) {
                    target.Damage(Damage);
                    return true;
                }
                return false;
            }
        }

        private class BloodDrainAttack : Attack {
            public int MinDamage = 1, MaxDamage = 5;
            public int ManaCost = 5;
            public int Damage = 0;

            public override IList<Sprite> GetDescription(Weapon source, Character friendly, Character target) {
                return new Sprite[] {
                    new Sprite($"Blood drain for {MinDamage}-{MaxDamage} damage and heal for that amount, costs {ManaCost} mana.")
                };
            }

            public override bool Use(Weapon source, Character friendly, Character target) {
                if (friendly.Mana.Use(ManaCost)) {
                    Damage = Program.RNG.Next(MinDamage, MaxDamage + 1);
                    target.Damage(Damage);
                    friendly.Heal(Damage);
                    return true;
                }
                return false;
            }
        }

        public ScarletWand() {
            Name = "Scarlet Wand";

            Attacks = new Attack[] {
                new HeatAttack(),
                new BloodDrainAttack()
            };
        }

        public override Item Clone() {
            return new ScarletWand();
        }
    }
}
