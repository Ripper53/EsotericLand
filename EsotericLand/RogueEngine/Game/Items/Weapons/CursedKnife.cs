using System;
using System.Collections.Generic;
using System.Text;

namespace EsotericLand.RogueEngine.Game.Items.Weapons {
    public class CursedKnife : Weapon {

        private class SwipeAttack : Attack {
            public int MinDamage = 300, MaxDamage = 500;
            public int Damage = 0;
            public int StaminaCost = 100;

            public override IList<Sprite> GetDescription(Weapon source, Character friendly, Character target) {
                return new Sprite[] {
                    new Sprite($"Swipe knife for {MinDamage}-{MaxDamage} damage, costs {StaminaCost} stamina.")
                };
            }

            public override bool Use(Weapon source, Character friendly, Character target) {
                if (friendly.Stamina.Use(StaminaCost)) {
                    Damage = Program.RNG.Next(MinDamage, MaxDamage + 1);
                    target.Damage(Damage);
                    return true;
                }
                return false;
            }
        }

        private class JabAttack : Attack {
            public int MinDamage = 10, MaxDamage = 1000;
            public int Damage = 0;
            public int StaminaCost = 100;

            public override IList<Sprite> GetDescription(Weapon source, Character friendly, Character target) {
                return new Sprite[] {
                    new Sprite($"Jab knife for {MinDamage}-{MaxDamage} damage, costs {StaminaCost} stamina.")
                };
            }

            public override bool Use(Weapon source, Character friendly, Character target) {
                if (friendly.Stamina.Use(StaminaCost)) {
                    Damage = Program.RNG.Next(MinDamage, MaxDamage + 1);
                    target.Damage(Damage);
                    return true;
                }
                return false;
            }
        }

        public CursedKnife() {
            Name = "Cursed Knife";

            Attacks = new Attack[] {
                new SwipeAttack(),
                new JabAttack()
            };
        }

        public override Item Clone() {
            return new CursedKnife();
        }
    }
}
