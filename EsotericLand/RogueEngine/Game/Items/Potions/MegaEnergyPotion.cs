using System;
using System.Collections.Generic;
using System.Text;

namespace EsotericLand.RogueEngine.Game.Items.Potions {
    public class MegaEnergyPotion : EnergyPotion {

        public MegaEnergyPotion() {
            Name = "Mega Energy Potion";
            Energy = 5;
        }

        public override Item Clone() {
            return new MegaEnergyPotion();
        }
    }
}
