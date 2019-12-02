using System;
using System.Collections.Generic;
using System.Text;

namespace EsotericLand.RogueEngine.Game.Items.Weapons {
    public class BareHands : Weapon {

        private class BareHandsPunchAttack : Attack {
            public int Damage = 1;

            public override IList<Sprite> GetDescription(Weapon source, Character friendly, Character target) {
                return new Sprite[] {
                    new Sprite($"Punch for {Damage} damage.")
                };
            }

            public override bool Use(Weapon source, Character friendly, Character target) {
                target.Damage(Damage);
                return true;
            }
        }

        private class BareHandsSlapAttack : Attack {
            public int Damage = 2;
            public int StaminaCost = 2;

            public override IList<Sprite> GetDescription(Weapon source, Character friendly, Character target) {
                return new Sprite[] {
                    new Sprite($"Slap for {Damage} damage, costs {StaminaCost} stamina.")
                };
            }

            public override bool Use(Weapon source, Character friendly, Character target) {
                if (friendly.Stamina.Use(StaminaCost)) {
                    target.Damage(Damage);
                    return true;
                }
                return false;
            }
        }

        private class BareHandsKickAttack : Attack {
            public int Damage = 3;
            public int StaminaCost = 3;

            public override IList<Sprite> GetDescription(Weapon source, Character friendly, Character target) {
                return new Sprite[] {
                    new Sprite($"Kick for {Damage} damage, costs {StaminaCost} stamina.")
                };
            }

            public override bool Use(Weapon source, Character friendly, Character target) {
                if (friendly.Stamina.Use(StaminaCost)) {
                    target.Damage(Damage);
                    return true;
                }
                return false;
            }
        }

        public BareHands() {
            Name = "Bare";

            Attacks = new Attack[] {
                new BareHandsPunchAttack(),
                new BareHandsSlapAttack(),
                new BareHandsKickAttack()
            };
        }

        public override Item Clone() {
            return new BareHands();
        }
    }
}
