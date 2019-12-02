using System;
using System.Collections.Generic;
using System.Text;

namespace EsotericLand.RogueEngine.Game {
    public abstract class Potion : Item {

        public override bool Use(PlayerUnit playerUnit) {
            if (Drink(playerUnit)) {
                playerUnit.Inventory.Items.Remove(this);
                return true;
            }
            return false;
        }

        public abstract bool Drink(PlayerUnit playerUnit);
    }
}
