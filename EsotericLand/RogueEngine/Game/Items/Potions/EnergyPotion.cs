using System;
using System.Collections.Generic;
using System.Text;

namespace EsotericLand.RogueEngine.Game.Items.Potions {
    public class EnergyPotion : Potion {
        public int Energy = 1;

        public EnergyPotion() {
            Name = "Energy Potion";
        }

        public override bool Drink(PlayerUnit playerUnit) {
            if (playerUnit.Character.Energy.Value < playerUnit.Character.Energy.Max) {
                playerUnit.Character.Energy.Add(Energy);
                return true;
            }
            return false;
        }

        public override Item Clone() {
            return new EnergyPotion();
        }
    }
}
