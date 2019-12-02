using System;
using System.Collections.Generic;
using System.Text;

namespace EsotericLand.RogueEngine.Game.Items.Potions {
    public class AzurePotion : Potion {
        public int ManaIncrease = 1;

        public AzurePotion() {
            Name = "Azure Potion";
        }

        public override Item Clone() {
            return new AzurePotion();
        }

        public override bool Drink(PlayerUnit playerUnit) {
            playerUnit.Character.Mana.IncreaseMax(ManaIncrease);
            return true;
        }

    }
}
