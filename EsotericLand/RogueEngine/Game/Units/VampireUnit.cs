using System;
using System.Collections.Generic;
using System.Text;
using EsotericLand.RogueEngine.Game.Items.Weapons;

namespace EsotericLand.RogueEngine.Game.Units {
    public class VampireUnit : UnitAI {

        private class VampireBattleBrain : BattleBrain {
            private enum AttackType { Rest, BloodDrain };
            private AttackType atk;
            public int BloodDrainMinDamage = 2, BloodDrainMaxDamage = 5;
            public int BloodDrainManaCost = 2;
            public int RestHeal = 1;
            public int Damage = 0;

            public override void Action(Character source, Character enemy) {
                if (source.Mana.Use(BloodDrainManaCost)) {
                    atk = AttackType.BloodDrain;
                    Damage = Program.RNG.Next(BloodDrainMinDamage, BloodDrainMaxDamage + 1);
                    enemy.Damage(Damage);
                    source.Heal(Damage);
                } else {
                    atk = AttackType.Rest;
                    source.Heal(RestHeal);
                }
            }

            public override IEnumerable<Sprite> GetActionDescription() {
                switch (atk) {
                    case AttackType.BloodDrain:
                        return new Sprite[] {
                            new Sprite($"Blood drain dealt {Damage} damage and healed for {Damage}.")
                        };
                    default:
                        return new Sprite[] {
                            new Sprite($"Resting, healed for {RestHeal}.")
                        };
                }
            }
        }

        public VampireUnit(Scene scene) : base(new Sprite("£", ConsoleColor.DarkRed), new Character("Vampire", 20, mana: 10), new UnitRandomControls(), scene) {
            ((UnitRandomControls)Controls).FollowDistance = 20;
            BattleBrain = new VampireBattleBrain();
            Character.Mana.RegenValue = 1;

            Character.GoldDropMin = 20;
            Character.GoldDropMax = 30;

            SetDropIfPlayerDoesNotHave(this, new ScarletWand());
        }
    }
}
