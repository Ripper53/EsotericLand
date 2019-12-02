using System;
using System.Collections.Generic;
using System.Text;

namespace EsotericLand.RogueEngine.Game.Items.Potions {
    public class ManaPotion : Potion {
        public int Mana = 1;

        public ManaPotion() {
            Name = "Mana Potion";
        }

        public override Item Clone() {
            return new ManaPotion();
        }

        public override bool Drink(PlayerUnit playerUnit) {
            if (playerUnit.Character.Mana.Value < playerUnit.Character.Mana.Max) {
                playerUnit.Character.Mana.Add(Mana);
                return true;
            }
            return false;
        }
    }
}
