using System;
using System.Collections.Generic;
using System.Text;

namespace EsotericLand.RogueEngine.Game.Items.Weapons {
    public class FireBall : Weapon {

        private class FireBallAttack : Attack {
            public int Damage = 5;
            public int ManaCost = 2;

            public override IList<Sprite> GetDescription(Weapon source, Character friendly, Character target) {
                return new Sprite[] {
                    new Sprite($"Throw a fireball that deals {Damage} damage, costs {ManaCost} mana.")
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

        public FireBall() {
            Name = "Fire Ball";

            Attacks = new Attack[] {
                new FireBallAttack()
            };
        }

        public override Item Clone() {
            return new FireBall();
        }
    }
}
