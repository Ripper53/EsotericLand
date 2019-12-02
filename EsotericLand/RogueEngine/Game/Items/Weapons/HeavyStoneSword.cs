using System;
using System.Collections.Generic;
using System.Text;

namespace EsotericLand.RogueEngine.Game.Items.Weapons {
    public class HeavyStoneSword : Weapon {

        private class HeavyStoneSwordAttack : Attack {
            public int Damage = 5;
            public int StaminaUse = 2;

            public override IList<Sprite> GetDescription(Weapon source, Character friendly, Character target) {
                return new Sprite[] {
                    new Sprite($"Swing for {Damage} damage, costs {StaminaUse} stamina.")
                };
            }

            public override bool Use(Weapon source, Character friendly, Character target) {
                if (friendly.Stamina.Use(StaminaUse)) {
                    target.Damage(Damage);
                    return true;
                }
                return false;
            }
        }

        public HeavyStoneSword() {
            Name = "Heavy Stone Sword";

            Attacks = new Attack[] {
                new HeavyStoneSwordAttack()
            };
        }

        public override Item Clone() {
            return new HeavyStoneSword();
        }
    }
}
