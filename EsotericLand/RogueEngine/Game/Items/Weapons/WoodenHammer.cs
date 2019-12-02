using System;
using System.Collections.Generic;
using System.Text;

namespace EsotericLand.RogueEngine.Game.Items.Weapons {
    public class WoodenHammer : Weapon {

        public class WoodenHammerAttack : Attack {
            public int Damage = 3;

            public override IList<Sprite> GetDescription(Weapon source, Character friendly, Character target) {
                return new Sprite[] {
                    new Sprite($"Slam for {Damage} damage, 50% chance to miss.")
                };
            }

            public override bool Use(Weapon source, Character friendly, Character target) {
                if (Program.RNG.Next(2) == 0)
                    target.Damage(Damage);
                return true;
            }
        }

        public WoodenHammer() {
            Name = "Wooden Hammer";

            Attacks = new Attack[] {
                new WoodenHammerAttack()
            };
        }

        public override Item Clone() {
            return new WoodenHammer();
        }
    }
}
