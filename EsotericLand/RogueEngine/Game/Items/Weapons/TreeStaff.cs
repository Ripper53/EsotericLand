using System;
using System.Collections.Generic;
using System.Text;

namespace EsotericLand.RogueEngine.Game.Items.Weapons {
    public class TreeStaff : Weapon {

        private class RootAttack : Attack {
            public int Damage = 5;
            public int ManaCost = 3;

            public override IList<Sprite> GetDescription(Weapon source, Character friendly, Character target) {
                return new Sprite[] {
                    new Sprite($"Root for {Damage} damage, costs {ManaCost} mana.")
                };
            }

            public override bool Use(Weapon source, Character friendly, Character target) {
                if (friendly.Mana.Use(ManaCost)) {
                    target.Damage(Damage);
                    return true;
                }
                return false;
            }
        }

        private class MountainGripAttack : Attack {
            public int Damage = 10;
            public int ManaCost = 5;

            public override IList<Sprite> GetDescription(Weapon source, Character friendly, Character target) {
                return new Sprite[] {
                    new Sprite($"Mountain grip for {Damage} damage, costs {ManaCost} mana.")
                };
            }

            public override bool Use(Weapon source, Character friendly, Character target) {
                if (friendly.Mana.Use(ManaCost)) {
                    target.Damage(Damage);
                    return true;
                }
                return false;
            }
        }

        private class TreeLifeAttack : Attack {
            public int Heal = 4;
            public int ManaCost = 2;

            public override IList<Sprite> GetDescription(Weapon source, Character friendly, Character target) {
                return new Sprite[] {
                    new Sprite($"Tree life heals for {Heal}, costs {ManaCost} mana.")
                };
            }

            public override bool Use(Weapon source, Character friendly, Character target) {
                if (friendly.Mana.Use(ManaCost)) {
                    friendly.Heal(Heal);
                    return true;
                }
                return false;
            }
        }

        public TreeStaff() {
            Name = "Tree Staff";

            Attacks = new Attack[] {
                new RootAttack(),
                new MountainGripAttack(),
                new TreeLifeAttack()
            };
        }

        public override Item Clone() {
            return new TreeStaff();
        }
    }
}
