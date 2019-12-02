using System;
using System.Collections.Generic;
using System.Text;

namespace EsotericLand.RogueEngine.Game.Items.Potions {
    public class IntensityPotion : Potion {
        public int EnergyIncrease = 1;

        public IntensityPotion() {
            Name = "Intensity Potion";
        }

        public override Item Clone() {
            return new IntensityPotion();
        }

        public override bool Drink(PlayerUnit playerUnit) {
            playerUnit.Character.Energy.IncreaseMax(EnergyIncrease);
            return true;
        }
    }
}
