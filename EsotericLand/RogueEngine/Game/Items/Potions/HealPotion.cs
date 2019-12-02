using System;
using System.Collections.Generic;
using System.Text;

namespace EsotericLand.RogueEngine.Game.Items.Potions {
    public class HealPotion : Potion {
        public int Heal = 2;

        public HealPotion() {
            Name = "Heal Potion";
        }

        public override Item Clone() {
            return new HealPotion();
        }

        public override bool Drink(PlayerUnit playerUnit) {
            if (playerUnit.Character.Health < playerUnit.Character.MaxHealth) {
                playerUnit.Character.Heal(Heal);
                return true;
            }
            return false;
        }
    }
}
