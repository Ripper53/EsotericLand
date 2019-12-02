using System;
using System.Collections.Generic;
using System.Text;
using EsotericLand.RogueEngine.Game.Items.Potions;
using EsotericLand.RogueEngine.Game.Items.Weapons;

namespace EsotericLand.RogueEngine.Game.Units {
    public class DragonUnit : UnitAI {

        private class DragonBattleBrain : BattleBrain {
            private enum AttackType {
                TailSwipe,
                FireBreath,
                Claw
            };
            private AttackType atk;

            public int FireBreathDamage = 20, TailSwipeDamage = 10, ClawDamage = 5;

            public override void Action(Character source, Character enemy) {
                if (source.Mana.Use(10)) {
                    atk = AttackType.FireBreath;
                    enemy.Damage(FireBreathDamage);
                } else if (source.Stamina.Use(10)) {
                    atk = AttackType.TailSwipe;
                    enemy.Damage(TailSwipeDamage);
                } else {
                    atk = AttackType.Claw;
                    enemy.Damage(ClawDamage);
                }
            }

            public override IEnumerable<Sprite> GetActionDescription() {
                switch (atk) {
                    case AttackType.FireBreath:
                        return new Sprite[] {
                            new Sprite($"Fire breath dealt {FireBreathDamage} damage.")
                        };
                    case AttackType.TailSwipe:
                        return new Sprite[] {
                            new Sprite($"Tail swipe dealt {TailSwipeDamage} damage.")
                        };
                    default:
                        return new Sprite[] {
                            new Sprite($"Claws dealt {ClawDamage} damage.")
                        };
                }
            }
        }

        public DragonUnit(Scene scene) : base(new Sprite("Ɏ", ConsoleColor.Red), new Character("Dragon", 100, 20, 10), new UnitRandomControls(), scene) {
            ((UnitRandomControls)Controls).FollowDistance = 25;
            Character.GoldDropMin = 75;
            Character.GoldDropMax = 100;

            Character.Stamina.RegenValue = 2;
            Character.Mana.RegenValue = 1;

            if (!SetDropIfPlayerDoesNotHave(this, new DrakeBlaster()) && CurrentScene.PlayerUnit.Character.MaxHealth < 50)
                Character.Drops = new Item[] {
                    new MegaHeartPotion()
                };

            BattleBrain = new DragonBattleBrain();
        }
    }
}
