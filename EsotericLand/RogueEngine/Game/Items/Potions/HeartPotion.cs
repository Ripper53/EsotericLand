using System;
using System.Collections.Generic;
using System.Text;

namespace EsotericLand.RogueEngine.Game.Items.Potions {
    public class HeartPotion : Potion {
        public int HealthIncrease = 1;

        public HeartPotion() {
            Name = "Heart Potion";
        }

        public override Item Clone() {
            return new HeartPotion();
        }

        public override bool Drink(PlayerUnit playerUnit) {
            playerUnit.Character.IncreaseMaxHealth(HealthIncrease);
            return true;
        }
    }
}
