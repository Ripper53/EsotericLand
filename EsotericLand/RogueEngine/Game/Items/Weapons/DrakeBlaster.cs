using System;
using System.Collections.Generic;
using System.Text;

namespace EsotericLand.RogueEngine.Game.Items.Weapons {
    public class DrakeBlaster : Weapon {

        private class DrakeBlasterShootAttack : Attack {
            public int Damage = 10;
            public int StaminaCost = 1, ManaCost = 5;

            public override IList<Sprite> GetDescription(Weapon source, Character friendly, Character target) {
                return new Sprite[] {
                    new Sprite($"Shoot for {Damage} damage, costs {StaminaCost} stamina and {ManaCost} mana.")
                };
            }

            public override bool Use(Weapon source, Character friendly, Character target) {
                if (friendly.Stamina.ContainsEnough(StaminaCost) && friendly.Mana.Use(ManaCost)) {
                    friendly.Stamina.Use(StaminaCost);
                    target.Damage(Damage);
                    return true;
                }
                return false;
            }
        }

        private class DrakeBlasterBlastAttack : Attack {
            public int Damage = 100;
            public int StaminaCost = 10, ManaCost = 40;

            public override IList<Sprite> GetDescription(Weapon source, Character friendly, Character target) {
                return new Sprite[] {
                    new Sprite($"Blast for {Damage} damage, costs {StaminaCost} stamina and {ManaCost} mana.")
                };
            }

            public override bool Use(Weapon source, Character friendly, Character target) {
                if (friendly.Stamina.ContainsEnough(StaminaCost) && friendly.Mana.Use(ManaCost)) {
                    friendly.Stamina.Use(StaminaCost);
                    target.Damage(Damage);
                    return true;
                }
                return false;
            }
        }

        public DrakeBlaster() {
            Name = "Drake Blaster";

            Attacks = new Attack[] {
                new DrakeBlasterShootAttack(),
                new DrakeBlasterBlastAttack()
            };
        }

        public override Item Clone() {
            return new DrakeBlaster();
        }
    }
}
